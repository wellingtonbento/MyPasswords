using System.Security.Cryptography;
using System.Text;

namespace MyPasswords.Services
{
    public class EncryptionService
    {
        private readonly byte[] _key;
        public EncryptionService(IConfiguration configuration)
        {
            string keyString = configuration["EncryptionKey"];
            _key = Encoding.UTF8.GetBytes(keyString);
        }

        public string Encrypt(string text)
        {
            using var aes = Aes.Create();
            aes.Key = _key;

            using var encryptor = aes.CreateEncryptor(aes.Key, aes.IV);
            using var memorySream = new MemoryStream();
            using (var cryptoStream = new CryptoStream(memorySream, encryptor, CryptoStreamMode.Write))
            using (var streamWriter = new StreamWriter(cryptoStream))
            {
                streamWriter.Write(text);
            }

            byte[] iv = aes.IV;
            byte[] ciphertext = memorySream.ToArray();
            byte[] result = iv.Concat(ciphertext).ToArray();

            return Convert.ToBase64String(result);
        }

        public string Decrypt(string encryptedText)
        {
            byte[] bytes = Convert.FromBase64String(encryptedText);

            byte[] iv = bytes.Take(16).ToArray();           
            byte[] ciphertext = bytes.Skip(16).ToArray();

            using var aes = Aes.Create();
            aes.Key = _key;
            aes.IV = iv;

            using var decryptor = aes.CreateDecryptor(aes.Key, aes.IV);
            using var memoryStream = new MemoryStream(ciphertext);
            using var cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read);
            using var streamWriter = new StreamReader(cryptoStream);

            return streamWriter.ReadToEnd();
        }
    }
}
