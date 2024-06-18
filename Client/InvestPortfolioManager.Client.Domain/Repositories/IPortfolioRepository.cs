using System.Threading.Tasks;
using InvestPortfolioManager.Client.Domain.Entities;

namespace InvestPortfolioManager.Client.Domain.Repositories
{
    public interface IPortfolioRepository
    {
        Task<Portfolio> GetByUserIdAsync(int userId);
        Task AddAsync(Portfolio portfolio);
        Task UpdateAsync(Portfolio portfolio);
        Task UpdatePortfolioItemAsync(PortfolioItem portfolioItem);
    }
}
