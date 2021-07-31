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
        private readonly ILogger<RecordingService> _logger;

        private const int PollingDelaySeconds = 5;

        public RecordingService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _logger = _serviceProvider.GetRequiredService<ILogger<RecordingService>>();
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Recording service is starting.");
            stoppingToken.Register(() => _logger.LogInformation("Recording service is stopping."));

            await ApplyPendingMigrationsAsync(stoppingToken);

            while (!stoppingToken.IsCancellationRequested)
            {
                await DoWorkAsync(stoppingToken);

                await Task.Delay(TimeSpan.FromSeconds(PollingDelaySeconds), stoppingToken);
            }
        }

        private async Task DoWorkAsync(CancellationToken stoppingToken)
        {
            _logger.LogDebug("Recording service is doing work.");

            using var scope = _serviceProvider.CreateScope();

            try
            {
                var recorder = scope.ServiceProvider.GetService<ISensorRecorder>();

                await recorder.RecordReadingAsync(stoppingToken);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Recording service encountered an exception.");
            }
        }

        private async Task ApplyPendingMigrationsAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Recording service is applying migrations to the database.");

            using var dbScope = _serviceProvider.CreateScope();

            var context = dbScope.ServiceProvider.GetService<AriesContext>();

            try
            {
                await context.Database.MigrateAsync(stoppingToken);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Recording service encountered an exception");
                throw;
            }

            _logger.LogInformation("Recording service is ready.");
        }
    }
}