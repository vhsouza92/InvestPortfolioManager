using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using InvestPortfolioManager.Shared.Events;
using InvestPortfolioManager.Client.Application.Services;

namespace InvestPortfolioManager.Client.Infrastructure.Messaging
{
    public class RabbitMqEventConsumer : BackgroundService
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly IConnection _connection;
        private readonly IModel _channel;

        public RabbitMqEventConsumer(IServiceScopeFactory serviceScopeFactory, IConnection connection)
        {
            _serviceScopeFactory = serviceScopeFactory;
            _connection = connection;
            _channel = _connection.CreateModel();
            _channel.ExchangeDeclare(exchange: "exchange_name", type: ExchangeType.Topic);
            _channel.QueueDeclare(queue: "transaction_queue", durable: true, exclusive: false, autoDelete: false, arguments: null);
            _channel.QueueBind(queue: "transaction_queue", exchange: "exchange_name", routingKey: "TransactionAdded");
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += async (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                var transactionEvent = JsonConvert.DeserializeObject<TransactionEvent>(message);

                using (var scope = _serviceScopeFactory.CreateScope())
                {
                    var transactionService = scope.ServiceProvider.GetRequiredService<TransactionService>();
                    await transactionService.HandleTransactionAddedAsync(transactionEvent);
                }
            };

            _channel.BasicConsume(queue: "transaction_queue", autoAck: true, consumer: consumer);

            return Task.CompletedTask;
        }

        public override void Dispose()
        {
            _channel.Close();
            _connection.Close();
            base.Dispose();
        }
    }
}
