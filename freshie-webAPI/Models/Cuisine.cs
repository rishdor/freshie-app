using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace freshie_webAPI.Models
{
    [Table("Cuisine")]
    public class Cuisine
    {
        [Key]
        [Column("cuisine_id")]
        public int CuisineId { get; set; }
        [Column("cuisine_name")]
        public string CuisineName { get; set; } = null!;
    }
}
