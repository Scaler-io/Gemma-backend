using Destructurama.Attributed;

namespace Gemma.Order.Application.Models
{
    public class EmailSettingsOptions
    {
        public const string EmailSettings = "EmailSettings";
        public string Server { get; set; }
        public int Port { get; set; }
        public string CompanyAddress { get; set; }
        [LogMasked]
        public string Username { get; set; }
        [LogMasked]
        public string Password { get; set; }
    }
}
