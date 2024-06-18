using System.Collections.Generic;
using System.Threading.Tasks;
using InvestPortfolioManager.Client.Domain.Entities;
using InvestPortfolioManager.Client.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace InvestPortfolioManager.Client.Infrastructure.Repositories
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly AppDbContext _context;

        public TransactionRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Transaction transaction)
        {
            await _context.Transactions.AddAsync(transaction);
            await _context.SaveChangesAsync();
        }

        public async Task<Transaction> GetByIdAsync(int id)
        {
            return await _context.Transactions.FindAsync(id);
        }

        public async Task<List<Transaction>> GetAllAsync()
        {
            return await _context.Transactions.ToListAsync();
        }
    }
}
