using RabbitMQ.Client;
using System;

namespace InvestPortfolioManager.Infrastructure.Messaging
{
    public class RabbitMQConnection : IDisposable
    {
        private readonly IConnection _connection;
        private readonly IModel _channel;

        public RabbitMQConnection(string hostName)
        {
            var factory = new ConnectionFactory() { HostName = hostName };
            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
        }

        public IModel GetChannel() => _channel;

        public void Dispose()
        {
            _channel.Close();
            _connection.Close();
        }
    }
}
