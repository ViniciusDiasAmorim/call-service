using CallServiceFlow.Dto;
using CallServiceFlow.Model;

namespace CallServiceFlow.Repository.Interfaces
{
    public interface ICustomerRepository : IRepository<Customer>
    {
        Task<(bool ok, string message)> CreateCustomer(CustomerDto customer);
    }
}
