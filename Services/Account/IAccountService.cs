using MyPasswords.DTOs;
using MyPasswords.Models;

namespace MyPasswords.Services.Account
{
    public interface IAccountService
    {
        Task<LoginDTO?> Login(LoginViewModel model);
    }
}
