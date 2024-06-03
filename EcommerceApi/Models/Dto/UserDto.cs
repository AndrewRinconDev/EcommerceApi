using EcommerceApi.Models.Database;
using System.Text.Json.Serialization;

namespace EcommerceApi.Models.Dto
{
    public class UserDto : BDEntityDto
    {
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public Guid roleId { get; set; }
        public bool? isActive { get; set; }
        [JsonIgnore]
        public virtual RoleDto? role { get; set; }
    }
}
