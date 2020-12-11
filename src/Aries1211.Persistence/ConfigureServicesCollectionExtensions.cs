using Aries1211.Domain;
using Aries1211.Persistence;
using Aries1211.Persistence.Readings;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection
{
    public static class ConfigureServicesCollectionExtensions
    {
        public static IServiceCollection AddAriesContext(
            this IServiceCollection services,
            IConfiguration configuration,
            bool isDevelopment)
        {
            // Add DbContextOptions<AriesContext> to the container
            services.AddSingleton(p => AriesContextDesignTimeDbContextFactory.CreateOptions(configuration, isDevelopment));

            // Use DbContextOptions<AriesContext> to construct the context
            services.AddScoped(p => new AriesContext(p.GetService<DbContextOptions<AriesContext>>()));

            services.AddScoped<IReadingRepository, ReadingRepository>();

            return services;
        }
    }
}