using InvestPortfolioManager.Client.Domain.Repositories;
using InvestPortfolioManager.Client.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InvestPortfolioManager.Client.Application.Services
{
    public class TransactionService
    {
        private readonly ITransactionRepository _transactionRepository;

        public TransactionService(ITransactionRepository transactionRepository)
        {
            _transactionRepository = transactionRepository;
        }

        public async Task HandleMessage(string message)
        {
            // Process the message and convert it to a transaction
            var transaction = new Transaction
            {
                // Initialize transaction properties based on the message
            };

            // Add transaction to the repository
            await _transactionRepository.AddAsync(transaction);
        }

        public async Task<IEnumerable<Transaction>> GetAllTransactionsAsync()
        {
            return await _transactionRepository.GetAllAsync();
        }

        public async Task<Transaction> GetTransactionByIdAsync(int id)
        {
            return await _transactionRepository.GetByIdAsync(id);
        }

        public async Task AddTransactionAsync(Transaction transaction)
        {
            await _transactionRepository.AddAsync(transaction);
        }
    }
}
