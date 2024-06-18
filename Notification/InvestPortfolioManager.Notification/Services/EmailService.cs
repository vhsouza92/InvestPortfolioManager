using InvestPortfolioManager.Notification.Entities;
using InvestPortfolioManager.Notification.Interfaces;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace InvestPortfolioManager.Notification.Services
{
    public class EmailService : IEmailService
    {
        private readonly ILogger<EmailService> _logger;
        private readonly NotificationSettings _settings;

        public EmailService(ILogger<EmailService> logger, IOptions<NotificationSettings> options)
        {
            _logger = logger;
            _settings = options.Value;
        }

        public async Task SendDailyNotificationsAsync(IEnumerable<NotificationFinancialProduct> productsNearMaturity)
        {
            if (productsNearMaturity == null || !productsNearMaturity.Any())
            {
                _logger.LogInformation("No products near maturity to notify.");
                return;
            }

            _logger.LogInformation($"Sending notifications to: {_settings.ToEmail}");
            foreach (var product in productsNearMaturity)
            {
                await SendEmailAsync(product);
            }
        }

        public async Task SendEmailAsync(NotificationFinancialProduct product)
        {
            try
            {
                if (product.Value <= 0)
                {
                    throw new ArgumentException("Product value must be a non-negative and non-zero value.", nameof(product.Value));
                }

                var smtpClient = new SmtpClient(_settings.SmtpServer)
                {
                    Port = _settings.SmtpPort,
                    Credentials = new NetworkCredential(_settings.SmtpUsername, _settings.SmtpPassword),
                    EnableSsl = true,
                };

                var mailMessage = new MailMessage
                {
                    From = new MailAddress(_settings.FromEmail),
                    Subject = $"Product Alert: {product.Name}",
                    Body = $"The product {product.Name} is nearing maturity. Current value: {product.Value}, Maturity date: {product.MaturityDate}",
                    IsBodyHtml = false,
                };

                var recipients = _settings.ToEmail.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                foreach (var recipient in recipients)
                {
                    mailMessage.To.Add(recipient.Trim());
                }

                await smtpClient.SendMailAsync(mailMessage);
                _logger.LogInformation($"Email sent for product: {product.Name}");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error sending email: {ex.Message}");
            }
        }
    }
}
