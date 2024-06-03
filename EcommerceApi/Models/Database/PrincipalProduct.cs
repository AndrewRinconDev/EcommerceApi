using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace EcommerceApi.Models.Database
{
    public class PrincipalProduct : BDEntity
    {
        [MaxLength(50)]
        public string name { get; set; }
        [MaxLength(300)]
        public string? description { get; set; }
        public Guid categoryId { get; set; }
        public bool isActive { get; set; } = true;
        [JsonIgnore]
        public virtual Category? category { get; set; }
        [JsonIgnore]
        public virtual ICollection<Product>? products { get; set; }
    }
}
