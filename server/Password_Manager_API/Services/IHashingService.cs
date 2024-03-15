namespace Password_Manager_API.Services
{
    public interface IHashingService
    {
        string HashPassword(string password);
        bool IsVerifiedUser(string password, string storedPassword);
    }
}
