using Microsoft.EntityFrameworkCore;
using OnlineCoffeeShop.Core.Interfaces;
using OnlineCoffeeShop.Domain.Models;

namespace OnlineCoffeeShop.Infrastructure.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly OnlineCoffeeShopDBContext _dbContext;

        public CustomerRepository(OnlineCoffeeShopDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task CreateCustomer(Customer customer)
        {
            await _dbContext.Customers.AddAsync(customer);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<Customer> GetCustomerByEmail(string email)
        {
            var customer = await _dbContext.Customers.FirstOrDefaultAsync(c => email == c.Email);

            return customer!;
        }
    }

}
