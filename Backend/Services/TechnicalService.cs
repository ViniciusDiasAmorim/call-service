using CallServiceFlow.Dto.TechnicalDTO;
using CallServiceFlow.Model;
using CallServiceFlow.Repository.Interfaces;
using CallServiceFlow.Services.Interfaces;

namespace CallServiceFlow.Services
{
    public class TechnicalService : ITechnicalService
    {
        private readonly IUnitOfWork _unitOfWork;
        public TechnicalService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<(bool ok, string message, TechnicalResponseDto responseDto)> CreateTechnicalAsync(TechnicalDto techinicalDto)
        {
            try
            {
                var techinical = new Technical()
                {
                    Name = techinicalDto.Name,
                    Email = techinicalDto.Email,
                    CreationDate = DateTime.Now,
                    Active = true,
                    MaxTickets = techinicalDto.MaxCalls ?? 5,
                    LastAccess = null
                };


                await _unitOfWork.TechnicalRepository.AddAsync(techinical);
                await _unitOfWork.Commit();

                var responseDto = new TechnicalResponseDto()
                {
                    Id = techinical.Id,
                    Name = techinical.Name,
                    Email = techinical.Email,
                    MaxCalls = techinical.MaxTickets
                };

                return (true, "Tecnico criado com sucesso", responseDto);
            }
            catch (Exception ex)
            {
                return (false, $"Erro ao criar tecnico: {ex.Message}", null);
            }
        }
    }
}
