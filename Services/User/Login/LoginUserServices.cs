using MyPasswords.DTOs;
using MyPasswords.Models;
using MyPasswords.Repositories.Interfaces;
using MyPasswords.Security;
using MyPasswords.Security.Interfaces;

namespace MyPasswords.Services.User.Login
{
    public class LoginUserServices : ILoginUserServices
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHashService _passwordHashService;

        public LoginUserServices(IUserRepository userRepository, IPasswordHashService passwordHashService)
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
                Name = user.Name,
                Email = user.Email
            };
        }
    }
}
