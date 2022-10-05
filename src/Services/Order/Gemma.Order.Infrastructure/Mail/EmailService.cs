using Gemma.Order.Application.Contracts.Infrastructure;
using Gemma.Order.Application.Contracts.Persistance;
using Gemma.Order.Application.Factory.Mail;
using Gemma.Order.Application.Models;
using Gemma.Order.Domain.Entities;
using Gemma.Shared.Extensions;
using Microsoft.Extensions.Configuration;
using MimeKit;
using Newtonsoft.Json;
using Serilog;

namespace Gemma.Order.Infrastructure.Mail
{
    public class EmailService : MailFactoryBase, IEmailService
    {
        private readonly IConfiguration _configuration;
        private EmailSettingsOptions _emailSettings = new EmailSettingsOptions();
        private readonly ILogger _logger;
        private readonly IAsyncRepository<NotificationEmailHistory> _emailHistoyRepository;

        public EmailService(ILogger logger, IConfiguration configuration, IAsyncRepository<NotificationEmailHistory> emailHistoyRepository)
        {
            _logger = logger;
            _configuration = configuration;
            _configuration.GetSection(EmailSettingsOptions.EmailSettings).Bind(_emailSettings);
            _emailHistoyRepository = emailHistoyRepository;
        }

        public async Task SendEmail(MimeMessage email)
        {
            _logger.Here().MethodEnterd();
            var client = await CreateMailClient(_emailSettings, _logger);
            var message = GenerateMessage(email, _emailSettings, _logger);

            _logger.Here().Debug("Preparing for sending mail");
            try
            {
                await client.SendAsync(message);
                _logger.Here().Information("Mail sent to {@recipient}", email.To);
            }
            catch (Exception ex)
            {
                _logger.Here().Error("Failed to send mail", ex);
            }
            finally
            {
                _logger.Here().MethodEnterd();
                client.Disconnect(true);
            }

            await SaveEmailHistoryToStore(email, _logger);
            _logger.Here().MethodExited();
        }
    
        private async Task SaveEmailHistoryToStore(MimeMessage email, ILogger logger)
        {
            var notificationHistory = new NotificationEmailHistory
            {
                Data = JsonConvert.SerializeObject(new { 
                    Sender = email.Sender.Address, 
                    Recipient = email.To,
                    Body = email.HtmlBody
                }),
                MailSentTime = DateTime.UtcNow,
                Status = "Sent"
            };

            try
            {
                await _emailHistoyRepository.AddAsync(notificationHistory);
                logger.Here().Information("mail history table updated.");
            }catch(Exception ex)
            {
                logger.Here().Error("mail history table updation failed. {@stackTrace}", ex.StackTrace);
            }
        }
    }
}
