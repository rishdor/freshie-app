namespace freshiehubAPI.Models
{
    public class UserUpdateModel
    {
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
    }

}
