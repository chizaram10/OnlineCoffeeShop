using Microsoft.AspNetCore.Identity;

namespace OnlineCoffeeShop.Domain.Models
{
    public class Customer : IdentityUser
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
    }
}