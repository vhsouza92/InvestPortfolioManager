using InvestPortfolioManager.Client.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace InvestPortfolioManager.Client.Infrastructure.Repositories
{
   public class AppDbContext : DbContext
{
    public DbSet<Transaction> Transactions { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Transaction>(entity =>
        {
            entity.ToTable("Transactions");
            entity.HasKey(t => t.Id);
            entity.Property(t => t.Amount).HasColumnType("decimal(18,2)");
        });
    }
}
}
