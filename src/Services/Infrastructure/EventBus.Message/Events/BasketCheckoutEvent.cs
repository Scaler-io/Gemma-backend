using EventBus.Message.Models;

namespace EventBus.Message.Events
{
    public class BasketCheckoutEvent: IntegrationBaseEvent
    {
        public string UserName { get; set; }
        public decimal TotalPrice { get; set; }
        public CheckoutOrderBillingAddress? Address { get; set; }
        public CheckoutOrderPaymentDetails? PaymentDetails { get; set; }
    }
}
