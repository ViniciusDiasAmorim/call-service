using CallServiceFlow.Model;

namespace CallServiceFlow.Repository.Interfaces
{
    public interface IUnitOfWork
    {
        IRepository<Ticket> TicketRepository { get; }
        IRepository<Customer> CustomerRepository { get; }
        IRepository<Technical> TechnicalRepository { get; }
        IRepository<ApplicationUser> ApplicationUserRepository { get; }
        Task SaveAsync();
    }
}
