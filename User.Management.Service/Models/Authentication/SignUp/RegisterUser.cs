using System.ComponentModel.DataAnnotations;

namespace User.Management.Service.Models.Authentication.SignUp
{
    public class RegisterUser
    {
        [Required(ErrorMessage ="Username Is Required")]
        public string? Username { get; set; }
        [EmailAddress]
        [Required(ErrorMessage ="Email Is Required")]
        public string?Email { get; set; }
        [Required(ErrorMessage ="Password Is Required")]
        public string? Password { get; set; }
        [Required(ErrorMessage = "Password Is Required")]
        public List<string> Roles { get; set; }
    }
}
