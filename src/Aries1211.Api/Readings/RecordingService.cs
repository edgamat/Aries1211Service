using System;
using System.Threading;
using System.Threading.Tasks;
using Aries1211.Domain;
using Aries1211.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Aries1211.Api.Readings
{
    public class RecordingService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;

        private const int PollingDelaySeconds = 5;

        public RecordingService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var logger = _serviceProvider.GetRequiredService<ILogger<RecordingService>>();

            logger.LogInformation("Recording service is starting.");

            stoppingToken.Register(() => logger.LogInformation("Recording service is stopping."));

            using (var dbScope = _serviceProvider.CreateScope())
            {
                var context = dbScope.ServiceProvider.GetService<AriesContext>();
                logger.LogInformation("Recording service is applying migrations to the database.");
                try
                {
                    await context.Database.MigrateAsync(stoppingToken);
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "Recording service encountered an exception");
                    throw;
                }
            }

            logger.LogInformation("Recording service is ready.");

            while (!stoppingToken.IsCancellationRequested)
            {
                using var scope = _serviceProvider.CreateScope();
                try
                {
                    logger.LogDebug("Recording service is doing work.");

                    var recorder = scope.ServiceProvider.GetService<ISensorRecorder>();

                    await recorder.RecordReadingAsync(stoppingToken);
                }
                catch (OperationCanceledException ex)
                {
                    logger.LogWarning(ex, "Recording service encountered a timeout");
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "Recording service encountered an exception");
                }

                await Task.Delay(TimeSpan.FromSeconds(PollingDelaySeconds), stoppingToken);
            }
        }
    }
}