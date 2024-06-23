using EcommerceApi.Models.Reference;
using System.Text.Json.Serialization;

namespace EcommerceApi.Models.Dto
{
    public class OrderHistoryDto : BDEntityDto
    {
        public string detail { get; set; }
        public DateTime date { get; set; }
        public Guid orderId { get; set; }
        public OrderStatusType orderState { get; set; }
        [JsonIgnore]
        public virtual OrderDto? order { get; set; }
    }
}
