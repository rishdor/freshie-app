namespace freshiehubAPI.Models
{
    public class AddProductModel
    {
        public int UserId { get; set; }
        public Product? Product { get; set; }
        public DateOnly? ExpirationDate { get; set; }
    }

}
