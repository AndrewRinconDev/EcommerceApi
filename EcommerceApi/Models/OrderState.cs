namespace EcommerceApi.Models
{
    public class OrderState
    {
        public string? id { get; set; }
        public string name { get; set; }
        public virtual ICollection<OrderRecord> orderRecords { get; set; }
    }
}
