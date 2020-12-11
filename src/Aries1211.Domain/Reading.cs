using System;

namespace Aries1211.Domain
{
    public class Reading
    {
        public int Id { get; set; }

        public DateTime RecordedAt { get; set; }

        public decimal? Pressure { get; set; }

        public decimal? Oxygen { get; set; }

        public decimal? Temperature { get; set; }
    }
}
