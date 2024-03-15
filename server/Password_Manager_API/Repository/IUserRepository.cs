using Password_Manager_API.Model;

namespace Password_Manager_API.Repository
{
    public interface IUserRepository
    {
        Task RegisterUserAsync(string username, string password);
        Task<UserInfo> RetrieveUserAsync(string username);
    }
}
