using System.Collections.Generic;
using System.Threading.Tasks;
using InvestPortfolioManager.Operational.Domain.Entities;

namespace InvestPortfolioManager.Operational.Domain.Repositories
{
    public interface IFinancialProductRepository
    {
        Task AddAsync(FinancialProduct product);
        Task<FinancialProduct> GetByIdAsync(int id);
        Task<IEnumerable<FinancialProduct>> GetAllAsync();
        Task UpdateAsync(FinancialProduct product);
        Task DeleteAsync(FinancialProduct product);
    }
}
