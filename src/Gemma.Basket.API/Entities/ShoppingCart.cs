﻿namespace Gemma.Basket.API.Entities
{
    public class ShoppingCart: BaseEntity
    {
        public string UserName { get; set; }
        public IEnumerable<ShoppingCartItem> Items { get; set; } = new List<ShoppingCartItem>();

        public ShoppingCart(){}

        public ShoppingCart(string username)
        {
            UserName = username;    
        }

        public decimal TotalPrice
        {
            get
            {
                decimal totalPrice = 0;
                foreach (var item in Items)
                {
                    totalPrice += item.Price * item.Quantity;
                }
                return totalPrice;
            }
        }
    }
}
