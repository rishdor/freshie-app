using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace freshie_app.DTO
{
    public class Shelf
    {
        public int ShelfId { get; set; }

        public string ShelfName { get; set; } = null!;

        public virtual ICollection<Product> Products { get; set; } = new List<Product>();

    }
}
