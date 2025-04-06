using CallServiceFlow.Context;
using CallServiceFlow.Dto.TechnicalDTO;
using CallServiceFlow.Model;
using CallServiceFlow.Repository.Interfaces;

namespace CallServiceFlow.Repository
{
    public class TechnicalRepository : Repository<Technical>, ITechnicalRepository
    {
        private readonly AppDbContext _context;

        public TechnicalRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<(bool, string, TechnicalResponseDto)> CreateTechnical(TechnicalDto techinicalDto)
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
            
            try
            {
                await _context.Technicals.AddAsync(techinical);
                await _context.SaveChangesAsync();

                var responseDto = new TechnicalResponseDto()
                {
                    Id = techinical.Id,
                    Name = techinical.Name,
                    Email = techinical.Email,
                    MaxCalls = techinical.MaxTickets
                };
                
                return (true, "Técnico criado com sucesso", responseDto);
            }
            catch (Exception ex)
            {
                return (false, $"Erro ao criar técnico: {ex.Message}", null);
            }

        }

    }
}
