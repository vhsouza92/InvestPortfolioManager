using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using InvestPortfolioManager.Notification.Entities;

namespace InvestPortfolioManager.Notification.Interfaces
{
    public interface INotificationFinancialProductRepository
    {
        Task<IEnumerable<NotificationFinancialProduct>> GetProductsNearMaturityAsync(DateTime nearMaturityDate);
    }
}
