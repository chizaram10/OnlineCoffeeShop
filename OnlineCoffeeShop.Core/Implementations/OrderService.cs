using OnlineCoffeeShop.Core.DTOs;
using OnlineCoffeeShop.Core.Interfaces;
using OnlineCoffeeShop.Domain.Models;

namespace OnlineCoffeeShop.Core.Implementations
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ICustomerRepository _customerRepository;

        public OrderService(IOrderRepository orderRepository, 
            ICustomerRepository customerRepository)
        {
            _orderRepository = orderRepository;
            _customerRepository = customerRepository;
        }

        public async Task<OrderDTO> CreateOrder(ShoppingCart shoppingCart, decimal amount,
            string email)
        {
            try
            {
                var order = new Order
                {
                    OrderDate = DateTime.UtcNow,
                    OrderTotal = amount,
                    Customer = await _customerRepository.GetCustomerByEmail(email),
                };

                foreach (var item in shoppingCart.ShoppingCartItems)
                {
                    order.OrderItems.Add(new OrderItem
                    {
                        Quantity = item.Quantity,
                        MenuItem = item.MenuItem,
                        TotalPrice = item.MenuItem.Price * item.Quantity
                    });
                }

                await _orderRepository.CreateOrder(order);
                return new OrderDTO
                {
                    OrderDate = order.OrderDate,
                    OrderTotal = order.OrderTotal,
                    OrderNumber = order.OrderNumber,
                    Id = order.Id,
                    Customer = new CustomerDTO
                    {
                        Email = order.Customer.Email,
                        FirstName = order.Customer.FirstName,
                        LastName = order.Customer.LastName,
                        PhoneNumber = order.Customer.PhoneNumber
                    }
                };
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex.Message}", ex);
            }
        }

        public async Task<List<OrderDTO>> GetOrdersByDate(DateTime date)
        {
            try
            {
                var orders = await _orderRepository.GetOrdersByDate(date);
                var result = new List<OrderDTO>();

                foreach(var order in orders)
                {
                    result.Add(new OrderDTO 
                    { 
                        OrderDate = order.OrderDate, OrderTotal = order.OrderTotal,
                        OrderNumber = order.OrderNumber, Id = order.Id,
                        Customer = new CustomerDTO 
                        { 
                            Email = order.Customer.Email, FirstName = order.Customer.FirstName,
                            LastName = order.Customer.LastName, PhoneNumber = order.Customer.PhoneNumber
                        }
                    });
                }

                return result;
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex.Message}", ex);
            }
        }

        public async Task CancelOrder(string id)
        {
            var order = await _orderRepository.GetOrderById(id);
            await _orderRepository.DeleteOrder(order);
        }
    }
}
