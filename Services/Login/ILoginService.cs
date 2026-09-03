using MyPasswords.DTOs;
using MyPasswords.Models;

namespace MyPasswords.Services.Account
{
    public interface ILoginService
    {
        Task<LoginDTO?> Login(LoginViewModel model);
    }
}
