using MyPasswords.Models.Entities;

namespace MyPasswords.Repositories.Interfaces
{
    public interface ICredentialRepository
    {
        Task<IList<Credential>> GetAllByUserId(int userId);
        Task<Credential?> GetById(int id);
        Task Add(Credential credential);
        void Update(Credential credential);
        void Delete(Credential credential);
        Task Save();
    }
}
