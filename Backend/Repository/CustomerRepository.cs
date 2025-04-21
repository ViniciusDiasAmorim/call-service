using CallServiceFlow.Context;
using CallServiceFlow.Model;
using CallServiceFlow.Repository.Interfaces;

namespace CallServiceFlow.Repository
{
    public class CustomerRepository : Repository<Customer>, ICustomerRepository
    {
        private readonly AppDbContext _context;
        public CustomerRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task CreateCustomerAsync(Customer customer)
        {
            await _context.AddAsync(customer);
        }
    }
}
