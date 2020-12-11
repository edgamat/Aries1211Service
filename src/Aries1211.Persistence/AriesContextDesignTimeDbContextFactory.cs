using System;
using System.IO;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Aries1211.Persistence
{
    // ReSharper disable once ClassNeverInstantiated.Global
    public class AriesContextDesignTimeDbContextFactory : IDesignTimeDbContextFactory<AriesContext>
    {
        public AriesContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location))
                .AddJsonFile("appsettings.json")
                .AddUserSecrets<AriesContextDesignTimeDbContextFactory>(true)
                .Build();

            return new AriesContext(CreateOptions(configuration, false));
        }

        public static DbContextOptions<AriesContext> CreateOptions(IConfiguration configuration, bool isDevelopment)
        {
            if (configuration == null)
                throw new ArgumentNullException(nameof(configuration));

            var contextOptions = new DbContextOptionsBuilder<AriesContext>();

            contextOptions.UseSqlServer(configuration["Database:ConnectionString"]);

            if (isDevelopment)
            {
                contextOptions.LogTo(Console.WriteLine, new[] { RelationalEventId.CommandExecuted }, LogLevel.Warning)
                    .EnableSensitiveDataLogging()
                    .EnableDetailedErrors();
            }

            return contextOptions.Options;
        }
    }
}