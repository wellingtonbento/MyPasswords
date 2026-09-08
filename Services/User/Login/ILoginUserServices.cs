using MyPasswords.DTOs;
using MyPasswords.Models;

namespace MyPasswords.Services.User.Login
{
    public interface ILoginUserServices
    {
        Task<LoginDTO?> Login(LoginViewModel model);
    }
}
