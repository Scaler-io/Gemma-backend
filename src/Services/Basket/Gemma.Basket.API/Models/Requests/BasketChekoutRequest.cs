namespace Gemma.Basket.API.Models.Requests
{
    public class BasketChekoutRequest
    {
        public string UserName { get; set; }
        public decimal TotalPrice { get; set; }
        public CheckoutOrderBillingAddressRequest? Address { get; set; }
        public CheckoutOrderPaymentDetailsRequest? PaymentDetails { get; set; }
    }
}
