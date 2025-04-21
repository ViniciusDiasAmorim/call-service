namespace CallServiceFlow.Repository.Interfaces
{
    public interface IUnitOfWork
    {
        ITicketRepository TicketRepository { get; }
        ICustomerRepository CustomerRepository { get; }
        ITechnicalRepository TechnicalRepository { get; }
        Task Commit();
    }
}
