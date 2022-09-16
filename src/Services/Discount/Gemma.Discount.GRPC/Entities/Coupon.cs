﻿namespace Gemma.Discount.GRPC.Entities
{
    public class Coupon: BaseEntity
    {
        public string ProductId { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }
        public int Amount { get; set; }
    }
}
