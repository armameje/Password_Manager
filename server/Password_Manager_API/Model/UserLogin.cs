using System.ComponentModel.DataAnnotations;

namespace Password_Manager_API.Model
{
    public class UserLogin
    {
        [Required]
        public string Username { get; set; }
        [Required] 
        public string Password { get; set; }
    }
}
