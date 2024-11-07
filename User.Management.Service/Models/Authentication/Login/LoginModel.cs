using System.ComponentModel.DataAnnotations;

namespace User.Management.Service.Models.Authentication.Login
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Username Is Required")]
        public string? Username { get; set; }

        [Required(ErrorMessage = "Password Is Required")]
        public string? Password { get; set; }
    }
}

