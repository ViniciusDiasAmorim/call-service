using CallServiceFlow.Context;
using CallServiceFlow.Dto;
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

        public async Task<(bool, string)> CreateCustomer(CustomerDto customerDto)
        {
            var customer = new Customer()
            {
                Name = customerDto.Name,
                Email = customerDto.Email,
                CreationDate = DateTime.Now
            };

            try
            {
                await _context.Customers.AddAsync(customer);
                await _context.SaveChangesAsync();

                return (true, "Cliente criado com sucesso");
            }
            catch (Exception ex) {
                return (false, $"Erro ao criar cliente: {ex.Message}");
            }

        }
    }
}
