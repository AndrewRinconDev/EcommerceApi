using EcommerceApi.Dto;
using System.Text.Json.Serialization;

namespace EcommerceApi.Models
{
    public class User
    {
        public string? id { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public bool? isActive { get; set; }
        public string roleId { get; set; }
        [JsonIgnore]
        public virtual Role? role { get; set; }
        [JsonIgnore]
        public virtual ICollection<Customer>? customers { get; set; }
    }
}
