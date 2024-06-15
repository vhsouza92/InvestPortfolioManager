using RabbitMQ.Client;
using System.Text;

namespace InvestPortfolioManager.Infrastructure.Messaging
{
    public class MessagePublisher
    {
        private readonly IModel _channel;

        public MessagePublisher(RabbitMQConnection connection)
        {
            _channel = connection.GetChannel();
        }

        public void Publish(string message, string exchange, string routingKey)
        {
            var body = Encoding.UTF8.GetBytes(message);
            _channel.BasicPublish(exchange: exchange, routingKey: routingKey, basicProperties: null, body: body);
        }
    }
}
