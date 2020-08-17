using System;

namespace PR.Services.Validate
{
    /// <summary>
    /// Clase de excepcion para la capa de servicios
    /// </summary>
    public class BusinessException : Exception
    {
        public BusinessException() : base()
        {
        }

        public BusinessException(string message) : base(message)
        {
        }

        public BusinessException(string message, Exception exception) : base(message, exception)
        {
        }
    }
}