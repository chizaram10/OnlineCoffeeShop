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

        public async Task<ResponseDTO<List<OrderItem>>> GetOrderItemsByOrderId(string orderId)
        {
            try
            {
                var orderItems = await _orderItemRepository.GetOrderItemsByOrderId(orderId);

                if (orderItems == null || orderItems.Count < 1)
                {
                    return ResponseDTO<List<OrderItem>>.Fail(new string[] { "No item was found for this order." });
                }

                return ResponseDTO<List<OrderItem>>.Success(orderItems);
            }
            catch
            {
                return ResponseDTO<List<OrderItem>>.Fail(new string[] { "No item was found for this order." }); ;
            }
        }
    }

}
