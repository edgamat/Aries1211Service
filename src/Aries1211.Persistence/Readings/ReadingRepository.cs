using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Aries1211.Domain;
using Microsoft.EntityFrameworkCore;

namespace Aries1211.Persistence.Readings
{
    public class ReadingRepository : IReadingRepository
    {
        private readonly AriesContext _context;

        public ReadingRepository(AriesContext context)
        {
            _context = context;
        }

        public Task<Reading> GetLatestAsync(CancellationToken token)
        {
            return _context.Set<Reading>()
                .AsNoTracking()
                .OrderByDescending(x => x.Id)
                .FirstOrDefaultAsync(token);
        }

        public async Task InsertAsync(Reading reading, CancellationToken token)
        {
            _context.Set<Reading>().Add(reading);

            var _ = await _context.SaveChangesAsync(token);
        }
    }
}