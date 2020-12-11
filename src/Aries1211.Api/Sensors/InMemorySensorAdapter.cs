using System;
using Aries1211.Domain;

namespace Aries1211.Api.Sensors
{
    public class InMemorySensorAdapter : ISensorAdapter
    {
        private readonly Random _rng = new Random();

        public decimal? GetTemperature()
        {
            return GetReading(19, 35, 2);
        }

        public decimal? GetPressure()
        {
            return GetReading(7, 20, 2);
        }

        public decimal? GetOxygen()
        {
            return GetReading(7, 100, 2);
        }

        internal decimal GetReading(int lower, int upper, int decimals)
        {
            var scale = 10 ^ decimals;

            return _rng.Next(lower * scale, upper * scale) / (decimal)scale;
        }
    }
}
