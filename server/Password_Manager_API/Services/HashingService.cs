namespace Password_Manager_API.Services
{
    public class HashingService : IHashingService
    {
        public string HashPassword(string password) => BCrypt.Net.BCrypt.EnhancedHashPassword(password);
        public bool IsVerifiedUser(string password, string storedPassword) => BCrypt.Net.BCrypt.EnhancedVerify(password, storedPassword);
    }
}
