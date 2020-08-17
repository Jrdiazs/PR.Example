namespace PR.Services.ModelRequest
{
    /// <summary>
    /// Modelo para consultar los datos del usuario
    /// </summary>
    public class UserRequest
    {
        /// <summary>
        /// Nick Name
        /// </summary>
        public string NickName { get; set; }

        /// <summary>
        /// Numero de documento
        /// </summary>
        public string DocumentNumber { get; set; }

        /// <summary>
        /// Nombre Completo
        /// </summary>
        public string FullName { get; set; }

        /// <summary>
        /// Consulta por el tipo de document
        /// </summary>
        public int? DocumentTypeId { get; set; }
    }
}