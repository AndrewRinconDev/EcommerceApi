using System.Text.Json.Serialization;

namespace EcommerceApi.Models.Database
{
    public class OrderProduct : BDEntity
    {
        public int quantity { get; set; }
        public Guid orderId { get; set; }
        public Guid productId { get; set; }
        [JsonIgnore]
        public virtual Order? order { get; set; }
        [JsonIgnore]
        public virtual Product? product { get; set; }
    }
}
