using MyPasswords.Security.Interfaces;

namespace MyPasswords.Security
{
    public class PasswordHashService : IPasswordHashService
    {
        public string HashPassword(string password) => BCrypt.Net.BCrypt.HashPassword(password);
        public bool VerifyPassword(string password, string hash) => BCrypt.Net.BCrypt.Verify(password, hash);
    }
}
