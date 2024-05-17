using EcommerceApi.Models.Reference;
using System.Text.Json.Serialization;

namespace EcommerceApi.Models.Database
{
    public class OrderRecord : BDEntity
    {
        public string detail { get; set; }
        public DateTime date { get; set; }
        public Guid orderId { get; set; }
        public OrderStatusType orderState { get; set; }
        [JsonIgnore]
        public virtual Order? order { get; set; }
    }
}
