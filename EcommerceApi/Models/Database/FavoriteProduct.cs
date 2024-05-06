using System.Text.Json.Serialization;

namespace EcommerceApi.Models.Database
{
    public class FavoriteProduct : BDEntity
    {
        public Guid customerId { get; set; }
        public Guid productId { get; set; }
        [JsonIgnore]
        public virtual Product? product { get; set; }
        [JsonIgnore]
        public virtual Customer? customer { get; set; }
    }
}
