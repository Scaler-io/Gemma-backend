namespace Gemma.Basket.API.Entities
{
    public class ShoppingCart: EntityBase
    {
        public string UserName { get; set; }
        public IEnumerable<ShoppingCartItems> Items { get; set; }

        public ShoppingCart()
        {

        }

        public ShoppingCart(string username)
        {
            UserName = username;
        }

        public decimal TotalPrice
        {
            get
            {
                decimal totalPrice = 0;
                foreach(var item in Items)
                {
                    totalPrice += item.Price * item.Quantity;
                }
                return totalPrice;
            }
        }
    }
}
