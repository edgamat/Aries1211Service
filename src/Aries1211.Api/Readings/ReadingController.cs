using System.Threading;
using System.Threading.Tasks;
using Aries1211.Domain;
using Microsoft.AspNetCore.Mvc;

namespace Aries1211.Api.Readings
{
    [ApiController]
    [Route("[controller]")]
    public class ReadingController : ControllerBase
    {
        private readonly IReadingRepository _readingRepository;

        public ReadingController(IReadingRepository readingRepository)
        {
            _readingRepository = readingRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetLatest(CancellationToken token)
        {
            var reading = await _readingRepository.GetLatestAsync(token);

            var data = new
            {
                pressure = reading?.Pressure,
                oxygen = reading?.Oxygen,
                temperature = reading?.Temperature
            };

            return Ok(data);
        }
    }
}
