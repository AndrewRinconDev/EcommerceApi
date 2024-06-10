using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace EcommerceApi.Models.Dto
{
    public class PrincipalProductDto : BDEntityDto
    {
        [MaxLength(50)]
        public string name { get; set; }
        [MaxLength(300)]
        public string? description { get; set; }
        public Guid categoryId { get; set; }
        public bool isActive { get; set; } = true;
        [JsonIgnore]
        public virtual CategoryDto? category { get; set; }
        [JsonIgnore]
        public virtual ICollection<ProductDto>? products { get; set; }
    }
}
