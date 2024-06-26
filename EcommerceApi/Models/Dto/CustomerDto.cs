﻿using System.Text.Json.Serialization;

namespace EcommerceApi.Models.Dto
{
    public class CustomerDto
    {
        public Guid? id { get; set; }
        public string identityType { get; set; }
        public string identityNumber { get; set; }
        public string phoneNumber { get; set; }
        public Guid? userId { get; set; }
        public bool? isActive { get; set; }
        [JsonIgnore]
        public virtual UserDto? user { get; set; }
        [JsonIgnore]
        public virtual ICollection<AddressDto>? addresses { get; set; }
        [JsonIgnore]
        public virtual ICollection<OrderDto>? orders { get; set; }
        [JsonIgnore]
        public virtual ICollection<FavoriteProductDto>? favoriteProducts { get; set; }
        [JsonIgnore]
        public virtual ICollection<PaymentCardDto>? paymentCards { get; set; }
    }
}
