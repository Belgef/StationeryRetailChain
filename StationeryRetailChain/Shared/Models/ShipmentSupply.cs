using System;
using System.Collections.Generic;

namespace StationeryRetailChain.Shared.Models
{
    public partial class ShipmentSupply
    {
        public int ShipmentSupplyId { get; set; }
        public int ShipmentInvoiceId { get; set; }
        public int StationeryProductId { get; set; }
        public decimal Cost { get; set; }
        public int Quantity { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
