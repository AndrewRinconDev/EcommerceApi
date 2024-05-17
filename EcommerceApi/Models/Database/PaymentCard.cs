using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;
using EcommerceApi.Models.Reference;

namespace EcommerceApi.Models.Database
{
    public class PaymentCard : BDEntity
    {
        public string number { get; set; }
        [MaxLength(16)]
        public string ownerName { get; set; }
        [MaxLength(50)]
        public CardCompanyType company { get; set; }
        public string? expirationDate{ get; set; }
        public string? cvv { get; set; }
        public string? endWith { get; set; }
        [JsonIgnore]
        public virtual Customer? customer { get; set; }
    }
}
