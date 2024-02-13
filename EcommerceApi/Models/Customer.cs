using System.Text.Json.Serialization;

namespace EcommerceApi.Models
{
    public class Customer
    {
        public string? id { get; set; }
        public string identityType { get; set; }
        public string identityNumber { get; set; }
        public string phoneNumber { get; set; }
        public string? userId { get; set; }
        public bool isActive { get; set; }
        [JsonIgnore]
        public virtual User user { get; set; }
        [JsonIgnore]
        public virtual ICollection<Address> addresses { get; set; }
        [JsonIgnore]
        public virtual ICollection<FavoriteProduct> favoriteProducts { get; set; }
    }
}
