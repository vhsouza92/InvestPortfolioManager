using InvestPortfolioManager.Notification.Interfaces;
using Microsoft.Extensions.Hosting;
using System.Threading;
using System.Threading.Tasks;

namespace InvestPortfolioManager.Notification.Services
{
    public class NotificationScheduler : BackgroundService
    {
        private readonly INotificationService _notificationService;

        public NotificationScheduler(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                await _notificationService.SendMaturityNotificationsAsync();
                await Task.Delay(TimeSpan.FromDays(1), stoppingToken);
            }
        }
    }
}
