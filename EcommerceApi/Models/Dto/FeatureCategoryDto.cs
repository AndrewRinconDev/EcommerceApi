using System.Text.Json.Serialization;

namespace EcommerceApi.Models.Dto
{
    public class FeatureCategoryDto : BDEntityDto
    {
        public string? label { get; set; }
        public string? name { get; set; }
        public string? description { get; set; }
    }
}
