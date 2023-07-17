using OnlineCoffeeShop.Core.DTOs;
using OnlineCoffeeShop.Domain.Models;

namespace OnlineCoffeeShop.UI.Models
{
    public class TodaysOrdersViewModel
    {
        public IEnumerable<OrderDTO> Orders { get; set; } = new List<OrderDTO>();
    }
}
