using System.Threading;
using System.Threading.Tasks;

namespace Aries1211.Domain
{
    public interface IReadingRepository
    {
        Task<Reading> GetLatestAsync(CancellationToken token);

        Task InsertAsync(Reading reading, CancellationToken token);
    }
}