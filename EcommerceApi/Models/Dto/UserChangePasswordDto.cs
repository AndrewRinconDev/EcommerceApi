namespace EcommerceApi.Models.Dto
{
    public class UserChangePasswordDto
    {
        public Guid id { get; set; }
        public string oldPassword { get; set; }
        public string newPassword { get; set; }
    }
}
