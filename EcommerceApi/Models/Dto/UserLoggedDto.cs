namespace EcommerceApi.Models.Dto
{
    public class UserLoggedDto : BDEntityDto
    {
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public Guid roleId { get; set; }
        public bool? isActive { get; set; }
        public string? token { get; set; }
        public List<string>? permissions { get; set; }
    }
}
