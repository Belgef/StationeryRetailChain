using System;
using System.Collections.Generic;

namespace StationeryRetailChain.Shared.Models
{
    public partial class OfferStationery
    {
        public int OfferStationeryId { get; set; }
        public int OfferId { get; set; }
        public decimal Discount { get; set; }
        public int ProductId { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
