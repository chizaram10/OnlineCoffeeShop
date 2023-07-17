using OnlineCoffeeShop.Domain.Models;

namespace OnlineCoffeeShop.UI.Models
{
    public class HomeViewModel
    {
        public IEnumerable<MenuItem> MenuItems { get; set; } = new List<MenuItem>();
    }
}
