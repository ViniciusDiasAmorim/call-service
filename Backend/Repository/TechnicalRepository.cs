using CallServiceFlow.Context;
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

        public async Task AddTechnicalAsync(Technical techinical)
        {
            await _context.AddAsync(techinical);
        }

    }
}
