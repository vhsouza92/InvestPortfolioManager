using InvestPortfolioManager.Shared.Events;

namespace InvestPortfolioManager.Operational.Domain.Services
{
    public interface IEventPublisher
    {
        void Publish(FinancialProductChangedEvent financialProductEvent);
    }
}
