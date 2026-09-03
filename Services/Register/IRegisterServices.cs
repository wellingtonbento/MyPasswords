using MyPasswords.Models;

namespace MyPasswords.Services.Register
{
    public interface IRegisterServices
    {
        public Task<bool> Register(RegisterViewModel model);
    }
}
