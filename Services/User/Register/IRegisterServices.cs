using MyPasswords.Models;

namespace MyPasswords.Services.User.Register
{
    public interface IRegisterServices
    {
        public Task<bool> Register(RegisterViewModel model);
    }
}
