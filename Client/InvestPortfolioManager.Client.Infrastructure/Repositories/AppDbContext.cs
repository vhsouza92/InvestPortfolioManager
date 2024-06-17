using InvestPortfolioManager.Client.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace InvestPortfolioManager.Client.Infrastructure.Repositories
{
    public class AppDbContext : DbContext
    {
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<User> Users { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Transaction>(entity =>
            {
                entity.ToTable("Transactions");
                entity.HasKey(t => t.Id);
                entity.Property(t => t.UnitPrice).HasColumnType("decimal(18,2)");
                entity.Property(t => t.Type).IsRequired().HasMaxLength(10); // Buy, Sell
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("Users");
                entity.HasKey(u => u.Id);
                entity.Property(u => u.Name).IsRequired().HasMaxLength(100);
                entity.Property(u => u.Email).IsRequired().HasMaxLength(100);
                entity.Property(u => u.CreatedDate).HasDefaultValueSql("GETDATE()");
                entity.Property(u => u.UpdatedDate).HasDefaultValueSql("GETDATE()");
            });
        }
    }
}
