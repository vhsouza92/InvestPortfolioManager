using InvestPortfolioManager.Client.Domain.Repositories;
using InvestPortfolioManager.Client.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using InvestPortfolioManager.Shared.Events;

namespace InvestPortfolioManager.Client.Application.Services
{
    public class TransactionService
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly IEventPublisher _eventPublisher;

        public TransactionService(ITransactionRepository transactionRepository, IEventPublisher eventPublisher)
        {
            _transactionRepository = transactionRepository;
            _eventPublisher = eventPublisher;
        }

        public async Task AddTransactionAsync(Transaction transaction)
        {
            if (!IsValidTransactionType(transaction.Type.ToUpper()))
            {
                throw new ArgumentException("Invalid transaction type");
            }

            await _transactionRepository.AddAsync(transaction);

            var transactionEvent = new TransactionEvent
            {
                TransactionId = transaction.Id,
                ProductId = transaction.ProductId,
                Amount = transaction.Quantity * transaction.UnitPrice,
                Date = transaction.TransactionDate,
                Type = transaction.Type
            };

            await _eventPublisher.PublishAsync("TransactionAdded", transactionEvent);
        }

        public async Task<Transaction> GetTransactionByIdAsync(int id)
        {
            return await _transactionRepository.GetByIdAsync(id);
        }

        public async Task<List<Transaction>> GetAllTransactionsAsync()
        {
            return await _transactionRepository.GetAllAsync();
        }

        public async Task HandleTransactionAddedAsync(TransactionEvent transactionEvent)
        {
            // Lógica para lidar com o evento de transação adicionada
        }

        private bool IsValidTransactionType(string type)
        {
            return type == "BUY" || type == "SELL";
        }
    }
}
