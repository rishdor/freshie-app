using System.ComponentModel.DataAnnotations;

namespace freshie_DTO
{
    public class User
    {
        [Key]
        public int user_id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        //public string Password { get; set; }
    }
}