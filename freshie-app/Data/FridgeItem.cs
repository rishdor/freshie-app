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
        private static MyDbContext _context = MyDbContext.Instance;
        public int UserId { get; set; }
        public int ProductId { get; set; }
        public DateTime ExpirationDate { get; set; }
        [Key]
        public int Id { get; set; }

        public override string ToString()
        {
            var product = _context.Products.FirstOrDefault(p => p.Id == ProductId);
            return product != null ? product.Name : string.Empty;
        }
    }
}
