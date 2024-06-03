namespace EcommerceApi.Models.Dto
{
    public class AddressDto : BDEntityDto
    {
        public string address { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public int zipCode { get; set; }
        public bool? isActive { get; set; }
        public Guid customerId { get; set; }
    }
}
