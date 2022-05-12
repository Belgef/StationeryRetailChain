using System;
using System.Collections.Generic;

namespace StationeryRetailChain.Shared.Models
{
    public partial class Manufacturer
    {
        public int ManufacturerId { get; set; }
        public string ManufacturerName { get; set; } = null!;
        public string? PhoneNumber { get; set; }
        public int? CountryId { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
