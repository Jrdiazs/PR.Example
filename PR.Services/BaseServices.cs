using AutoMapper;
using FluentValidation.Results;
using System;

namespace PR.Services
{
    /// <summary>
    /// Clase base de servicios
    /// </summary>
    public abstract class BaseServices : IDisposable, IBaseServices
    {
        /// <summary>
        /// Objeto Mapper para generar los mapeos
        /// </summary>
        public IMapper Mapper { get; }

        /// <summary>
        /// Instancia la clase de sercicios con el objeto mapper ya construido
        /// </summary>
        /// <param name="mapper"></param>
        public BaseServices(IMapper mapper)
        {
            Mapper = mapper;
        }

        /// <summary>
        /// Obtiene los errores de una validacion de modelo por FluentValidation
        /// </summary>
        /// <param name="results"></param>
        /// <returns></returns>
        public string GetMessageValidation(ValidationResult result)
        {
            try
            {
                string message = string.Empty;

                foreach (ValidationFailure item in result.Errors)
                {
                    message += $"Failed validation {item.ErrorMessage} \n";
                }
                return message;
            }
            catch (Exception)
            {
                throw;
            }
        }

        #region [Dispose]

        private bool disposedValue;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects)
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                disposedValue = true;
            }
        }

        // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        // ~BaseServices()
        // {
        //     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        //     Dispose(disposing: false);
        // }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);

            #endregion [Dispose]
        }
    }

    /// <summary>
    /// Clase base de servicios
    /// </summary>
    public interface IBaseServices : IDisposable
    {
        /// <summary>
        /// Objeto Mapper para generar los mapeos
        /// </summary>
        IMapper Mapper { get; }

        /// <summary>
        /// Obtiene los errores de una validacion de modelo por FluentValidation
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        string GetMessageValidation(ValidationResult result);
    }
}