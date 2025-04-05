using CallServiceFlow.Context;
using CallServiceFlow.Model;
using CallServiceFlow.Repository.Interfaces;
using System.Threading.Tasks;

namespace CallServiceFlow.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        private IRepository<Ticket> _ticketRepository;
        private IRepository<Customer> _customerRepository;
        private IRepository<Technical> _technicalRepository;
        private IRepository<ApplicationUser> _applicationUserRepository;

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
        }

        public IRepository<Ticket> TicketRepository => _ticketRepository ??= new Repository<Ticket>(_context);
        public IRepository<Customer> CustomerRepository => _customerRepository ??= new Repository<Customer>(_context);
        public IRepository<Technical> TechnicalRepository => _technicalRepository ??= new Repository<Technical>(_context);
        public IRepository<ApplicationUser> ApplicationUserRepository => _applicationUserRepository ??= new Repository<ApplicationUser>(_context);

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
