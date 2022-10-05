namespace Gemma.Order.Domain.Entities
{
    public class NotificationEmailHistory: BaseEntity
    {
        public string Data { get; set; }
        public DateTime MailSentTime { get; set; }
        public string Status { get; set; }
    }
}
