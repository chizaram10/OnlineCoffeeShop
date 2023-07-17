using OnlineCoffeeShop.Core.DTOs;
using OnlineCoffeeShop.Core.Interfaces;
using OnlineCoffeeShop.Core.Utilities;
using OnlineCoffeeShop.Domain.Models;

namespace OnlineCoffeeShop.Core.Implementations
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerService(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<CustomerDTO> CreateCustomer(RegisterDTO customerDTO)
        {
            try
            {
                var existingUser = await _customerRepository.GetCustomerByEmail(customerDTO.Email!);

                if (existingUser != null)
                    return null!;

                Customer customer = new Customer()
                {
                    PhoneNumber = customerDTO.PhoneNumber,
                    Email = customerDTO.Email,
                    FirstName = customerDTO.FirstName,
                    LastName = customerDTO.LastName,
                    PasswordHash = PasswordHashAndVerification.HashPassword(customerDTO.Password),
                    UserName = customerDTO.Email,
                    NormalizedUserName = customerDTO.FirstName,
                };

                await _customerRepository.CreateCustomer(customer);

                return new CustomerDTO
                {
                    FirstName = customer.FirstName,
                    LastName = customer.LastName,
                    PhoneNumber = customer.PhoneNumber,
                    Email = customer.Email,
                };
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        public async Task<CustomerDTO> Login(LoginDTO loginDTO)
        {
            try
            {
                CustomerDTO customerDTO = null!;

                var customer = await _customerRepository.GetCustomerByEmail(loginDTO.Email);

                if (customer == null)
                {
                    return customerDTO!;
                }

                var isPasswordValid = PasswordHashAndVerification.VerifyPassword(loginDTO.Password,
                    customer.PasswordHash);

                if (isPasswordValid)
                {
                    return new CustomerDTO
                    {
                        FirstName = customer.FirstName,
                        LastName = customer.LastName,
                        PhoneNumber = customer.PhoneNumber,
                        Email = customer.Email,
                    };
                }

                return customerDTO;
            }
            catch (Exception ex)
            {
                throw new Exception("Invalid ligin credentials.", ex);
            }
        }

        public async Task<CustomerDTO> GetCustomerByEmail(string email)
        {
            try
            {
                Customer customer = await _customerRepository.GetCustomerByEmail(email);

                if (customer != null)
                {
                    return new CustomerDTO
                    {
                        Email = customer.Email,
                        PhoneNumber = customer.PhoneNumber,
                        FirstName = customer.FirstName,
                        LastName = customer.LastName,
                    };
                }

                return null!;
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex.Message}", ex);
            }
        }
    }
}
