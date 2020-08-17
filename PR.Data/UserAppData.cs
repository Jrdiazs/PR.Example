using Dapper;
using PR.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace PR.Data
{
    /// <summary>
    /// Repositorio para la tabla UserApp
    /// </summary>
    public class UserAppData : RepositoryGeneric<UserApp>, IUserAppData, IDisposable
    {
        /// <summary>
        /// Repositorio para la tabla UserApp
        /// </summary>
        public UserAppData() : base() { }

        /// <summary>
        /// Verifica si existe un usuario por id
        /// </summary>
        /// <param name="user">usuairo</param>
        /// <param name="transaction">transaccion sql</param>
        /// <returns>true or false</returns>
        public bool ExistUserFromById(UserApp user, IDbTransaction transaction = null)
        {
            try
            {
                int count = -1;
                count = Count("WHERE UserId =@Id", new { Id = user.UserId }, transaction: transaction);
                bool existUser = count > 0;
                return existUser;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Verifica si existe un usuario por nombre
        /// </summary>
        /// <param name="user">usuairo</param>
        /// <param name="transaction">transaccion sql</param>
        /// <returns>true or false</returns>
        public bool ExistUserFromByNickName(UserApp user, IDbTransaction transaction = null)
        {
            try
            {
                if (ExistUserFromById(user, transaction: transaction))
                {
                    int count = Count("WHERE UserNickName=@Nick AND UserId<>@Id", new
                    {
                        Nick = user.UserNickName,
                        Id = user.UserId
                    }, transaction: transaction);

                    return count > 0;
                }
                else
                {
                    int count = Count("WHERE UserNickName=@Nick", new
                    {
                        Nick = user.UserNickName
                    }, transaction: transaction);

                    return count > 0;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Consulta un listado de usuarios por id de usuario, tipo de documento, nickName, nombre completo, documento de usuario
        /// </summary>
        /// <param name="userId">usuario id</param>
        /// <param name="documentTypeId">tipo de documento</param>
        /// <param name="userNickName">nickname</param>
        /// <param name="fullName">nombre completo</param>
        /// <param name="userDocumentId">numero de documento</param>
        /// <param name="transaction">transaccion sql</param>
        /// <returns>List UserApp</returns>
        public List<UserApp> GetUserApps(int? userId = null, int? documentTypeId = null, string userNickName = null, string fullName = null, string userDocumentId = null, IDbTransaction transaction = null)
        {
            try
            {
                var users = new List<UserApp>();

                users = Connection.Query<UserApp, DocumentType, UserApp>("UserApp_Search_ALL", map: (ua, dt) => { ua.DocumentType = dt; return ua; }, param: new
                {
                    UserId = userId,
                    DocumentTypeId = documentTypeId,
                    UserNickName = userNickName,
                    UserFullName = fullName,
                    UserDocumentId = userDocumentId
                }, transaction: transaction, commandType: CommandType.StoredProcedure, splitOn: "split").ToList();

                return users ?? new List<UserApp>();
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Consulta la informacion del usuario por id
        /// </summary>
        /// <param name="userId">id del usuario</param>
        /// <param name="completeInfo">determina si debe consultar la informacion del usuario</param>
        /// <param name="transaction">transaccion sql</param>
        /// <returns></returns>
        public UserApp GetUserFromById(int userId, bool completeInfo, IDbTransaction transaction = null)
        {
            try
            {
                var users = new List<UserApp>();
                if (completeInfo)
                {
                    users = GetUserApps(userId: userId, transaction: transaction);
                    return users.FirstOrDefault();
                }
                else
                {
                    UserApp user = GetFindId(userId, transaction: transaction);
                    return user;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Consulta el usuario por NickName
        /// </summary>
        /// <param name="nickName">nick Name del usuario</param>
        /// <param name="completeInfo">determina si debe consultar la informacion completa del usuario</param>
        /// <param name="transaction">transaccion sql</param>
        /// <returns>UserApp</returns>
        public UserApp GetUserFromByNickName(string nickName, bool completeInfo = false, IDbTransaction transaction = null)
        {
            try
            {
                var users = new List<UserApp>();
                users = completeInfo ? GetUserApps(userNickName: nickName, transaction: transaction) :
                    users = GetList("WHERE UserNickName=@UserNickName", new
                    {
                        UserNickName = nickName
                    }, transaction: transaction);

                return users.FirstOrDefault();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }

    /// <summary>
    /// Repositorio para la tabla UserApp
    /// </summary>
    public interface IUserAppData : IRepositoryGeneric<UserApp>, IDisposable
    {
        /// <summary>
        /// Consulta el usuario por NickName
        /// </summary>
        /// <param name="nickName">nick Name del usuario</param>
        /// <param name="completeInfo">determina si debe consultar la informacion completa del usuario</param>
        /// <param name="transaction">transaccion sql</param>
        /// <returns>UserApp</returns>
        UserApp GetUserFromByNickName(string nickName, bool completeInfo = false, IDbTransaction transaction = null);

        /// <summary>
        /// Verifica si existe un usuario por id
        /// </summary>
        /// <param name="user">usuairo</param>
        /// <param name="transaction">transaccion sql</param>
        /// <returns>true or false</returns>
        bool ExistUserFromById(UserApp user, IDbTransaction transaction = null);

        /// <summary>
        /// Verifica si existe un usuario por nombre
        /// </summary>
        /// <param name="user">usuairo</param>
        /// <param name="transaction">transaccion sql</param>
        /// <returns>true or false</returns>
        bool ExistUserFromByNickName(UserApp user, IDbTransaction transaction = null);

        /// <summary>
        /// Consulta la informacion del usuario por id
        /// </summary>
        /// <param name="userId">id del usuario</param>
        /// <param name="completeInfo">determina si debe consultar la informacion del usuario</param>
        /// <param name="transaction">transaccion sql</param>
        /// <returns></returns>
        UserApp GetUserFromById(int userId, bool completeInfo, IDbTransaction transaction = null);

        /// <summary>
        /// Consulta un listado de usuarios por id de usuario, tipo de documento, nickName, nombre completo, documento de usuario
        /// </summary>
        /// <param name="userId">usuario id</param>
        /// <param name="documentTypeId">tipo de documento</param>
        /// <param name="userNickName">nickname</param>
        /// <param name="fullName">nombre completo</param>
        /// <param name="userDocumentId">numero de documento</param>
        /// <param name="transaction">transaccion sql</param>
        /// <returns>List UserApp</returns>
        List<UserApp> GetUserApps(int? userId = null, int? documentTypeId = null, string userNickName = null, string fullName = null, string userDocumentId = null, IDbTransaction transaction = null);
    }
}