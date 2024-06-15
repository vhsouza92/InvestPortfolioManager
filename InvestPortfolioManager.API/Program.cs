using InvestPortfolioManager.Application.Services;
using InvestPortfolioManager.Infrastructure.Repositories;
using InvestPortfolioManager.Domain.Repositories;
using InvestPortfolioManager.Infrastructure.Messaging;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register DbContext with a connection string
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        sqlServerOptions => sqlServerOptions.EnableRetryOnFailure()
    ));

// Register application services as scoped
builder.Services.AddScoped<IFinancialProductRepository, FinancialProductRepository>();
builder.Services.AddScoped<FinancialProductService>();
builder.Services.AddSingleton(sp => new RabbitMQConnection("rabbitmq"));
builder.Services.AddSingleton<MessagePublisher>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();