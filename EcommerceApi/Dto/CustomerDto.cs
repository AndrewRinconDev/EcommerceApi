namespace EcommerceApi.Dto
{
    public class CustomerDto
    {
        public string? id { get; set; }
        public string identityType { get; set; }
        public string identityNumber { get; set; }
        public string phoneNumber { get; set; }
        public string? userId { get; set; }
        public bool? isActive { get; set; }
    }
}
