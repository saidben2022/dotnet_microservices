namespace Basket.Api.Entities
{
    public class ShopingCartItem
    {
        public int Quantity { get; set; }
        public string ProductId { get; set; }//Important Id is String
        
        public string Color { get; set; }

        public decimal Price { get; set; }

        public string ProductName { get; set; }
    }
}
