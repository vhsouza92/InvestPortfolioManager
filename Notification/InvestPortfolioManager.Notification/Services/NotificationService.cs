using System;
using System.Threading;
using System.Threading.Tasks;
using InvestPortfolioManager.Notification.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace InvestPortfolioManager.Notification.Services
{
    public class NotificationService : BackgroundService
    {
        private readonly ILogger<NotificationService> _logger;
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public NotificationService(ILogger<NotificationService> logger, IServiceScopeFactory serviceScopeFactory)
        {
            _logger = logger;
            _serviceScopeFactory = serviceScopeFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Notification Service running.");

            while (!stoppingToken.IsCancellationRequested)
            {
                using (var scope = _serviceScopeFactory.CreateScope())
                {
                    var productRepository = scope.ServiceProvider.GetRequiredService<INotificationFinancialProductRepository>();
                    var emailService = scope.ServiceProvider.GetRequiredService<IEmailService>();

                    var productsNearMaturity = await productRepository.GetProductsNearMaturityAsync(DateTime.UtcNow.AddDays(30));
                    await emailService.SendDailyNotificationsAsync(productsNearMaturity);
                }

                await Task.Delay(TimeSpan.FromHours(24), stoppingToken); // Delay for 24 hours
            }
        }
    }

}
