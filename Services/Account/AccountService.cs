using MyPasswords.DTOs;
using MyPasswords.Models;
using MyPasswords.Repositories.Interfaces;
using MyPasswords.Security;

namespace MyPasswords.Services.Account
{
    public class AccountService : IAccountService
    {
        private readonly IUserRepository _userRepository;
        private readonly PasswordHashService _passwordHashService;

        public AccountService(IUserRepository userRepository, PasswordHashService passwordHashService)
        {
            _userRepository = userRepository;
            _passwordHashService = passwordHashService;
        }

        public async Task<LoginDTO?> Login(LoginViewModel model)
        {
            var user = await _userRepository.GetByEmail(model.Email);
            if (user is null) return null;

            if(!_passwordHashService.VerifyPassword(model.Password, user.Password))
                return null;

            return new LoginDTO
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email
            };
        }
    }
}
