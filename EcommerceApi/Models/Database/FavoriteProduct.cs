using System.Text.Json.Serialization;

namespace EcommerceApi.Models.Database
{
    public class FavoriteProduct : BDEntity
    {
        public Guid customerId { get; set; }
        public Guid principalProductId { get; set; }
        [JsonIgnore]
        public virtual PrincipalProduct? principalProduct { get; set; }
        [JsonIgnore]
        public virtual Customer? customer { get; set; }
    }
}
