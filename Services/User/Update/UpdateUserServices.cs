using MyPasswords.Models;
using MyPasswords.Repositories.Interfaces;
using MyPasswords.Security;
using MyPasswords.Security.Interfaces;
using MyPasswords.Services.ObtainUserLogged;

namespace MyPasswords.Services.User.Update
{
    public class UpdateUserServices : IUpdateUserServices
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHashService _passwordHashService;
        private readonly ILoggedUser _loggedUser;

        public UpdateUserServices(IUserRepository userRepository, IPasswordHashService passwordHashService, ILoggedUser loggedUser)
        {
            _userRepository = userRepository;
            _passwordHashService = passwordHashService;
            _loggedUser = loggedUser;
        }

        public async Task<bool> Update(UserEditViewModel model)
        {
            var user = await _userRepository.GetById(_loggedUser.Id);
            if (user is null) return false;

            user.Name = model.Name;

            if (!string.IsNullOrWhiteSpace(model.Password))
                user.Password = _passwordHashService.HashPassword(model.Password);

            _userRepository.Update(user);
            await _userRepository.Save();
            return true;
        }
    }
}
