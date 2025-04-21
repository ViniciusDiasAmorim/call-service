using CallServiceFlow.Context;
using CallServiceFlow.Repository.Interfaces;

namespace CallServiceFlow.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        private TicketRepository _ticketRepository;
        private CustomerRepository _customerRepository;
        private TechnicalRepository _technicalRepository;

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
        }

        public ITicketRepository TicketRepository
        {
            get { return _ticketRepository = _ticketRepository ?? new TicketRepository(_context); }
        }

        public ICustomerRepository CustomerRepository
        {
            get { return _customerRepository = _customerRepository ?? new CustomerRepository(_context); }
        }

        public ITechnicalRepository TechnicalRepository
        {
            get { return _technicalRepository = _technicalRepository ?? new TechnicalRepository(_context); }
        }

        public async Task Commit()
        {
            await _context.SaveChangesAsync();
        }
    }
}
