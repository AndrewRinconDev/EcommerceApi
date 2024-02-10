using System.Collections.Specialized;

namespace EcommerceApi.Models
{
    public class OrderRecord
    {
        public string? id { get; set; }
        public string detail { get; set; }
        public DateTime date { get; set; }
        public string orderId { get; set; }
        public virtual Order order { get; set; }
        public string orderStateId { get; set; }
        public virtual OrderState orderState { get; set; }
    }
}
