using System.Text.Json.Serialization;

namespace EcommerceApi.Models.Database
{
    public class Order : BDEntity
    {
        public double subtotal { get; set; }
        public double total { get; set; }
        public bool isPaid { get; set; }
        public DateTime creationOn { get; set; }
        public DateTime updateOn { get; set; }
        public Guid customerId { get; set; }
        public Guid addressId { get; set; }
        public bool isActive { get; set; } = true;
        [JsonIgnore]
        public virtual Customer? customer { get; set; }
        [JsonIgnore]
        public virtual Address? address { get; set; }
        [JsonIgnore]
        public virtual ICollection<Payment>? payments { get; set; }
        [JsonIgnore]
        public virtual ICollection<OrderRecord>? orderRecords { get; set; }
        [JsonIgnore]
        public virtual ICollection<OrderProduct>? orderProducts { get; set; }
    }
}
