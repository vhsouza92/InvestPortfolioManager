﻿using Microsoft.EntityFrameworkCore;
using InvestPortfolioManager.Operational.Domain.Entities;

namespace InvestPortfolioManager.Operational.Infrastructure.Repositories
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<FinancialProduct> FinancialProducts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<FinancialProduct>(entity =>
            {
                entity.Property(e => e.Value)
                    .HasColumnType("decimal(18,2)");
            });
        }
    }
}

