using MyPasswords.Models;

namespace MyPasswords.Services.Credential.Register
{
    public interface IRegisterCredentialServices
    {
        Task<bool> Register(RegisterCredentialViewModel model);
    }
}
