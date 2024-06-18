using InvestPortfolioManager.Notification.Entities;
using Microsoft.EntityFrameworkCore;

namespace InvestPortfolioManager.Notification.Infrastructure
{
    public class AppDbContext : DbContext
    {
        public DbSet<NotificationFinancialProduct> FinancialProducts { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<NotificationFinancialProduct>()
                .Property(p => p.Value)
                .HasColumnType("decimal(18,2)");
        }
    }
}
