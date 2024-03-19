﻿using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace EcommerceApi.Models
{
    public class Role
    {
        public Guid? id { get; set; }
        [MaxLength(50)]
        public string name { get; set; }
    }
}
