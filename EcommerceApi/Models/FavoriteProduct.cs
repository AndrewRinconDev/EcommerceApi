using System.Text.Json.Serialization;

namespace EcommerceApi.Models
{
    public class FavoriteProduct
    {
        public Guid? id { get; set; }
        public Guid customerId { get; set; }
        public bool? isActive { get; set; }
        public Guid productId { get; set; }
        [JsonIgnore]
        public virtual Product? product { get; set; }
        [JsonIgnore]
        public virtual Customer? customer { get; set; }
    }
}
