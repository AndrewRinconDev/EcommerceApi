using EcommerceApi.Models.Reference;
using System.Text.Json.Serialization;

namespace EcommerceApi.Models.Database
{
    public class Payment : BDEntity
    {
        public string description { get; set; }
        public DateTime date { get; set; }
        public double ammount { get; set; }
        public PaymentMethodType? method { get; set; }
        public PaymentStatusType? status { get; set; }
        public string? detail { get; set; }
        public Guid customerId { get; set; }
        public Guid orderId { get; set; }
        [JsonIgnore]
        public virtual Customer? customer { get; set; }
        [JsonIgnore]
        public virtual Order? order { get; set; }
    }
}
