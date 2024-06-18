using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using InvestPortfolioManager.Notification.Services;
using InvestPortfolioManager.Notification.Repositories;
using InvestPortfolioManager.Notification.Infrastructure;
using Microsoft.EntityFrameworkCore;
using InvestPortfolioManager.Notification.Interfaces;

public class Program
{
    public static void Main(string[] args)
    {
        CreateHostBuilder(args).Build().Run();
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureAppConfiguration((context, config) =>
            {
                config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            })
            .ConfigureServices((context, services) =>
            {
                // Configuration for NotificationSettings
                services.Configure<NotificationSettings>(context.Configuration.GetSection("NotificationSettings"));
                
                // Add DbContext with SQL Server provider
                services.AddDbContext<AppDbContext>(options =>
                    options.UseSqlServer(context.Configuration.GetConnectionString("DefaultConnection")));
                
                // Register services and repositories
                services.AddSingleton<IEmailService, EmailService>();
                services.AddScoped<INotificationFinancialProductRepository, NotificationFinancialProductRepository>();
                services.AddHostedService<DailyNotificationService>();
                services.AddHostedService<NotificationService>();
            });
}
