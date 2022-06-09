using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Estetika.Models
{
    public class IdentityModel
    {
        public string Login { get; set; }
        public string Password { get; set; }
    }
    public class LoginModel : IdentityModel { }

    public class RegisterModel : IdentityModel
    {
        public string Email { get; set; }
    }
}