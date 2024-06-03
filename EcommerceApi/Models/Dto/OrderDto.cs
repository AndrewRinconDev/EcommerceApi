namespace EcommerceApi.Models.Dto
{
    public class OrderDto : BDEntityDto
    {
        public double subtotal { get; set; }
        public double total { get; set; }
        public bool isPaid { get; set; }
        public DateTime creationOn { get; set; }
        public DateTime updateOn { get; set; }
        public Guid customerId { get; set; }
        public Guid addressId { get; set; }
        public bool isActive { get; set; } = true;
    }
}
