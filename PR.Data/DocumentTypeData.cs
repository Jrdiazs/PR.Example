using PR.Models;
using System;

namespace PR.Data
{
    /// <summary>
    /// Repositorio de datos para la tabla DocumentType
    /// </summary>
    public class DocumentTypeData : RepositoryGeneric<DocumentType>, IDocumentTypeData, IDisposable
    {
        /// <summary>
        /// Repositorio de datos para la tabla DocumentType
        /// </summary>
        public DocumentTypeData() : base() { }
    }

    /// <summary>
    /// Repositorio de datos para la tabla DocumentType
    /// </summary>
    public interface IDocumentTypeData : IRepositoryGeneric<DocumentType>, IDisposable
    {
    }
}