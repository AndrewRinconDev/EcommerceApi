using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace EcommerceApi.Models.Database
{
    public class Address : BDEntity
    {
        [MaxLength(100)]
        public string address { get; set; }
        [MaxLength(50)]
        public string city { get; set; }
        [MaxLength(50)]
        public string state { get; set; }
        public int zipCode { get; set; }
        public bool? isActive { get; set; }
        public Guid customerId { get; set; }
        [JsonIgnore]
        public virtual Customer? customer { get; set; }
    }
}
