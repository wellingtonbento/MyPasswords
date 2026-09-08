using MyPasswords.Models;

namespace MyPasswords.Services.User.Update
{
    public interface IUpdateUserServices
    {
        public Task<bool> Update(UserEditViewModel model);
    }
}
