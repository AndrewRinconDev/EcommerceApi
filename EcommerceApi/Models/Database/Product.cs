using System.Text.Json.Serialization;

namespace EcommerceApi.Models.Database
{
    public class Product : BDEntity
    {
        public string imageUrl { get; set; }
        public double price { get; set; }
        public int stock { get; set; }
        public Guid principalProductId { get; set; }
        public bool isActive { get; set; }
        [JsonIgnore]
        public virtual PrincipalProduct? principalProduct { get; set; }
        [JsonIgnore]
        public virtual ICollection<FeatureProduct>? featureProducts { get; set; }
    }
}
