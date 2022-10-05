using Gemma.Order.Application.Contracts.Infrastructure;
using Gemma.Order.Application.Factory.Mail;
using Gemma.Order.Application.Models;
using Gemma.Shared.Extensions;
using Microsoft.Extensions.Configuration;
using MimeKit;
using Serilog;

namespace Gemma.Order.Infrastructure.Mail
{
    public class EmailService : MailFactoryBase, IEmailService
    {
        private readonly IConfiguration _configuration;
        private EmailSettingsOptions _emailSettings = new EmailSettingsOptions();
        private readonly ILogger _logger;

        public EmailService(ILogger logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
            _configuration.GetSection(EmailSettingsOptions.EmailSettings).Bind(_emailSettings);
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

            }catch (Exception ex)
            {
                _logger.Here().Error("Failed to send mail", ex);
            }
            finally
            {
                _logger.Here().MethodEnterd();
                client.Disconnect(true);
            }
        }
    }
}
