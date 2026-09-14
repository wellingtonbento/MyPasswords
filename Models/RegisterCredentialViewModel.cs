using System.ComponentModel.DataAnnotations;

namespace MyPasswords.Models
{
    public class RegisterCredentialViewModel
    {
        [Required(ErrorMessage = "Service Name is required.")]
        public string ServiceName { get; set; } = string.Empty;
        
        [Required(ErrorMessage = "User Name is required.")]
        [MaxLength(100, ErrorMessage = "User name must be at most 100 characters.")]
        public string UserName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Password is required.")]
        public string Password { get; set; } = string.Empty;
    }
}
