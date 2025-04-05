using CallServiceFlow.Dto;
using CallServiceFlow.Model;

namespace CallServiceFlow.Repository.Interfaces
{
    public interface ITechnicalRepository : IRepository<Technical>
    {
        Task <(bool ok, string message)> CreateTechnical(TechinicalDto techinicalDto);
    }
}
