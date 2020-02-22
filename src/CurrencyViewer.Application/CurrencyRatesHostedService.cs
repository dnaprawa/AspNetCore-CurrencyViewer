using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CurrencyViewer.Application
{
    public class CurrencyRatesHostedService : IHostedService, IDisposable
    {
        private Task _executingTask;
        private readonly ILogger<CurrencyRatesHostedService> _logger;
        private readonly CancellationTokenSource _stoppingCts = new CancellationTokenSource();
        public CurrencyRatesHostedService(IServiceProvider services,
            ILogger<CurrencyRatesHostedService> logger
            )
        {
            Services = services;
            _logger = logger;

        }

        public IServiceProvider Services { get; }

        protected async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using (var scope = Services.CreateScope())
                {
                    var service =
                        scope.ServiceProvider
                            .GetRequiredService<ICurrencyRatesReceiver>();

                    service.GetCurrencyRatesAsync(DateTime.UtcNow.Date).GetAwaiter().GetResult();
                }

                await Task.Delay(TimeSpan.FromDays(1), stoppingToken);
            }
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation(
                $"CurrencyRateHostedService is starting..."
                );

            _executingTask = ExecuteAsync(_stoppingCts.Token);

            if (_executingTask.IsCompleted)
            {
                return _executingTask;
            }

            return Task.CompletedTask;
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation($"CurrencyRates background task is stopping.");

            if (_executingTask == null)
            {
                return;
            }

            try
            {
                _stoppingCts.Cancel();
            }
            finally
            {
                await Task.WhenAny(_executingTask, Task.Delay(Timeout.Infinite,
                    cancellationToken));
            }
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _stoppingCts.Cancel();
            }
        }
    }
}
