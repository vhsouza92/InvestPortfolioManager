namespace InvestPortfolioManager.Notification.Interfaces
{
    public interface INotificationService
    {
        Task SendMaturityNotificationsAsync();
    }
}
