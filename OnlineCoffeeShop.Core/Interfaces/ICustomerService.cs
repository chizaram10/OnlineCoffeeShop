using OnlineCoffeeShop.Core.DTOs;

namespace OnlineCoffeeShop.Core.Interfaces
{
    public interface ICustomerService
    {
        Task<CustomerDTO> CreateCustomer(RegisterDTO customerDTO);
        Task<CustomerDTO> Login(LoginDTO loginDTO);
        Task<CustomerDTO> GetCustomerByEmail(string email);
    }
}