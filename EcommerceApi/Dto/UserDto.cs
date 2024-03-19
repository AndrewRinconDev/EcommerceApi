namespace EcommerceApi.Dto
{
    public class UserDto
    {
        public Guid? id { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public Guid roleId { get; set; }
        public bool? isActive { get; set; }
    }
}
