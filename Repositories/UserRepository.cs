using Microsoft.EntityFrameworkCore;
using MyPasswords.Data;
using MyPasswords.Models.Entities;
using MyPasswords.Repositories.Interfaces;

namespace MyPasswords.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context) => _context = context;

        public async Task Add(User user)
        {
            await _context.Users.AddAsync(user);
        }

        public void Delete(User user)
        {
            _context.Users.Remove(user);
        }

        public async Task<User?> GetByEmail(string email)
        {
            return await _context.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<bool> ExistUserWithEmail(string email)
        { 
            return await _context.Users.AsNoTracking().AnyAsync(user => user.Email.Equals(email)); 
        }

        public async Task<User?> GetById(int id)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }

        public void Update(User user)
        {
            _context.Users.Update(user);
        }
    }
}
