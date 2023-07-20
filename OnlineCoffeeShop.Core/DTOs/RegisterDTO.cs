using OnlineCoffeeShop.Core.Utilities;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace OnlineCoffeeShop.Core.DTOs
{
    public class RegisterDTO
    {
        [Required(ErrorMessage = "Required."), MaxLength(50,
            ErrorMessage = "First name cannot exceed 50 characters.")]
        public string FirstName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Required."), MaxLength(50,
            ErrorMessage = "Last name cannot exceed 50 characters.")]
        public string LastName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Required."),
            EmailAddress(ErrorMessage = "Invalid email address."), MaxLength(50,
            ErrorMessage = "Email address cannot exceed 50 characters.")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Required."), Phone(ErrorMessage = "Invalid phone number.")]
        public string PhoneNumber { get; set; } = string.Empty;

        [Required(ErrorMessage = "Required."), PasswordPropertyText, PasswordValidation(ErrorMessage
            = "Password must have at least 8 characters, alphanumeric keys, and a special character")]
        public string Password { get; set; } = string.Empty;

        [Required(ErrorMessage = "Required."), Compare("Password", ErrorMessage = "Passwords do not match.")]
        public string ConfirmPassword { get; set; } = string.Empty;
    }
}
