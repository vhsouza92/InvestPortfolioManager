using System.Collections.Generic;
using System.Threading.Tasks;
using InvestPortfolioManager.Operational.Domain.Entities;

namespace InvestPortfolioManager.Operational.Domain.Repositories
{
    public interface IFinancialProductRepository
    {
        Task<IEnumerable<FinancialProduct>> GetAllAsync();
        Task<FinancialProduct> GetByIdAsync(int id);
        Task AddAsync(FinancialProduct product);
        Task UpdateAsync(FinancialProduct product);
        Task DeleteAsync(int id);
        Task<IEnumerable<FinancialProduct>> GetProductsNearMaturityAsync(DateTime date);
    }
}
