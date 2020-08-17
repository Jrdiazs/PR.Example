using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PR.Models
{
    [Table("DocumentType")]
    public class DocumentType
    {
        [Key]
        [Column("DocumentTypeId")]
        public int DocumentTypeId { get; set; }

        [Column("DocumentDescription")]
        public string DocumentDescription { get; set; }

        [Column("DocumentAbbreviation")]
        public string DocumentAbbreviation { get; set; }
    }
}