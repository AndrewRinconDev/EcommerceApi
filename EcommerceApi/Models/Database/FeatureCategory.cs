using System.Text.Json.Serialization;

namespace EcommerceApi.Models.Database
{
    public class FeatureCategory : BDEntity
    {
        public string? label { get; set; }
        public string? name { get; set; }
        public string? description { get; set; }
        [JsonIgnore]
        public virtual ICollection<Feature>? features { get; set; }
    }
}
