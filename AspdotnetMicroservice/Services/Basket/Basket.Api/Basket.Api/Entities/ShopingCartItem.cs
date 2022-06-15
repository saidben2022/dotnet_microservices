namespace Basket.Api.Entities
{
    public class ShopingCartItem
    {
        public int Quantity { get; set; }
        public int ProductId { get; set; }
        
        public string Color { get; set; }

        public decimal Price { get; set; }

        public string ProductName { get; set; }
    }
}
