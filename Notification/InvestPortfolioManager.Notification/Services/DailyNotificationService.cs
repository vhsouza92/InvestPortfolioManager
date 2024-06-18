using InvestPortfolioManager.Notification.Interfaces;
using InvestPortfolioManager.Notification.Entities;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace InvestPortfolioManager.Notification.Services
{
    public class DailyNotificationService : BackgroundService
    {
        private readonly ILogger<DailyNotificationService> _logger;
        private readonly IEmailService _emailService;
        private readonly INotificationFinancialProductRepository _productRepository;
        private readonly NotificationSettings _settings;
        private Timer _timer;

        public DailyNotificationService(ILogger<DailyNotificationService> logger, IEmailService emailService, INotificationFinancialProductRepository productRepository, IOptions<NotificationSettings> settings)
        {
            _logger = logger;
            _emailService = emailService;
            _productRepository = productRepository;
            _settings = settings.Value;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation($"Notification service is starting with DaysToMaturity: {_settings.DaysToMaturity} and ToEmail: {_settings.ToEmail}");    
            _timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromDays(1));
            return Task.CompletedTask;
        }

        private async void DoWork(object state)
        {
            _logger.LogInformation("Daily notification service is working.");
            _logger.LogInformation($"Days to Maturity: {_settings.DaysToMaturity}");
            _logger.LogInformation($"Sending notifications to: {_settings.ToEmail}");

            var nearMaturityDate = DateTime.UtcNow.AddDays(_settings.DaysToMaturity);
            var today = DateTime.UtcNow;

            var productsNearMaturity = await _productRepository.GetProductsNearMaturityAsync(nearMaturityDate);
            await _emailService.SendDailyNotificationsAsync(productsNearMaturity);
        }

        public override void Dispose()
        {
            _timer?.Dispose();
            base.Dispose();
        }
    }
}
