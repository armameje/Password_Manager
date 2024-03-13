namespace Password_Manager_API.Repository
{
    public interface IUserRepository
    {
        Task RegisterUserAsync(string username, string password);
        Task RetrieveUserAsync(string username);
    }
}
