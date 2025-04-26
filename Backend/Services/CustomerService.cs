using CallServiceFlow.Dto.CustomerDTO;
using CallServiceFlow.Model;
using CallServiceFlow.Repository.Interfaces;
using CallServiceFlow.Services.Interfaces;

namespace CallServiceFlow.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CustomerService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<(bool ok, string message, CustomerResponseDto responseDto)> CreateCustomer(CustomerDto customerDto)
        {
            var customer = new Customer()
            {
                Name = customerDto.Name,
                Email = customerDto.Email,
                CreationDate = DateTime.Now
            };

            try
            {
                await _unitOfWork.CustomerRepository.AddAsync(customer);
                await _unitOfWork.Commit();

                var responseDto = new CustomerResponseDto()
                {
                    Id = customer.Id,
                    Name = customer.Name,
                    Email = customer.Email
                };

                return (true, "Cliente criado com sucesso", responseDto);
            }
            catch (Exception ex)
            {
                return (false, $"Erro ao criar cliente: {ex.Message}", null);
            }
        }

        public async Task<(bool ok, string message)> DeleteCustomer(int id)
        {
            var customer = await _unitOfWork.CustomerRepository.GetByIdAsync(id);

            if (customer == null)
                return (false, "Cliente não encontrado");

            customer.IsActive = false;

            await _unitOfWork.CustomerRepository.UpdateAsync(customer);
            await _unitOfWork.Commit();

            return (true, "Cliente excluído com sucesso");

        }
    }
}
