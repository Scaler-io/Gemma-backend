namespace Gemma.Order.Domain.Entities
{
    public class Order: BaseEntity
    {
        public string UserName { get; set; }
        public decimal TotalPrice { get; set; }
        public BillingAddress Address { get; set; }
        public PaymentDetails PaymentDetails { get; set; }
        public int BillingAddressId { get; set; }
        public int PaymentDetailsId { get; set; }
    }
}
