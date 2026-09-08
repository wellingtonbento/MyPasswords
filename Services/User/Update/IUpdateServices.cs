using MyPasswords.Models;

namespace MyPasswords.Services.User.Update
{
    public interface IUpdateServices
    {
        public Task<bool> Update(UserEditViewModel model);
    }
}
