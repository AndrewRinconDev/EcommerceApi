namespace EcommerceApi.Models
{
    public class OrderProduct
    {
        public string? id { get; set; }
        public int quantity { get; set; }
        public string orderId { get; set; }
        public virtual Order order { get; set; }
        public string productId { get; set; }
        public virtual Product product { get; set; }
    }
}
