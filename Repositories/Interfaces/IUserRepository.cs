using MyPasswords.Models.Entities;

namespace MyPasswords.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<User?> GetByEmail(string email);
        Task<bool> ExistUserWithEmail(string email);
        Task<User?> GetById(int id);
        Task Add(User user);
        void Update(User user);
        void Delete(User user);
        Task Save();
    }
}
