using System;
using System.Collections.Generic;

namespace StationeryRetailChain.Shared.Models
{
    public partial class Offer
    {
        public int OfferId { get; set; }
        public string OfferName { get; set; } = null!;
        public string? OfferDescription { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
