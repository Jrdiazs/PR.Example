using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PR.Services.ModelResponse
{
    /// <summary>
    /// Modelo para el mapeo de datos de la entidad UserApp
    /// </summary>
    public class UserViewModel
    {
        [Required]
        [DisplayName("User Id")]
        public int UserId { get; set; }

        [Required]
        [DisplayName("Type Document Id")]
        public int DocumentTypeId { get; set; }

        [Required]
        [DisplayName("User Nick Name")]
        public string UserNickName { get; set; }

        [Required]
        [DisplayName("Document Number")]
        public string UserDocumentId { get; set; }

        [Required]
        [DisplayName("User Name")]
        public string UserName { get; set; }

        [Required]
        [DisplayName("User Last Name")]
        public string UserLastName { get; set; }

        [EmailAddress]
        [DisplayName("User Email")]
        public string UserEmail { get; set; }

        [DisplayName("Date Creation")]
        public DateTime? UserDateCreation { get; set; }

        [DisplayName("Full Name")]
        public string UserFullName { get; set; }

        [DisplayName("Document Type")]
        public string DocumentType { get; set; }

        [DisplayName("Document Type Abbreviation")]
        public string DocumentAbbreviation { get; set; }

        [DisplayName("User Password")]
        public string UserPassword { get; set; }

        [DisplayName("Confirm Password")]
        public string ConfirmPassword { get; set; }
    }
}