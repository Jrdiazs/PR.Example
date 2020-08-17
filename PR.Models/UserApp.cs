using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PR.Models
{
    [Table("UserApp")]
    public class UserApp
    {
        [Key]
        [Column("UserId")]
        public int UserId { get; set; }

        [Column("DocumentTypeId")]
        public int DocumentTypeId { get; set; }

        [Column("UserNickName")]
        public string UserNickName { get; set; }

        [Column("UserDocumentId")]
        public string UserDocumentId { get; set; }

        [Column("UserName")]
        public string UserName { get; set; }

        [Column("UserLastName")]
        public string UserLastName { get; set; }

        [Column("UserEmail")]
        public string UserEmail { get; set; }

        [Column("UserPw")]
        public string UserPw { get; set; }

        [Column("UserDateCreation")]
        public DateTime? UserDateCreation { get; set; }

        [Column("UserFullName")]
        [ReadOnly(true)]
        public string UserFullName { get; set; }

        public DocumentType DocumentType { get; set; }
    }
}