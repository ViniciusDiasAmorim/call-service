using CallServiceFlow.Model;

namespace CallServiceFlow.Repository.Interfaces
{
    public interface ICustomerRepository : IRepository<Customer>
    {
        Task CreateCustomerAsync(Customer customer);
    }
}
