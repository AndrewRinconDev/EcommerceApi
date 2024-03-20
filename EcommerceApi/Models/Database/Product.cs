using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace EcommerceApi.Models.Database
{
    public class Product
    {
        public Guid? id { get; set; }
        [MaxLength(50)]
        public string name { get; set; }
        [MaxLength(300)]
        public string? description { get; set; }
        public string imageUrl { get; set; }
        public double price { get; set; }
        public int stock { get; set; }
        public Guid categoryId { get; set; }
        public bool isActive { get; set; }
        [JsonIgnore]
        public virtual Category? category { get; set; }
    }
}
