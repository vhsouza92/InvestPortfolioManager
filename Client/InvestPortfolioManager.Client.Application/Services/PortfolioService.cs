using System.Threading.Tasks;
using InvestPortfolioManager.Client.Domain.Repositories;
using InvestPortfolioManager.Client.Application.Models;
using InvestPortfolioManager.Client.Domain.Entities;

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


    }
}
