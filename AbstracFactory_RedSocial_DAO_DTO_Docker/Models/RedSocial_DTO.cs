using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AbstracFactory_RedSocial_DAO_DTO_Docker.Models
{
    [Table("RedSocial")]
    public class RedSocial_DTO
    {
        [Key]
        public int id_RedSocial { get; set; }

        [Column("Name_User")]
        public string? Name { get; set; }

    }
}
