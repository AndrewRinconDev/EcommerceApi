using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace EcommerceApi.Models.Database
{
    public class Category
    {
        public Guid? id { get; set; }
        [MaxLength(50)]
        public string name { get; set; }
        public Guid? parentId { get; set; }
        public bool? isActive { get; set; }
        [JsonIgnore]
        public virtual Category? parent { get; set; }
        [JsonIgnore]
        public virtual ICollection<Category>? subcategories { get; set; }
    }
}
