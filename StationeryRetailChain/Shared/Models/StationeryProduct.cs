using System;
using System.Collections.Generic;

namespace StationeryRetailChain.Shared.Models
{
    public partial class StationeryProduct
    {
        public int StationeryProductId { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public string BarCode { get; set; } = null!;
        public int ManufacturerId { get; set; }
        public decimal Price { get; set; }
        public int TypeId { get; set; }
        public int? Length { get; set; }
        public int? Width { get; set; }
        public int? Height { get; set; }
        public int? ItemsPerPackage { get; set; }
        public int? PackagesPerBox { get; set; }
        public int? ColorId { get; set; }
        public int MinimumAge { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
