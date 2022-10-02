namespace Gemma.Order.Application.Models.Dtos.Order
{
    public class OrdersDto
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public decimal TotalPrice { get; set; }

        public BillingAddressDto Address { get; set; }
        public PaymentDetailsDto PaymentDetails { get; set; }
        public MetadataDto MetaData { get; set; }
    }
}
