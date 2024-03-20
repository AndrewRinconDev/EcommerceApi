using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;

namespace EcommerceApi.Models.Database
{
    public class Customer
    {
        public Guid? id { get; set; }
        [MaxLength(50)]
        public string identityType { get; set; }
        [MaxLength(50)]
        public string identityNumber { get; set; }
        [Length(10, 10, ErrorMessage = "Phone number must be 10 digits")]
        public string phoneNumber { get; set; }
        public Guid? userId { get; set; }
        public bool isActive { get; set; }
        [JsonIgnore]
        public virtual User user { get; set; }
        [JsonIgnore]
        public virtual ICollection<Address> addresses { get; set; }
        [JsonIgnore]
        public virtual ICollection<Order> orders { get; set; }
        [JsonIgnore]
        public virtual ICollection<FavoriteProduct> favoriteProducts { get; set; }
    }
}
