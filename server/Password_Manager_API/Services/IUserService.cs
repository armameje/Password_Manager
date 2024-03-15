using Password_Manager_API.Model;

namespace Password_Manager_API.Services
{
    public interface IUserService
    {
        Task Register(UserLogin user);
        Task Login(UserLogin user);
    }
}
