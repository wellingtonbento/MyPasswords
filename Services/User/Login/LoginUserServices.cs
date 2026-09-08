using MyPasswords.DTOs;
using MyPasswords.Models;
using MyPasswords.Repositories.Interfaces;
using MyPasswords.Security;

namespace MyPasswords.Services.User.Login
{
    public class LoginUserServices : ILoginUserServices
    {
        private readonly IUserRepository _userRepository;

        public LoginUserServices(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<LoginDTO?> Login(LoginViewModel model)
        {
            var user = await _userRepository.GetByEmail(model.Email);
            if (user is null) return null;

            if(!PasswordHashService.VerifyPassword(model.Password, user.Password))
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
