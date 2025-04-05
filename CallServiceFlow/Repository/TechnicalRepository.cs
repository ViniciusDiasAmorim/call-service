using CallServiceFlow.Context;
using CallServiceFlow.Dto;
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

        public async Task<(bool, string)> CreateTechnical(TechinicalDto techinicalDto)
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
                
                return (true, "Técnico criado com sucesso");
            }
            catch (Exception ex)
            {
                return (false, $"Erro ao criar técnico: {ex.Message}");
            }

        }

    }
}
