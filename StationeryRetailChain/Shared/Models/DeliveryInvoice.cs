using System;
using System.Collections.Generic;

namespace StationeryRetailChain.Shared.Models
{
    public partial class DeliveryInvoice
    {
        public int DeliveryInvoiceId { get; set; }
        public int? DeliveryInvoiceNumber { get; set; }
        public int ShipmentInvoiceId { get; set; }
        public ShipmentInvoice? ShipmentInvoice { get; set; }
        public int ShopId { get; set; }
        public StationeryShop? Shop { get; set; }
        public DateTime? CreationDate { get; set; }
        public int AuthorId { get; set; }
        public Employee? Author { get; set; }
        public IEnumerable<StationerySupply>? StationerySupplies { get; set; }

    }
}
