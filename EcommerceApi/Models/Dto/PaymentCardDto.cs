using System.ComponentModel.DataAnnotations;
using EcommerceApi.Models.Reference;

namespace EcommerceApi.Models.Dto
{
    public class PaymentCardDto : BDEntityDto
    {
        public string number { get; set; }
        [MaxLength(16)]
        public string ownerName { get; set; }
        [MaxLength(50)]
        public CardCompanyType company { get; set; }
        public string? expirationDate{ get; set; }
        public string? cvv { get; set; }
        public string? endWith { get; set; }
    }
}
