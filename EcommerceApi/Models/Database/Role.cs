using System.Text.Json.Serialization;

namespace EcommerceApi.Models.Database
{
    public class Role : BDEntity
    {
        public string name { get; set; }
        public bool? isActive { get; set; } = true;
        [JsonIgnore]
        public virtual ICollection<Permission>? permissions { get; set; }
    }
}
