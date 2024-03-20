using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;

namespace EcommerceApi.Models.Database
{
    public class User
    {
        public Guid? id { get; set; }
        [MaxLength(50)]
        public string firstName { get; set; }
        [MaxLength(50)]
        public string lastName { get; set; }
        [MaxLength(50)]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string email { get; set; }
        [MaxLength(50)]
        public string password { get; set; }
        public bool? isActive { get; set; }
        public Guid roleId { get; set; }
        [JsonIgnore]
        public virtual Role? role { get; set; }
        [JsonIgnore]
        public virtual ICollection<Customer>? customers { get; set; }
    }
}
