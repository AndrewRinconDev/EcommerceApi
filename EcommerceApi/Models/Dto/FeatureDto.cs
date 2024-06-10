using System.Text.Json.Serialization;

namespace EcommerceApi.Models.Dto
{
    public class FeatureDto : BDEntityDto
    {
        public string? label { get; set; }
        public string? color { get; set; }
        public Guid featureCategoryId { get; set; }
        [JsonIgnore]
        public virtual FeatureCategoryDto? featureCategory { get; set; }
    }
}
