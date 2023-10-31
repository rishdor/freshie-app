using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace freshie_app.Data
{
    public class FridgeItem
    {
        public int UserId { get; set; }
        public int ProductId { get; set; }
        public DateTime ExpirationDate { get; set; }
        public static int currentId = 1;
        [Key]
        public int Id { get; private set; } = currentId++;
    }
}
