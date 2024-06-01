using EcommerceApi.Models.Reference;

namespace EcommerceApi.Models.Dto
{
    public class PermissionDto : BDEntityDto
    {
        public string? name { get; set; }
        public PermissionType? permissionType { get; set; }
        public Guid roleId { get; set; }
    }
}
