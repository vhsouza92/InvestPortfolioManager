using System.Collections.Generic;
using System.Threading.Tasks;
using InvestPortfolioManager.Client.Domain.Entities;

namespace InvestPortfolioManager.Client.Domain.Repositories
{
    public interface ITransactionRepository
    {
        Task AddAsync(Transaction transaction);
        Task<Transaction> GetByIdAsync(int id);
        Task<IEnumerable<Transaction>> GetAllAsync();
    }
}
