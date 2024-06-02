namespace PasswordManagerAPI.Services.Models
{
    public class RegistrationResponse
    {
        public bool IsSuccess { get; set; }
        public string Token { get; set; } = string.Empty;
        public string Error { get; set; } = string.Empty;
    }
}
