using Aries1211.Api.Sensors;
using Shouldly;
using Xunit;

namespace UnitTests.Aries1211.Api
{
    public class InMemorySensorAdapterTests
    {
        [Fact]
        public void TestGetReading()
        {
            var sut = new InMemorySensorAdapter();

            var actual = sut.GetReading(1, 100, 0);

            actual.ShouldBeGreaterThanOrEqualTo(0);
            actual.ShouldBeLessThanOrEqualTo(100);
        }

        [Fact]
        public void TestGetReadingDecimals()
        {
            var sut = new InMemorySensorAdapter();

            var actual = sut.GetReading(1, 100, 2);

            actual.ShouldBeGreaterThanOrEqualTo(0);
            actual.ShouldBeLessThanOrEqualTo(100);
        }
    }
}
