using System.ComponentModel.DataAnnotations;

namespace MyPasswords.Models
{
    public class UserEditViewModel
    {
        [Required(ErrorMessage = "Name is required.")]
        public string Name { get; set; } = string.Empty;

        [DataType(DataType.Password)]
        public string? Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Passwords do not match.")]
        public string? ConfirmPassword { get; set; }
    }
}
