using MyPasswords.Models;

namespace MyPasswords.Services.User.Register
{
    public interface IRegisterUserServices
    {
        public Task<bool> Register(RegisterViewModel model);
    }
}
