using System.Text.Json.Serialization;

namespace EcommerceApi.Models.Dto
{
    public class CategoryDto : BDEntityDto
    {
        public string name { get; set; }
        public Guid? parentId { get; set; }
        public bool? isActive { get; set; }
        [JsonIgnore]
        public virtual CategoryDto? parent { get; set; }
        [JsonIgnore]
        public virtual ICollection<CategoryDto>? subcategories { get; set; }
    }
}
