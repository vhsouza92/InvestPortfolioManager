using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using InvestPortfolioManager.Client.Domain.Repositories;
using InvestPortfolioManager.Client.Domain.Entities;
using InvestPortfolioManager.Shared.Events;
using InvestPortfolioManager.Client.Application.DTOs;

namespace InvestPortfolioManager.Client.Application.Services
{
    public class TransactionService
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly IEventPublisher _eventPublisher;
        private readonly IPortfolioRepository _portfolioRepository;

        public TransactionService(ITransactionRepository transactionRepository, IEventPublisher eventPublisher, IPortfolioRepository portfolioRepository)
        {
            _transactionRepository = transactionRepository;
            _eventPublisher = eventPublisher;
            _portfolioRepository = portfolioRepository;
        }

        public async Task AddTransactionAsync(TransactionDto request)
        {
            var portfolio = await _portfolioRepository.GetByUserIdAsync(request.UserId);

            if (portfolio == null)
            {
                portfolio = new Portfolio
                {
                    UserId = request.UserId,
                    CreatedDate = DateTime.UtcNow,
                    Items = new List<PortfolioItem>()
                };
                await _portfolioRepository.AddAsync(portfolio);
            }

            var portfolioItem = portfolio.Items.FirstOrDefault(pi => pi.ProductId == request.ProductId);

            if (request.Type?.ToUpper() == "BUY")
            {
                if (portfolioItem == null)
                {
                    portfolioItem = new PortfolioItem
                    {
                        PortfolioId = portfolio.Id,
                        ProductId = request.ProductId,
                        Quantity = request.Quantity,
                        TotalValue = request.Quantity * request.UnitPrice
                    };
                    portfolio.Items.Add(portfolioItem);
                }
                else
                {
                    portfolioItem.Quantity += request.Quantity;
                    portfolioItem.TotalValue += request.Quantity * request.UnitPrice;
                }
            }
            else if (request.Type?.ToUpper() == "SELL")
            {
                if (portfolioItem == null || portfolioItem.Quantity < request.Quantity)
                {
                    throw new InvalidOperationException("Insufficient quantity for sale");
                }

                portfolioItem.Quantity -= request.Quantity;
                portfolioItem.TotalValue -= request.Quantity * request.UnitPrice;

                if (portfolioItem.Quantity == 0)
                {
                    portfolio.Items.Remove(portfolioItem);
                }
            }
            else
            {
                throw new ArgumentException("Invalid transaction type");
            }

            var transaction = new Transaction
            {
                ProductId = request.ProductId,
                UserId = request.UserId,
                Quantity = request.Quantity,
                UnitPrice = request.UnitPrice,
                TransactionDate = DateTime.UtcNow,
                Type = request.Type
            };

            await _transactionRepository.AddAsync(transaction);

            portfolio.UpdatedDate = DateTime.UtcNow;
            await _portfolioRepository.UpdateAsync(portfolio);

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

        private bool IsValidTransactionType(string type)
        {
            return type == "BUY" || type == "SELL";
        }
    }
}
