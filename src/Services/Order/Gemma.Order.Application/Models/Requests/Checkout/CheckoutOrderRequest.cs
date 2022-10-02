namespace Gemma.Order.Application.Models.Requests.Checkout
{
    public class CheckoutOrderRequest
    {
        public string UserName { get; set; }
        public decimal TotalPrice { get; set; }
        public BillingAddressRequest Address { get; set; }
        public PaymentDetailsRequest PaymentDetails { get; set; }
    }
}
