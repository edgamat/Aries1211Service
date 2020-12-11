using System;
using System.Threading;
using System.Threading.Tasks;

namespace Aries1211.Domain
{
    public class SensorRecorder : ISensorRecorder
    {
        private readonly ISensorAdapter _sensorAdapter;
        private readonly IReadingRepository _readingRepository;

        public SensorRecorder(ISensorAdapter sensorAdapter, IReadingRepository readingRepository)
        {
            _sensorAdapter = sensorAdapter;
            _readingRepository = readingRepository;
        }

        public async Task<Reading> RecordReadingAsync(CancellationToken token)
        {
            var reading = new Reading
            {
                Pressure = _sensorAdapter.GetPressure(),
                Oxygen = _sensorAdapter.GetOxygen(),
                Temperature = _sensorAdapter.GetTemperature(),
                RecordedAt = DateTime.UtcNow
            };

            await _readingRepository.InsertAsync(reading, token);

            return reading;
        }
    }
}