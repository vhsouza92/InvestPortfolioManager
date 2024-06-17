using InvestPortfolioManager.Operational.Domain.Entities;
using InvestPortfolioManager.Operational.Domain.Repositories;
using InvestPortfolioManager.Shared.Events;
using InvestPortfolioManager.Operational.Domain.Services;

namespace InvestPortfolioManager.Operational.Application.Services
{
    public class FinancialProductService
    {
        private readonly IFinancialProductRepository _repository;
        private readonly IEventPublisher _eventPublisher;

        public FinancialProductService(IFinancialProductRepository repository, IEventPublisher eventPublisher)
        {
            _repository = repository;
            _eventPublisher = eventPublisher;
        }

        public async Task AddProductAsync(FinancialProduct product)
        {
            await _repository.AddAsync(product);

            var eventMessage = new FinancialProductChangedEvent
            {
                Action = "Created",
                ProductId = product.Id,
                ProductName = product.Name,
                MaturityDate = product.MaturityDate,
                Value = product.Value
            };

            _eventPublisher.Publish(eventMessage);
        }

        public async Task<FinancialProduct> GetProductByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<FinancialProduct>> GetAllProductsAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task UpdateProductAsync(FinancialProduct product)
        {
            await _repository.UpdateAsync(product);

            var eventMessage = new FinancialProductChangedEvent
            {
                Action = "Updated",
                ProductId = product.Id,
                ProductName = product.Name,
                MaturityDate = product.MaturityDate,
                Value = product.Value
            };

            _eventPublisher.Publish(eventMessage);
        }

        public async Task DeleteProductAsync(int id)
        {
            var product = await _repository.GetByIdAsync(id);
            if (product != null)
            {
                await _repository.DeleteAsync(product);

                var eventMessage = new FinancialProductChangedEvent
                {
                    Action = "Deleted",
                    ProductId = product.Id,
                    ProductName = product.Name,
                    MaturityDate = product.MaturityDate,
                    Value = product.Value
                };

                _eventPublisher.Publish(eventMessage);
            }
        }
    }
}
