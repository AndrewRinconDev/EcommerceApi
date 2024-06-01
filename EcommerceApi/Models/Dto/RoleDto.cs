namespace EcommerceApi.Models.Dto
{
    public class RoleDto : BDEntityDto
    {
        public string name { get; set; }
        public bool? isActive { get; set; } = true;
        public List<string>? permissionList { get; set; }
    }
}
