namespace EcommerceApi.Models
{
    public class Order
    {
        public string? id { get; set; }
        public double subtotal { get; set; }
        public double total { get; set; }
        public bool isPaid { get; set; }
        public DateTime creationDates { get; set; }
        public DateTime dateUpdate { get; set; }
        public string customerId { get; set; }
        public string addressId { get; set; }
        public bool isActive { get; set; }
        public virtual Customer customer { get; set; }
        public virtual Address address { get; set; }
        public virtual ICollection<OrderRecord> orderRecords { get; set; }
        public virtual ICollection<OrderProduct> orderProducts { get; set; }
    }
}
