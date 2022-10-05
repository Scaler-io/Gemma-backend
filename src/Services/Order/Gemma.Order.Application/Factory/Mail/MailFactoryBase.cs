using Gemma.Order.Application.Models;
using Gemma.Shared.Extensions;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using Serilog;

namespace Gemma.Order.Application.Factory.Mail
{
    public class MailFactoryBase
    {
         public async Task<SmtpClient> CreateMailClient(EmailSettingsOptions settings, ILogger logger)
        {
            var client = new SmtpClient();
            try{
                await client.ConnectAsync(settings.Server, settings.Port, SecureSocketOptions.StartTls);
                await client.AuthenticateAsync(settings.Username, settings.Password);
            }catch(Exception ex){
                logger.Here().Information("Failed to establish connection to SMTP server. {@stackTrace}", ex);
            }
            logger.Here().Information("Mail client established {@clientDetails}", settings);
            return client;
        }   

        public MimeMessage GenerateMessage(MimeMessage email, EmailSettingsOptions settings, ILogger logger)
        {
            email.Sender = MailboxAddress.Parse(settings.CompanyAddress);
            logger.Here().Information("Mail message prepared {@message}", email);
            return email;
        }
    }
}