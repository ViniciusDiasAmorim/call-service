using CallServiceFlow.Dto.CustomerDTO;
using CallServiceFlow.Model;

namespace CallServiceFlow.Repository.Interfaces
{
    public interface ICustomerRepository : IRepository<Customer>
    {
        Task<(bool ok, string message, CustomerResponseDto responseDto)> CreateCustomer(CustomerDto customer);
    }
}
