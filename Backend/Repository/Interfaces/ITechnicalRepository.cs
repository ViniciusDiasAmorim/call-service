using CallServiceFlow.Model;

namespace CallServiceFlow.Repository.Interfaces
{
    public interface ITechnicalRepository : IRepository<Technical>
    {
        Task AddTechnicalAsync(Technical techinical);
    }
}
