using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace OnlineCoffeeShop.Core.Utilities
{
    public class PasswordValidationAttribute : ValidationAttribute
    {
        public override bool IsValid(object? value)
        {
            var password = value as string;

            if (string.IsNullOrEmpty(password))
            {
                return false;
            }

            // Password must have at least 8 characters
            if (password.Length < 8)
            {
                return false;
            }

            // Password must contain at least one alphanumeric character
            if (!Regex.IsMatch(password, @"\w"))
            {
                return false;
            }

            // Password must contain at least one special character
            if (!Regex.IsMatch(password, @"[!@#$%^&*()_+=\[{\]};:<>|./?,-]"))
            {
                return false;
            }

            return true;
        }
    }
}
