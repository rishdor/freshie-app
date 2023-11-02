using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace freshie_app.Data
{
    public class Product
    {
        public string Name { get; set; }
        public int CategoryId { get; set; }
        public int ShelfId { get; set; }
        [Key]
        public int Id { get; set; }
    }
}
