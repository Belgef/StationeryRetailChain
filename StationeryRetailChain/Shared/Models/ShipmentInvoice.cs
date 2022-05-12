using System;
using System.Collections.Generic;

namespace StationeryRetailChain.Shared.Models
{
    public partial class ShipmentInvoice
    {
        public int ShipmentInvoiceId { get; set; }
        public int ShipmentInvoiceNumber { get; set; }
        public int SupplierId { get; set; }
        public DateTime CreationDate { get; set; }
        public int AuthorId { get; set; }
        public decimal TotalSum { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
