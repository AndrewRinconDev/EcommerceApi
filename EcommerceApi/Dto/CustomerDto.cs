namespace EcommerceApi.Dto
{
    public class CustomerDto
    {
        public Guid? id { get; set; }
        public string identityType { get; set; }
        public string identityNumber { get; set; }
        public string phoneNumber { get; set; }
        public Guid? userId { get; set; }
        public bool? isActive { get; set; }
    }
}
