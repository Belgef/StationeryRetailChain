using System;
using System.Collections.Generic;

namespace StationeryRetailChain.Shared.Models
{
    public partial class StationeryShop
    {
        public int ShopId { get; set; }
        public string? PhoneNumber { get; set; }
        public string? StreetAddress { get; set; }
        public int? CityId { get; set; }
        public City? City { get; set; }
    }
}
