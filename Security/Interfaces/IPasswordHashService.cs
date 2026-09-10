namespace MyPasswords.Security.Interfaces
{
    public interface IPasswordHashService
    {
        string HashPassword(string password);
        bool VerifyPassword(string password, string hash);
    }
}
