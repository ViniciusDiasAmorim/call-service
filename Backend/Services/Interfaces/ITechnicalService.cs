using CallServiceFlow.Dto.TechnicalDTO;

namespace CallServiceFlow.Services.Interfaces
{
    public interface ITechnicalService
    {
        Task<(bool ok, string message, TechnicalResponseDto responseDto)> CreateTechnicalAsync(TechnicalDto techinicalDto);
    }
}
