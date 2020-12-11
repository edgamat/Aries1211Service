using Microsoft.EntityFrameworkCore;

namespace Aries1211.Persistence
{
    public class AriesContext : DbContext
    {
        public AriesContext(DbContextOptions<AriesContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AriesContext).Assembly);

            base.OnModelCreating(modelBuilder);
        }
    }
}
