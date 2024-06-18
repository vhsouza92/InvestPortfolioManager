using System.Data.SqlTypes;
using System.Threading.Tasks;
using InvestPortfolioManager.Client.Domain.Entities;
using InvestPortfolioManager.Client.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace InvestPortfolioManager.Client.Infrastructure.Repositories
{
    public class PortfolioRepository : IPortfolioRepository
    {
        private readonly AppDbContext _context;

        public PortfolioRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Portfolio> GetByUserIdAsync(int userId)
        {
            try
            {
                var portfolio = await _context.Portfolios
                                  .Include(p => p.Items)
                                  .FirstOrDefaultAsync(p => p.UserId == userId);

                if (portfolio == null)
                {
                    throw new InvalidOperationException("Portfolio not found");
                }

                return portfolio;
            }
            catch (SqlNullValueException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                throw new InvalidOperationException("Portfolio contains null values", ex);
            }
        }




        public async Task AddAsync(Portfolio portfolio)
        {
            await _context.Portfolios.AddAsync(portfolio);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Portfolio portfolio)
        {
            _context.Portfolios.Update(portfolio);
            await _context.SaveChangesAsync();
        }

        public async Task UpdatePortfolioItemAsync(PortfolioItem portfolioItem)
        {
            _context.PortfolioItems.Update(portfolioItem);
            await _context.SaveChangesAsync();
        }
    }
}
