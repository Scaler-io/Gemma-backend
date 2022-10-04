namespace Gemma.Order.Application.Models.Requests
{
    public class PaymentDetailsRequest
    {
        public int Id { get; set; }
        public string CardName { get; set; }
        public string CardNumber { get; set; }
        public string Expiration { get; set; }
        public string CVV { get; set; }
        public int PaymentMethod { get; set; }
    }
}