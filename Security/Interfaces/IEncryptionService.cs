namespace MyPasswords.Security.Interfaces
{
    public interface IEncryptionService
    {
        string Encrypt(string text);
        string Decrypt(string encryptedText);
    }
}
