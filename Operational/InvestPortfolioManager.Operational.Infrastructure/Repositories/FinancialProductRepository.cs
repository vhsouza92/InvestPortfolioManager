using System.Collections.Generic;
using System.Threading.Tasks;
using InvestPortfolioManager.Operational.Domain.Entities;
using InvestPortfolioManager.Operational.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace InvestPortfolioManager.Operational.Infrastructure.Repositories
{
    public class FinancialProductRepository : IFinancialProductRepository
    {
        private readonly AppDbContext _context;

        public FinancialProductRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(FinancialProduct product)
        {
            await _context.FinancialProducts.AddAsync(product);
            await _context.SaveChangesAsync();
        }

        public async Task<FinancialProduct> GetByIdAsync(int id)
        {
            return await _context.FinancialProducts.FindAsync(id);
        }

        public async Task<IEnumerable<FinancialProduct>> GetAllAsync()
        {
            return await _context.FinancialProducts.ToListAsync();
        }

        public async Task UpdateAsync(FinancialProduct product)
        {
            _context.FinancialProducts.Update(product);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(FinancialProduct product)
        {
            _context.FinancialProducts.Remove(product);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<FinancialProduct>> GetProductsNearMaturityAsync(DateTime date)
        {
             var today = DateTime.UtcNow;

            return await _context.FinancialProducts
                .Where(p => p.MaturityDate <= date && p.MaturityDate >= today)
                .ToListAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var product = await _context.FinancialProducts.FindAsync(id);
            if (product != null)
            {
                _context.FinancialProducts.Remove(product);
                await _context.SaveChangesAsync();
            }
        }
    }
}
