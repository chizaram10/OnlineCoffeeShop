using OnlineCoffeeShop.Core.DTOs;
using OnlineCoffeeShop.Core.Interfaces;
using OnlineCoffeeShop.Domain.Models;

namespace OnlineCoffeeShop.Core.Implementations
{
    public class OrderItemService : IOrderItemService
    {
        private readonly IOrderItemRepository _orderItemRepository;

        public OrderItemService(IOrderItemRepository orderItemRepository)
        {
            _orderItemRepository = orderItemRepository;
        }

        public async Task<List<OrderItem>> GetOrderItemsByOrderId(string orderId)
        {
            try
            {
                var orderItems = await _orderItemRepository.GetOrderItemsByOrderId(orderId);
                return orderItems;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }
    }

}
