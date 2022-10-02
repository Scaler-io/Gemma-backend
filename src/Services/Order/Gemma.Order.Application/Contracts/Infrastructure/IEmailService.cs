using Gemma.Order.Application.Models;

namespace Gemma.Order.Application.Contracts.Infrastructure
{
    public interface IEmailService
    {
        Task SendEmail(Email email);
    }
}
