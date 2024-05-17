using System.Text.Json.Serialization;

namespace EcommerceApi.Models.Database
{
    public class FeatureProduct : BDEntity
    {
        public Guid ProductId { get; set; }
        public Guid featureId { get; set; }
        [JsonIgnore]
        public virtual Product? product { get; set; }
        public virtual Feature? feature { get; set; }
    }
}
