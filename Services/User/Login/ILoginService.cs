using MyPasswords.DTOs;
using MyPasswords.Models;

namespace MyPasswords.Services.User.Login
{
    public interface ILoginService
    {
        Task<LoginDTO?> Login(LoginViewModel model);
    }
}
