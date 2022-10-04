using Gemma.Order.Application.Contracts.Infrastructure;
using Gemma.Order.Application.Models;
using Gemma.Shared.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Serilog;
using System.Net;
using System.Net.Mail;

namespace Gemma.Order.Infrastructure.Mail
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;
        private EmailSettingsOptions _emailSettings = new EmailSettingsOptions();
        private readonly ILogger _logger;
        private readonly IHttpContextAccessor _context;

        public EmailService(ILogger logger, IHttpContextAccessor context, IConfiguration configuration)
        {
            _logger = logger;
            _context = context;
            _configuration = configuration;
            _configuration.GetSection(EmailSettingsOptions.EmailSettings).Bind(_emailSettings);
        }

        public async Task SendEmail(Email email)
        {
            _logger.Here().MethodEnterd();
            var client = CreateMailClient();
            var message = GenerateMessage(email);

            _logger.Here().Debug("Preparing for sending mail");
            try
            {
                await client.SendMailAsync(message);
                _logger.Here().Information("Mail sent to {@recipient}", email.To);

            }catch (Exception ex)
            {
                _logger.Here().Error("Failed to send mail", ex);
            }
            finally
            {
                _logger.Here().MethodEnterd();
                client.Dispose();
            }
        }

        private MailMessage GenerateMessage(Email email)
        {
            MailAddress to = new MailAddress(email.To);
            MailAddress from = new MailAddress(_emailSettings.CompanyAddress);
            MailMessage message = new MailMessage(from, to);
            message.Subject = email.Subject;
            message.Body = email.Body;

            _logger.Here().Information("Mail message prepared {@message}", message);
            return message;
        }

        private SmtpClient CreateMailClient()
        {
            var client = new SmtpClient(_emailSettings.Server, _emailSettings.Port)
            {
                Credentials = new NetworkCredential(_emailSettings.Username, _emailSettings.Password),
                EnableSsl = false
            };
            _logger.Here().Information("Mail client established {@clientDetails}", _emailSettings);
            return client;
        }
    }
}
