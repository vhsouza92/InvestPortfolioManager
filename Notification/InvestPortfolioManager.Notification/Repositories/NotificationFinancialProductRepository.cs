using InvestPortfolioManager.Notification.Entities;
using InvestPortfolioManager.Notification.Infrastructure;
using InvestPortfolioManager.Notification.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InvestPortfolioManager.Notification.Repositories
{
    public class NotificationFinancialProductRepository : INotificationFinancialProductRepository
    {
        private readonly AppDbContext _context;

        public NotificationFinancialProductRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<NotificationFinancialProduct>> GetProductsNearMaturityAsync(DateTime nearMaturityDate)
        {
            var today = DateTime.UtcNow;
            return await _context.FinancialProducts
                .Where(p => p.MaturityDate <= nearMaturityDate && p.MaturityDate >= today)
                .ToListAsync();
        }
    }
}
