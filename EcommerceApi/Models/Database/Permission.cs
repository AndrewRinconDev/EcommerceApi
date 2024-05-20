using System.Text.Json.Serialization;

namespace EcommerceApi.Models.Database
{
    public class Permission : BDEntity
    {
        public string? name { get; set; }
        //public PermissionType? permissionType { get; set; }
        public Guid roleId { get; set; }
        [JsonIgnore]
        public virtual Role? role { get; set; }
    }
}
