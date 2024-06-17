using System.Text;
using System.Text.Json;
using RabbitMQ.Client;
using InvestPortfolioManager.Shared.Events;
using InvestPortfolioManager.Operational.Domain.Services;

namespace InvestPortfolioManager.Operational.Infrastructure.Messaging
{
    public class RabbitMqEventPublisher : IEventPublisher
    {
        private readonly IConnection _connection;

        public RabbitMqEventPublisher(IConnection connection)
        {
            _connection = connection;
        }

        public void Publish(FinancialProductChangedEvent financialProductEvent)
        {
            using (var channel = _connection.CreateModel())
            {
                channel.QueueDeclare(queue: "financialProductQueue",
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                var message = JsonSerializer.Serialize(financialProductEvent);
                var body = Encoding.UTF8.GetBytes(message);

                channel.BasicPublish(exchange: "",
                                     routingKey: "financialProductQueue",
                                     basicProperties: null,
                                     body: body);
            }
        }
    }
}
