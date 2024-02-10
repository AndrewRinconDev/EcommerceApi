namespace EcommerceApi.Models
{
    public class Product
    {
        public string? id { get; set; }
        public string name { get; set; }
        public string? description { get; set; }
        public string imageUrl { get; set; }
        public double price { get; set; }
        public int stock { get; set; }
        public string categoryId { get; set; }
        public bool isActive { get; set; }
        public virtual Category category { get; set; }
        public virtual ICollection<OrderProduct> orderProducts { get; set; }
    }
}
