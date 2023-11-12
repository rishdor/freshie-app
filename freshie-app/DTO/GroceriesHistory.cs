using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace freshie_app.DTO
{
    public class GroceriesHistory
    {
        public int? ProductId { get; set; }

        public int? UserId { get; set; }

        public DateTime? Date { get; set; }

        public bool? Purchased { get; set; }

        public int Id { get; set; }

        public virtual Product? Product { get; set; }

        public virtual User? User { get; set; }
    }
}
