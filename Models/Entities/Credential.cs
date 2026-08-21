namespace MyPasswords.Models.Entities
{
    public class Credential
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string ServiceName { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.Now;

    }
}
