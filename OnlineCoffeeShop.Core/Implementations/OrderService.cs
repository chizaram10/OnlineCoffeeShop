using OnlineCoffeeShop.Core.DTOs;
using OnlineCoffeeShop.Core.Interfaces;
using OnlineCoffeeShop.Domain.Models;
using System.Collections.Generic;

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

        public async Task<ResponseDTO<OrderDTO>> CreateOrder(ShoppingCart shoppingCart, decimal amount,
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
                return ResponseDTO<OrderDTO>.Success(
                    new OrderDTO
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
                    });
            }
            catch
            {
                return ResponseDTO<OrderDTO>.Fail(new string[] { "Unable to create order." });
            }
        }

        public async Task<ResponseDTO<List<OrderDTO>>> GetOrdersByDate(DateTime date)
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

                return ResponseDTO<List<OrderDTO>>.Success(result);
            }
            catch
            {
                return ResponseDTO<List<OrderDTO>>.Fail(new string[] { "Unable to get orders for this date." });
            }
        }

        public async Task<ResponseDTO<OrderDTO>> CancelOrder(string id)
        {
            try
            {
                var order = await _orderRepository.GetOrderById(id);

                if (order == null)
                {
                    return ResponseDTO<OrderDTO>.Fail(new string[] { "Unable to find this order." });
                }

                await _orderRepository.DeleteOrder(order);
                return ResponseDTO<OrderDTO>.Success(
                    new OrderDTO
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
                    });
            }
            catch
            {
                return ResponseDTO<OrderDTO>.Fail(new string[] { "Unable to find this order." });
            }            
        }
    }
}
