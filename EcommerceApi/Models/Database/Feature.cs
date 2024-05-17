using System.Text.Json.Serialization;

namespace EcommerceApi.Models.Database
{
    public class Feature : BDEntity
    {
        public string? label { get; set; }
        public string? color { get; set; }
        public Guid featureCategoryId { get; set; }
        [JsonIgnore]
        public virtual FeatureCategory? featureCategory { get; set; }
        [JsonIgnore]
        public virtual ICollection<FeatureProduct>? featureProducts { get; set; }
    }
}
