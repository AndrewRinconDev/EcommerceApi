using System.Text.Json.Serialization;

namespace EcommerceApi.Models
{
    public class FavoriteProduct
    {
        public string? id { get; set; }
        public string customerId { get; set; }
        public bool? isActive { get; set; }
        public string productId { get; set; }
        [JsonIgnore]
        public virtual Product? product { get; set; }
    }
}
