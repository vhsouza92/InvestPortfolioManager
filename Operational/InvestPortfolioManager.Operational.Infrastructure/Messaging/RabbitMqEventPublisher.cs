using System.Text;
using System.Threading.Tasks;
using InvestPortfolioManager.Shared.Events;
using Newtonsoft.Json;
using RabbitMQ.Client;

namespace InvestPortfolioManager.Client.Infrastructure.Messaging
{
    public class RabbitMqEventPublisher : IEventPublisher
    {
        private readonly IConnection _connection;

        public RabbitMqEventPublisher(IConnection connection)
        {
            _connection = connection;
        }

        public async Task PublishAsync(string eventName, object eventData)
        {
            using var channel = _connection.CreateModel();
            channel.ExchangeDeclare(exchange: "exchange_name", type: ExchangeType.Topic);

            var message = JsonConvert.SerializeObject(eventData);
            var body = Encoding.UTF8.GetBytes(message);

            channel.BasicPublish(exchange: "exchange_name", routingKey: eventName, basicProperties: null, body: body);
            await Task.CompletedTask;
        }
    }
}
