using System.Text.Json.Serialization;

namespace EcommerceApi.Models.Dto
{
    public class FeatureProductDto : BDEntityDto
    {
        public Guid productId { get; set; }
        public Guid featureId { get; set; }
        [JsonIgnore]
        public virtual ProductDto? product { get; set; }
        [JsonIgnore]
        public virtual FeatureDto? feature { get; set; }
    }
}
