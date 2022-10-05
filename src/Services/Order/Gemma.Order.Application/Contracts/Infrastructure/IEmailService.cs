using Gemma.Order.Application.Models;
using MimeKit;

namespace Gemma.Order.Application.Contracts.Infrastructure
{
    public interface IEmailService
    {
        Task SendEmail(MimeMessage email);
    }
}
