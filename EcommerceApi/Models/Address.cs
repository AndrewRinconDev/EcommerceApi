using System.Text.Json.Serialization;

namespace EcommerceApi.Models
{
    public class Address
    {
        public string? id { get; set; }
        public string address { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public int zipCode { get; set; }
        public bool? isActive { get; set; }
        public string customerId { get; set; }
        [JsonIgnore]
        public virtual Customer? customer { get; set; }
    }
}
