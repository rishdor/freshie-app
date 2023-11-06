using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace freshie_DTO
{
    public class Product
    {
        [Key]
        [Column("product_id")]
        public int Id { get; set; }
        [Column("product_name")]
        public string Name { get; set; }
        [Column("category_id")]
        public int CategoryId { get; set; }     
    }
}
