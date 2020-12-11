using Aries1211.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aries1211.Persistence.Readings
{
    public class ReadingConfiguration : IEntityTypeConfiguration<Reading>
    {
        public void Configure(EntityTypeBuilder<Reading> builder)
        {
            builder.ToTable("Readings");

            builder.Property(x => x.Pressure)
                .HasPrecision(8, 4);

            builder.Property(x => x.Oxygen)
                .HasPrecision(8, 4);

            builder.Property(x => x.Temperature)
                .HasPrecision(8, 4);
        }
    }
}
