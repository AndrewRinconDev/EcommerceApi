using System.ComponentModel.DataAnnotations;

namespace EcommerceApi.Models.Database
{
    public class OrderState
    {
        public Guid? id { get; set; }
        [MaxLength(50)]
        public string name { get; set; }
    }
}
