using OnlineCoffeeShop.Core.DTOs;

namespace OnlineCoffeeShop.UI.Models
{
    public class TodaysOrdersViewModel
    {
        public IEnumerable<OrderDTO> Orders { get; set; } = new List<OrderDTO>();
    }
}
