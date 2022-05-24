using System;
using System.Collections.Generic;

namespace StationeryRetailChain.Shared.Models
{
    public partial class StockProduct
    {
        public int StockProductId { get; set; }
        public int StationeryProductId { get; set; }
        public StationeryProduct? StationeryProduct { get; set; }
        public int ShopId { get; set; }
        public int Quantity { get; set; }
    }
}
