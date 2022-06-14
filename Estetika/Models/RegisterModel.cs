namespace Estetika.Models
{
    public class RegisterModel : IdentityModel
    {
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
    }
}