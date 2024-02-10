namespace EcommerceApi.Dto
{
    public class UserDto
    {
        public string? id { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public string roleId { get; set; }
        public bool? isActive { get; set; }
    }
}
