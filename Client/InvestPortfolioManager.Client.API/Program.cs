using InvestPortfolioManager.Client.Application.Services;
using InvestPortfolioManager.Client.Domain.Repositories;
using InvestPortfolioManager.Client.Infrastructure.Messaging;
using InvestPortfolioManager.Client.Infrastructure.Repositories;
using InvestPortfolioManager.Shared.Events;
using Microsoft.EntityFrameworkCore;
using RabbitMQ.Client;
using Polly;
using RabbitMQ.Client.Exceptions;

var builder = WebApplication.CreateBuilder(args);

// services
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// DbContext
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Application Services
builder.Services.AddScoped<ITransactionRepository, TransactionRepository>();
builder.Services.AddScoped<TransactionService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<IPortfolioRepository, PortfolioRepository>();
builder.Services.AddScoped<PortfolioService>();
builder.Services.AddHostedService<RabbitMqEventConsumer>();
builder.Services.AddScoped<IEventPublisher, RabbitMqEventPublisher>();

builder.Services.AddSingleton(sp =>
{
    var factory = new ConnectionFactory() { HostName = "rabbitmq" };
    return Policy
        .Handle<BrokerUnreachableException>()
        .WaitAndRetry(5, retryAttempt => TimeSpan.FromSeconds(2 * retryAttempt))
        .Execute(() => factory.CreateConnection());
});

// Kestrel 
builder.WebHost.ConfigureKestrel(serverOptions =>
{
    serverOptions.ListenAnyIP(80);
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1"));
}

if (!app.Environment.IsDevelopment())
{
    app.UseHttpsRedirection();
}
app.UseAuthorization();
app.MapControllers();
app.Run();
