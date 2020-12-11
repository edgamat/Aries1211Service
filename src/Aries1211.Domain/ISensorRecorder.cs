using System.Threading;
using System.Threading.Tasks;

namespace Aries1211.Domain
{
    public interface ISensorRecorder
    {
        Task<Reading> RecordReadingAsync(CancellationToken token);
    }
}