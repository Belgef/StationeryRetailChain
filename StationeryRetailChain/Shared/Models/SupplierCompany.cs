using System;
using System.Collections.Generic;

namespace StationeryRetailChain.Shared.Models
{
    public partial class SupplierCompany
    {
        public int SupplierId { get; set; }
        public string? SupplierName { get; set; }
        public string? PhoneNumber { get; set; }
        public string? StreetAddress { get; set; }
        public int? CityId { get; set; }
    }
}
