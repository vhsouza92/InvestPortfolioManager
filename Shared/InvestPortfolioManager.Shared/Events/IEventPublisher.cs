namespace InvestPortfolioManager.Shared.Events
{
    public interface IEventPublisher
    {
        Task PublishAsync(string eventName, object eventData);
    }
}