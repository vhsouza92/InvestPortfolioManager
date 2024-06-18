using System.Collections.Generic;
using System.Threading.Tasks;
using InvestPortfolioManager.Notification.Entities;
using InvestPortfolioManager.Shared.Events;

namespace InvestPortfolioManager.Notification.Interfaces
{
    public interface IEmailService
    {
        Task SendDailyNotificationsAsync(IEnumerable<NotificationFinancialProduct> productsNearMaturity);
        Task SendEmailAsync(NotificationFinancialProduct productEvent);
    }

}
