using InvestPortfolioManager.Client.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace InvestPortfolioManager.Client.Infrastructure.Repositories
{
    public class AppDbContext : DbContext
    {
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Portfolio> Portfolios { get; set; }
        public DbSet<PortfolioItem> PortfolioItems { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("Users");
                entity.HasKey(u => u.Id);
                entity.Property(u => u.Name).IsRequired().HasMaxLength(100);
                entity.Property(u => u.Email).IsRequired().HasMaxLength(100);
                entity.Property(u => u.CreatedDate).IsRequired();
                entity.Property(u => u.UpdatedDate).IsRequired(false); 
            });

            modelBuilder.Entity<Portfolio>(entity =>
            {
                entity.ToTable("Portfolios");
                entity.HasKey(p => p.Id);
                entity.Property(p => p.UserId).IsRequired();
                entity.Property(p => p.CreatedDate).IsRequired();
                entity.Property(p => p.UpdatedDate).IsRequired(false); 

                entity.HasMany(p => p.Items)
                      .WithOne()
                      .HasForeignKey(pi => pi.PortfolioId);
            });

            modelBuilder.Entity<PortfolioItem>(entity =>
            {
                entity.ToTable("PortfolioItems");
                entity.HasKey(pi => pi.Id);
                entity.Property(pi => pi.PortfolioId).IsRequired();
                entity.Property(pi => pi.ProductId).IsRequired();
                entity.Property(pi => pi.Quantity).IsRequired();
                entity.Property(pi => pi.TotalValue).HasColumnType("decimal(18,2)").IsRequired();
            });

            modelBuilder.Entity<Transaction>(entity =>
            {
                entity.ToTable("Transactions");
                entity.HasKey(t => t.Id);
                entity.Property(t => t.ProductId).IsRequired();
                entity.Property(t => t.UserId).IsRequired();
                entity.Property(t => t.Quantity).IsRequired();
                entity.Property(t => t.UnitPrice).HasColumnType("decimal(18,2)").IsRequired();
                entity.Property(t => t.TransactionDate).IsRequired();
                entity.Property(t => t.Type).IsRequired();
            });
        }
    }
}
