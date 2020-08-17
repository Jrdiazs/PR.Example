using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PR.Models
{
    [Table("Menus")]
    public class Menus
    {
        [Key]
        [Column("MenuId")]
        public int MenuId { get; set; }

        [Column("MenuName")]
        public string MenuName { get; set; }

        [Column("MenuUrl")]
        public string MenuUrl { get; set; }

        [Column("MenuOrder")]
        public int MenuOrder { get; set; }

        [Column("MenuParentId")]
        public int? MenuParentId { get; set; }

        [Column("IsMenuParent")]
        [ReadOnly(true)]
        public bool? IsMenuParent { get; set; }

        [NotMapped]
        public Menus MenuParent { get; set; }
    }
}