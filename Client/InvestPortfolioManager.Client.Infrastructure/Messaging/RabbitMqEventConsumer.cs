using InvestPortfolioManager.Client.Application.Services;
using Microsoft.Extensions.Hosting;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace InvestPortfolioManager.Client.Infrastructure.Messaging
{
    public class RabbitMqEventConsumer : BackgroundService
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private IConnection _connection;
        private IModel _channel;

        public RabbitMqEventConsumer(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
            InitializeRabbitMq();
        }

        private void InitializeRabbitMq()
        {
            var factory = new ConnectionFactory() { HostName = "rabbitmq" };

            while (true)
            {
                try
                {
                    _connection = factory.CreateConnection();
                    _channel = _connection.CreateModel();
                    _channel.QueueDeclare(queue: "financial_product_changes", durable: false, exclusive: false, autoDelete: false, arguments: null);
                    break;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Failed to connect to RabbitMQ: {ex.Message}. Retrying in 5 seconds...");
                    Thread.Sleep(5000);
                }
            }
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);

                using (var scope = _serviceScopeFactory.CreateScope())
                {
                    var transactionService = scope.ServiceProvider.GetRequiredService<TransactionService>();
                    transactionService.HandleMessage(message).GetAwaiter().GetResult();
                }
            };
            _channel.BasicConsume(queue: "financial_product_changes", autoAck: true, consumer: consumer);
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
