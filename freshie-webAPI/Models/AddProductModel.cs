namespace freshie_webAPI.Models
{
    public class AddProductModel
    {
        public int UserId { get; set; }
        public Product Product { get; set; }
        public DateTime? ExpirationDate { get; set; }
    }

}
