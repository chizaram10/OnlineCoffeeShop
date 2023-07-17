using OnlineCoffeeShop.Domain.Models;

namespace OnlineCoffeeShop.Core.Interfaces
{
    public interface ICustomerRepository
    {
        Task CreateCustomer(Customer customer);
        Task<Customer> GetCustomerByEmail(string email);
    }
}