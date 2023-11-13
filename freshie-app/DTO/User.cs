using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace freshie_app.DTO
{
    public class User
    {
        public int UserId { get; set; }

        public string Password { get; set; } = null!;

        public string Name { get; set; } = null!;

        public string? Email { get; set; }

        public string? Salt { get; set; }

        //public virtual ICollection<FridgeItem> FridgeItems { get; set; } = new List<FridgeItem>();

        //public virtual ICollection<GroceriesHistory> GroceriesHistories { get; set; } = new List<GroceriesHistory>();

        //public virtual ICollection<GroceriesList> GroceriesLists { get; set; } = new List<GroceriesList>();
    }

}
