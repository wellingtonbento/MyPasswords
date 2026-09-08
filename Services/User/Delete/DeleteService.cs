using MyPasswords.Repositories.Interfaces;
using MyPasswords.Services.ObtainUserLogged;

namespace MyPasswords.Services.User.Delete
{
    public class DeleteService : IDeleteService
    {
        private readonly IUserRepository _userRepository;
        private readonly ILoggedUser _loggedUser;

        public DeleteService(IUserRepository userRepository, ILoggedUser loggedUser)
        {
            _userRepository = userRepository;
            _loggedUser = loggedUser;
        }

        public async Task<bool> Delete()
        {
            var user = await _userRepository.GetById(_loggedUser.Id);
            if (user is null) return false;

            _userRepository.Delete(user);
            await _userRepository.Save();
            return true;
        }
    }
}
