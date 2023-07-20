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

        public async Task<ResponseDTO<CustomerDTO>> CreateCustomer(RegisterDTO registerDTO)
        {
            try
            {
                var existingUser = await _customerRepository.GetCustomerByEmail(registerDTO.Email!);

                if (existingUser != null)
                    return ResponseDTO<CustomerDTO>.Fail(new string[]
                    {
                        $"Customer with email {registerDTO.Email} already exists"
                    });

                Customer customer = new Customer()
                {
                    PhoneNumber = registerDTO.PhoneNumber,
                    Email = registerDTO.Email,
                    FirstName = registerDTO.FirstName,
                    LastName = registerDTO.LastName,
                    PasswordHash = PasswordHashAndVerification.HashPassword(registerDTO.Password),
                    UserName = registerDTO.Email,
                    NormalizedUserName = registerDTO.FirstName,
                };

                await _customerRepository.CreateCustomer(customer);

                return ResponseDTO<CustomerDTO>.Success(new CustomerDTO
                {
                    FirstName = customer.FirstName,
                    LastName = customer.LastName,
                    PhoneNumber = customer.PhoneNumber,
                    Email = customer.Email,
                });
            }
            catch
            {
                return ResponseDTO<CustomerDTO>.Fail(new string[]
                {
                    "Unable to register customer. Try later."
                });
            }
        }

        public async Task<ResponseDTO<CustomerDTO>> Login(LoginDTO loginDTO)
        {
            try
            {
                var customer = await _customerRepository.GetCustomerByEmail(loginDTO.Email);

                if (customer == null)
                {
                    return ResponseDTO<CustomerDTO>.Fail(new string[] { "Invalid ligin credentials." });
                }

                var isPasswordValid = PasswordHashAndVerification.VerifyPassword(loginDTO.Password,
                    customer.PasswordHash);

                if (isPasswordValid)
                {
                    return ResponseDTO<CustomerDTO>.Success(new CustomerDTO
                    {
                        FirstName = customer.FirstName,
                        LastName = customer.LastName,
                        PhoneNumber = customer.PhoneNumber,
                        Email = customer.Email,
                    });
                }

                return ResponseDTO<CustomerDTO>.Fail(new string[] { "Invalid ligin credentials." });
            }
            catch
            {
                return ResponseDTO<CustomerDTO>.Fail(new string[] { "Invalid ligin credentials." });
            }
        }

        public async Task<ResponseDTO<CustomerDTO>> GetCustomerByEmail(string email)
        {
            try
            {
                Customer customer = await _customerRepository.GetCustomerByEmail(email);

                if (customer != null)
                {
                    return ResponseDTO<CustomerDTO>.Success(new CustomerDTO
                    {
                        FirstName = customer.FirstName,
                        LastName = customer.LastName,
                        PhoneNumber = customer.PhoneNumber,
                        Email = customer.Email,
                    });
                }

                return ResponseDTO<CustomerDTO>.Fail(new string[] { $"Customer with this email {email} not found." });
            }
            catch
            {
                return ResponseDTO<CustomerDTO>.Fail(new string[] { $"Customer with this email {email} not found." });
            }
        }
    }
}
