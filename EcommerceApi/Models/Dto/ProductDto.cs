using System.Text.Json.Serialization;

namespace EcommerceApi.Models.Dto
{
    public class ProductDto : BDEntityDto
    {
        public string imageUrl { get; set; }
        public double price { get; set; }
        public int stock { get; set; }
        public Guid principalProductId { get; set; }
        public bool isActive { get; set; } = true;
        [JsonIgnore]
        public virtual PrincipalProductDto? principalProduct { get; set; }
        [JsonIgnore]
        public virtual ICollection<FeatureProductDto>? featureProducts { get; set; }
    }
}
