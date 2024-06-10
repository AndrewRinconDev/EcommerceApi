using EcommerceApi.Models.Database;
using System.Text.Json.Serialization;

namespace EcommerceApi.Models.Dto
{
    public class FavoriteProductDto : BDEntityDto
    {
        public Guid customerId { get; set; }
        public Guid principalProductId { get; set; }
        [JsonIgnore]
        public virtual PrincipalProductDto? principalProduct { get; set; }
    }
}
