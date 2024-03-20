using System.Collections.Specialized;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace EcommerceApi.Models.Database
{
    public class OrderRecord
    {
        public Guid? id { get; set; }
        [MaxLength(100)]
        public string detail { get; set; }
        public DateTime date { get; set; }
        public Guid orderId { get; set; }
        public Guid orderStateId { get; set; }
        [JsonIgnore]
        public virtual Order? order { get; set; }
        [JsonIgnore]
        public virtual OrderState? orderState { get; set; }
    }
}
