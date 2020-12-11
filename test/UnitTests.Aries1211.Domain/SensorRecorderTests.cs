using System.Threading;
using System.Threading.Tasks;
using Aries1211.Domain;
using Moq;
using Shouldly;
using Xunit;

namespace UnitTests.Aries1211.Domain
{
    public class SensorRecorderTests
    {
        private int _counter = 0;

        [Fact]
        public async Task RecordReading_ReturnsReading()
        {
            var fakeRepository = CreateFakeRepository();
            var fakeSensorAdapter = CreateFakeSensorAdapter((1,2,3));

            var sut = new SensorRecorder(fakeSensorAdapter.Object, fakeRepository.Object);

            var reading = await sut.RecordReadingAsync(CancellationToken.None);

            reading.ShouldNotBeNull();
            reading.Id.ShouldBeGreaterThan(0);
            reading.Pressure.ShouldBe(1);
            reading.Oxygen.ShouldBe(2);
            reading.Temperature.ShouldBe(3);
        }

        private Mock<IReadingRepository> CreateFakeRepository()
        {
            var fakeRepository = new Mock<IReadingRepository>();
            fakeRepository
                .Setup(x => x.InsertAsync(It.IsAny<Reading>(), CancellationToken.None))
                .Callback((Reading reading, CancellationToken token) =>
                {
                    _counter++;
                    reading.Id = _counter;
                });

            return fakeRepository;
        }

        private Mock<ISensorAdapter> CreateFakeSensorAdapter((decimal? Pressure, decimal? Oxygen, decimal? Temperature) data)
        {
            var fakeRepository = new Mock<ISensorAdapter>();

            var (pressure, oxygen, temperature) = data;

            fakeRepository.Setup(x => x.GetPressure()).Returns(pressure);
            fakeRepository.Setup(x => x.GetOxygen()).Returns(oxygen);
            fakeRepository.Setup(x => x.GetTemperature()).Returns(temperature);

            return fakeRepository;
        }
    }
}
