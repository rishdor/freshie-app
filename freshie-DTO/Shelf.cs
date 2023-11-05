using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace freshie_DTO
{
    public class Shelf
    {
        [Key]
        [Column("shelf_id")]
        public int ShellfId { get; set; }
        [Column("shelf_name")]
        public string Name { get; set; }
    }
}
