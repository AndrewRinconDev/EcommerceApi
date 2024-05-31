using System.Text.Json.Serialization;

namespace EcommerceApi.Models.Database
{
    public class Role : BDEntity
    {
        public string name { get; set; }
        [JsonIgnore]
        public virtual ICollection<Permission>? permissions { get; set; }
    }
}
