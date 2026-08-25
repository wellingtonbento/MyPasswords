using Microsoft.EntityFrameworkCore;
using MyPasswords.Data;
using MyPasswords.Models.Entities;
using MyPasswords.Repositories.Interfaces;

namespace MyPasswords.Repositories
{
    public class CredentialRepository : ICredentialRepository
    {
        private readonly AppDbContext _context;

        public CredentialRepository(AppDbContext context) => _context = context;

        public async Task Add(Credential credential)
        {
            await _context.Credentials.AddAsync(credential);
        }

        public void Delete(Credential credential)
        {
            _context.Credentials.Remove(credential);
        }

        public async Task<IList<Credential>> GetAllByUserId(int userId)
        {
            return await _context.Credentials
                .Where(c => c.UserId == userId)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Credential?> GetById(int id)
        {
            return await _context.Credentials.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }

        public void Update(Credential credential)
        {
            _context.Credentials.Update(credential);
        }
    }
}
