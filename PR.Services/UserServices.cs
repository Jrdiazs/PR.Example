using AutoMapper;
using PR.Data;
using PR.Models;
using PR.Services.ModelRequest;
using PR.Services.ModelResponse;
using PR.Services.Validate;
using PR.Tools.String;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PR.Services
{
    /// <summary>
    /// Clase de servicio para el manejo del repositorio IUserAppData
    /// </summary>
    public class UserServices : BaseServices, IUserServices, IDisposable
    {
        /// <summary>
        /// Repositorio de la tabla UserApp
        /// </summary>
        private readonly IUserAppData _data;

        /// <summary>
        /// Repositorio para la tabla DocumentType
        /// </summary>
        private readonly IDocumentTypeData _documentTypeData;

        /// <summary>
        /// Instancia un nuevo servicio para el manejo del repositorio IUserAppData
        /// </summary>
        /// <param name="data">repositorio IUserAppData</param>
        /// <param name="mapper">mapeo de objetos</param>
        public UserServices(IUserAppData data, IDocumentTypeData documentData, IMapper mapper) : base(mapper)
        {
            _data = data ?? throw new ArgumentNullException(nameof(data));
            _documentTypeData = documentData ?? throw new ArgumentNullException(nameof(documentData));
        }

        /// <summary>
        /// Agrega un usuario nuevo a la base de datos
        /// verifica que no existe por nick Name
        /// </summary>
        /// <param name="user">usuario</param>
        /// <returns></returns>
        public UserViewModel CreateNewUserApp(UserViewModel user)
        {
            try
            {
                user.UserDateCreation = DateTime.Now;

                ValidUserFields(user);

                UserApp userApp = Mapper.Map<UserApp>(user);

                ValidateUserUniqueFromByNickName(userApp);

                string pwBase64 = user.UserPassword.Base64Decode();
                string keyDecript = "KeyDecript".ReadKey();

                string pwEncript = pwBase64.Encode(keyDecript);
                userApp.UserPw = pwEncript;

                int userAppId = _data.Insert<int>(userApp);
                userApp.UserId = userAppId;

                UserApp userpAppBd = _data.GetUserFromById(userAppId, true);

                user = Mapper.Map<UserViewModel>(userpAppBd);

                return user;
            }
            catch (BusinessException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Modifica un usuario por id
        /// </summary>
        /// <param name="user">usuario</param>
        /// <returns>UserResponse</returns>
        public UserViewModel UpdateUserApp(UserViewModel user)
        {
            try
            {
                #region [Validacion Negocio]

                ValidUserFields(user);

                UserApp userApp = Mapper.Map<UserApp>(user);

                UserApp userAppBd = _data.GetUserFromById(userApp.UserId, true);

                if (userAppBd == null)
                    throw new BusinessException($"No existe el usuario con id {user.UserId}");

                ValidateUserUniqueFromByNickName(userApp);

                #endregion [Validacion Negocio]

                userAppBd.DocumentTypeId = userApp.DocumentTypeId;
                userAppBd.UserDocumentId = userApp.UserDocumentId;
                userAppBd.UserEmail = userApp.UserEmail;
                userAppBd.UserLastName = userApp.UserLastName;
                userAppBd.UserName = userApp.UserName;
                userAppBd.UserNickName = userApp.UserNickName;

                _data.Update(userAppBd);

                user = Mapper.Map<UserViewModel>(userAppBd);

                return user;
            }
            catch (BusinessException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Consulta un usuario por nick Name
        /// </summary>
        /// <param name="nickName">nick Name</param>
        /// <returns></returns>
        public UserViewModel GetUserFromByNickName(string nickName)
        {
            try
            {
                var user = _data.GetUserFromByNickName(nickName, true);
                if (user == null)
                    throw new BusinessException($"No existe el usuario con id {user.UserId}");
                var userModel = Mapper.Map<UserViewModel>(user);

                return userModel;
            }
            catch (BusinessException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Consulta un usuario por id
        /// </summary>
        /// <param name="userId">usuario id</param>
        /// <returns>UserViewModel</returns>
        public UserViewModel GetUserFromById(int userId)
        {
            try
            {
                var user = _data.GetUserFromById(userId, true);
                if (user == null)
                    throw new BusinessException($"No existe el usuario con id {user.UserId}");
                var userModel = Mapper.Map<UserViewModel>(user);

                return userModel;
            }
            catch (BusinessException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Consulta un listado de usuario segun el fitrol
        /// </summary>
        /// <param name="filter">filtro</param>
        /// <returns></returns>
        public List<UserViewModel> GetUserFromFilter(UserRequest filter)
        {
            try
            {
                var query = _data.GetUserApps(fullName: filter.FullName, documentTypeId: filter.DocumentTypeId, userDocumentId: filter.DocumentNumber);
                var users = Mapper.Map<List<UserViewModel>>(query);
                return users ?? new List<UserViewModel>();
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Consulta los tipos de documentos
        /// </summary>
        /// <returns></returns>
        public List<ComboItem> GetTypeDocuments()
        {
            try
            {
                var documents = _documentTypeData.GetAll();

                var query = documents.Select(x => new ComboItem()
                {
                    Value = x.DocumentTypeId,
                    Text = x.DocumentDescription,
                    SecodValue = x.DocumentAbbreviation
                }).
                    ToList();

                return query ?? new List<ComboItem>();
            }
            catch (Exception)
            {
                throw;
            }
        }

        #region [Metodos Comunes]

        /// <summary>
        /// Verifica que no exista duplicado en la base de datos un usuario por el campo UserNickName
        /// </summary>
        /// <param name="userApp">UserApp</param>
        private void ValidateUserUniqueFromByNickName(UserApp userApp)
        {
            try
            {
                if (_data.ExistUserFromByNickName(userApp))
                    throw new BusinessException($"Ya se encuentra registrado un usuario con el nick Name '{userApp.UserNickName}' verifique !!");
            }
            catch (BusinessException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Metodo para validar el usuario cuando se inserta en la base de datos
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        private void ValidUserFields(UserViewModel user)
        {
            try
            {
                var validator = new UserAppValidator();
                var result = validator.Validate(user);

                if (!result.IsValid)
                    throw new BusinessException(GetMessageValidation(result));
            }
            catch (BusinessException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion [Metodos Comunes]
    }

    /// <summary>
    /// Clase de servicio para el manejo del repositorio IUserAppData
    /// </summary>
    public interface IUserServices : IBaseServices, IDisposable
    {
        /// <summary>
        /// Agrega un usuario nuevo a la base de datos
        /// verifica que no existe por nick Name
        /// </summary>
        /// <param name="user">usuario</param>
        /// <returns></returns>
        UserViewModel CreateNewUserApp(UserViewModel user);

        /// <summary>
        /// Modifica un usuario por id
        /// </summary>
        /// <param name="user">usuario</param>
        /// <returns>UserResponse</returns>
        UserViewModel UpdateUserApp(UserViewModel user);

        /// <summary>
        /// Consulta un usuario por nick Name
        /// </summary>
        /// <param name="nickName">nick Name</param>
        /// <returns></returns>
        UserViewModel GetUserFromByNickName(string nickName);

        /// <summary>
        /// Consulta un usuario por id
        /// </summary>
        /// <param name="userId">usuario id</param>
        /// <returns>UserViewModel</returns>
        UserViewModel GetUserFromById(int userId);

        /// <summary>
        /// Consulta un listado de usuario segun el fitrol
        /// </summary>
        /// <param name="filter">filtro</param>
        /// <returns></returns>
        List<UserViewModel> GetUserFromFilter(UserRequest filter);

        /// <summary>
        /// Consulta los tipos de documentos
        /// </summary>
        /// <returns></returns>
        List<ComboItem> GetTypeDocuments();
    }
}