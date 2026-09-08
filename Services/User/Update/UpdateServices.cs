using MyPasswords.Models;
using MyPasswords.Repositories.Interfaces;
using MyPasswords.Security;
using MyPasswords.Services.ObtainUserLogged;

namespace MyPasswords.Services.User.Update
{
    public class UpdateServices : IUpdateServices
    {
        private readonly IUserRepository _userRepository;
        private readonly ILoggedUser _loggedUser;

        public UpdateServices(IUserRepository userRepository, ILoggedUser loggedUser)
        {
            _userRepository = userRepository;
            _loggedUser = loggedUser;
        }

        public async Task<bool> Update(UserEditViewModel model)
        {
            var user = await _userRepository.GetById(_loggedUser.Id);
            if (user is null) return false;

            user.Name = model.Name;

            if (!string.IsNullOrWhiteSpace(model.Password))
                user.Password = PasswordHashService.HashPassword(model.Password);

            _userRepository.Update(user);
            await _userRepository.Save();
            return true;
        }
    }
}
