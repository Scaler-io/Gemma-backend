using System.Reflection;
using System.Text;
using Gemma.Order.Application.Models;
using Gemma.Shared.Extensions;
using MimeKit;
using Serilog;

namespace Gemma.Order.Application.Factory.Mail
{
    using Order = Order.Domain.Entities.Order;
    public class OrderPlacedMailFactory
    {
        public static MimeMessage GenerateOrderPlacedMessage(Order order, ILogger logger)
        {
            return GenerateMailTemplate(order, logger);
        }

        private static MimeMessage GenerateMailTemplate(Order order, ILogger logger)
        {
            var mailText = ReadEmailTemplate(logger);
            mailText = EmbeddEmailFields(mailText, order, logger);

            var builder = new BodyBuilder();
            builder.HtmlBody = mailText;

            var email = new MimeMessage();
            email.To.Add(MailboxAddress.Parse(order.Address.EmailAddress));
            email.Subject = "Your Order has been placed";

            email.Body = builder.ToMessageBody();
            return email;
        }

        private static string ReadEmailTemplate(ILogger logger)
        {
            string filePath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Factory/Templates/OrderPlacedEmailTemplate.html");
            StreamReader str = new StreamReader(filePath);
            string mailText = str.ReadToEnd();
            str.Close();
            return mailText;
        }

        private static string EmbeddEmailFields(string mailText, Order order, ILogger logger)
        {
            var emailfields = new List<EmailField>
            {
                new EmailField("[username]", order.UserName),
                new EmailField("[total]", order.TotalPrice.ToString()),
                new EmailField("[addressLine]", order.Address.AddressLine)
            };

            var builder = new StringBuilder(mailText);

            foreach (var field in emailfields)
            {
                logger.Here().Information($"field - {field.Key}, value - {field.Value}");
                builder.Replace(field.Key, field.Value);
            }
            
            return builder.ToString();
        }
    }
}