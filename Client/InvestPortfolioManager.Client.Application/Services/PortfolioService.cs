using System.Threading.Tasks;
using InvestPortfolioManager.Client.Domain.Repositories;
using InvestPortfolioManager.Client.Application.DTOs;
using InvestPortfolioManager.Client.Domain.Entities;
using InvestPortfolioManager.Shared.Events;

namespace InvestPortfolioManager.Client.Application.Services
{
    public class PortfolioService
    {
        private readonly IPortfolioRepository _portfolioRepository;

        public PortfolioService(IPortfolioRepository portfolioRepository)
        {
            _portfolioRepository = portfolioRepository;
        }

        public async Task<PortfolioDto> GetPortfolioByUserIdAsync(int userId)
        {
            try
            {
                var portfolio = await _portfolioRepository.GetByUserIdAsync(userId);

                if (portfolio == null)
                {
                    throw new InvalidOperationException("Portfolio not found");
                }

                var portfolioDto = new PortfolioDto
                {
                    UserId = portfolio.UserId,
                    CreatedDate = portfolio.CreatedDate,
                    UpdatedDate = portfolio.UpdatedDate,
                    Items = portfolio.Items.Select(item => new PortfolioItemDto
                    {
                        ProductId = item.ProductId,
                        Quantity = item.Quantity,
                        TotalValue = item.TotalValue
                    }).ToList()
                };

                return portfolioDto;
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                throw;
            }
        }

        public async Task UpdatePortfolioItemValuesAsync(FinancialProductChangedEvent productEvent)
        {
            var portfolios = await _portfolioRepository.GetAllPortfoliosAsync();
            foreach (var portfolio in portfolios)
            {
                foreach (var item in portfolio.Items)
                {
                    if (item.ProductId == productEvent.ProductId)
                    {
                        item.TotalValue = item.Quantity * productEvent.Value;
                        portfolio.UpdatedDate = DateTime.UtcNow;
                    }
                }
                await _portfolioRepository.UpdateAsync(portfolio);
            }
        }
    }
}
