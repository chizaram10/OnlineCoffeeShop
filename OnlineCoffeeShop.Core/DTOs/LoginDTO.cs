using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace OnlineCoffeeShop.Core.DTOs
{
    public class LoginDTO
    {
        [Required(ErrorMessage = "Required")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Required"), DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;
    }
}
