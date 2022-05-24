using System;
using System.Collections.Generic;

namespace StationeryRetailChain.Shared.Models
{
    public partial class Customer
    {
        public int CustomerId { get; set; }
        public string? CustomerName { get; set; }
        public string? CustomerPhoneNumber { get; set; }
        public string? StreetAddress { get; set; }
        public int CityId { get; set; }
        public City? City { get; set; }
        public override string ToString()
        {
            return $"{CustomerName}, {CustomerPhoneNumber}, {City?.CityName}";
        }
    }
}
