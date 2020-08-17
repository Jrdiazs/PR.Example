using Microsoft.AspNetCore.Mvc;
using PR.Services;
using PR.Services.ModelResponse;
using PR.Services.Validate;
using PR.Tools.String;
using System;

namespace Web.Site.UI.Controllers
{
    /// <summary>
    /// Controlador para el manejo del componente IUserServices
    /// </summary>
    [Route("api/UserApp")]
    [ApiController]
    public class UserAppController : ControllerBase
    {
        /// <summary>
        /// Servicio IUserServices
        /// </summary>
        private readonly IUserServices _services;

        /// <summary>
        /// Controlador para el manejo del componente IUserServices
        /// </summary>
        /// <param name="services">IUserServices</param>
        public UserAppController(IUserServices services)
        {
            _services = services ?? throw new ArgumentNullException(nameof(services));
        }

        /// <summary>
        /// Crea un nuevo usuario en la base de datos
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("CreateNewUserApp")]
        public IActionResult CreateNewUserApp(UserViewModel user)
        {
            try
            {
                if (!user.UserPassword.IsBase64String())
                    user.UserPassword = user.UserPassword.Base64Encode();

                if (!user.ConfirmPassword.IsBase64String())
                    user.ConfirmPassword = user.ConfirmPassword.Base64Encode();

                if (!ModelState.IsValid)
                    return BadRequest();

                var userBd = _services.CreateNewUserApp(user);

                return Ok(userBd);
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
        /// Actualizar un usuario en la base de datos por id
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("UpdateUserApp")]
        public IActionResult UpdateUserApp(UserViewModel user)
        {
            try
            {

                if (!ModelState.IsValid)
                    return BadRequest();

                var userBd = _services.UpdateUserApp(user);

                return Ok(userBd);
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
    }
}