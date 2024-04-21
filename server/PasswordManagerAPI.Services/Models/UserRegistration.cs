using System.ComponentModel.DataAnnotations;

namespace PasswordManagerAPI.Services.Models
{
    public class UserRegistration
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
