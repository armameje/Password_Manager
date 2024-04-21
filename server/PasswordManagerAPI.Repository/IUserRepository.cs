using PasswordManagerAPI.Repository.Model;

namespace PasswordManagerAPI.Repository
{
    public interface IUserRepository
    {
        Task RegisterUserAsync(string username, string password, string salt, int numberOfRounds);
        Task<StoredUserAccount> RetrieveUserByUsernameAsync(string username);
        Task<bool> IsUsernameTakenAsync(string username);
    }
}
