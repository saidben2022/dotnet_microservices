namespace Basket.Api.Entities
{
    public class ShopingCart
    {
        public string Username { get; set; }
        public List<ShopingCartItem> Items { get; set; } = new List<ShopingCartItem>();
        public ShopingCart()
        {

        }
        public ShopingCart(string Username)
        {
            this.Username = Username;
        }
        public decimal TotalPrice
        {
            get
            {
                decimal total = 0;
                foreach (var item in Items)
                {
                    total += item.Price * item.Quantity;
                }
                return total;
            }
            
          
        }



    }
}
