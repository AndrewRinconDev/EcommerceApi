namespace EcommerceApi.Models.Dto
{
    public class OrderProductDto : BDEntityDto
    {
        public int quantity { get; set; }
        public Guid orderId { get; set; }
        public Guid productId { get; set; }
    }
}
