using OnlineCoffeeShop.Domain.Enums;

namespace OnlineCoffeeShop.Domain.Models
{
    public class MenuItem
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string ImageUrl { get; set; } = string.Empty;
        public MenuItemCategory Category { get; set; }
    }
}
