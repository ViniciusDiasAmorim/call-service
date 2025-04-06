using CallServiceFlow.Dto.TechnicalDTO;
using CallServiceFlow.Model;

namespace CallServiceFlow.Repository.Interfaces
{
    public interface ITechnicalRepository : IRepository<Technical>
    {
        Task <(bool ok, string message, TechnicalResponseDto responseDto)> CreateTechnical(TechnicalDto techinicalDto);
    }
}
