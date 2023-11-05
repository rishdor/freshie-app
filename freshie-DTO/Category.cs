using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace freshie_DTO
{
    public class Category
    {
        [Key]
        [Column("category_id")]
        public int CategoryId { get; set; }
        [Column("category_name")]
        public string Name { get; set; }

    }
}
