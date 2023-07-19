using OnlineCoffeeShop.Core.DTOs;

namespace OnlineCoffeeShop.Core.Interfaces
{
    public interface ICustomerService
    {
        Task<ResponseDTO<CustomerDTO>> CreateCustomer(RegisterDTO registerDTO);
        Task<ResponseDTO<CustomerDTO>> Login(LoginDTO loginDTO);
        Task<ResponseDTO<CustomerDTO>> GetCustomerByEmail(string email);
    }
}