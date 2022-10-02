namespace Gemma.Order.Application.Models.Requests.OrderUpdate
{
    public class UpdateOrderRequest
    {
        public int OrderId { get; set; }
        public string UserName { get; set; }
        public decimal TotalPrice { get; set; }
        public BillingAddressRequest Address { get; set; }
        public PaymentDetailsRequest PaymentDetails { get; set; }
    }
}
