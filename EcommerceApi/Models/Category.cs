using System.Text.Json.Serialization;

namespace EcommerceApi.Models
{
    public class Category
    {
        public string? id { get; set; }
        public string name { get; set; }
        public string? parentId { get; set; }
        public bool? isActive { get; set; }
        [JsonIgnore]
        public virtual Category? parent { get; set; }
        [JsonIgnore]
        public virtual ICollection<Category>? subcategories { get; set; }
    }
}
