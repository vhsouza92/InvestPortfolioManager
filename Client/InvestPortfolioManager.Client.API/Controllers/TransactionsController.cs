using InvestPortfolioManager.Client.Application.Services;
using InvestPortfolioManager.Client.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InvestPortfolioManager.Client.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TransactionsController : ControllerBase
    {
        private readonly TransactionService _transactionService;

        public TransactionsController(TransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        [HttpGet]
        public async Task<IEnumerable<Transaction>> GetAllTransactions()
        {
            return await _transactionService.GetAllTransactionsAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Transaction>> GetTransactionById(int id)
        {
            var transaction = await _transactionService.GetTransactionByIdAsync(id);

            if (transaction == null)
            {
                return NotFound();
            }

            return transaction;
        }

        [HttpPost]
        public async Task<ActionResult<Transaction>> AddTransaction(Transaction transaction)
        {
            await _transactionService.AddTransactionAsync(transaction);
            return CreatedAtAction(nameof(GetTransactionById), new { id = transaction.Id }, transaction);
        }
    }
}
