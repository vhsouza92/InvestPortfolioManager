// Operational/InvestPortfolioManager.Operational.Application/Services/FinancialProductService.cs
using System.Threading.Tasks;
using InvestPortfolioManager.Operational.Domain.Entities;
using InvestPortfolioManager.Operational.Domain.Repositories;
using InvestPortfolioManager.Shared;
using InvestPortfolioManager.Shared.Events;

namespace InvestPortfolioManager.Operational.Application.Services
{
    public class FinancialProductService
    {
        private readonly IFinancialProductRepository _financialProductRepository;
        private readonly IEventPublisher _eventPublisher;

        public FinancialProductService(IFinancialProductRepository financialProductRepository, IEventPublisher eventPublisher)
        {
            _financialProductRepository = financialProductRepository;
            _eventPublisher = eventPublisher;
        }

        public async Task AddProductAsync(FinancialProduct product)
        {
            await _financialProductRepository.AddAsync(product);

            var productEvent = new FinancialProductChangedEvent
            {
                Action = "Added",
                ProductName = product.Name,
                Value = product.Value,
                MaturityDate = product.MaturityDate
            };

            await _eventPublisher.PublishAsync("FinancialProductChanged", productEvent);
        }

        public async Task<FinancialProduct> GetProductByIdAsync(int id)
        {
            return await _financialProductRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<FinancialProduct>> GetAllProductsAsync()
        {
            return await _financialProductRepository.GetAllAsync();
        }

        public async Task UpdateProductAsync(FinancialProduct product)
        {
            await _financialProductRepository.UpdateAsync(product);

            var productEvent = new FinancialProductChangedEvent
            {
                Action = "Updated",
                ProductName = product.Name,
                Value = product.Value,
                MaturityDate = product.MaturityDate
            };

            await _eventPublisher.PublishAsync("FinancialProductChanged", productEvent);
        }

        public async Task DeleteProductAsync(int id)
        {
            var product = await _financialProductRepository.GetByIdAsync(id);
            if (product != null)
            {
                await _financialProductRepository.DeleteAsync(product);

                var productEvent = new FinancialProductChangedEvent
                {
                    Action = "Deleted",
                    ProductName = product.Name,
                    Value = product.Value,
                    MaturityDate = product.MaturityDate
                };

                await _eventPublisher.PublishAsync("FinancialProductChanged", productEvent);
            }
        }
    }
}
