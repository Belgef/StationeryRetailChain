using System;
using System.Collections.Generic;

namespace StationeryRetailChain.Shared.Models
{
    public partial class ShipmentInvoice
    {
        public int ShipmentInvoiceId { get; set; }
        public int ShipmentInvoiceNumber { get; set; }
        public int SupplierId { get; set; }
        public SupplierCompany? Supplier { get; set; }
        public DateTime CreationDate { get; set; }
        public int AuthorId { get; set; }
        public Employee? Author { get; set; }
        public decimal TotalSum { get; set; }
        public IEnumerable<ShipmentSupply>? ShipmentSupplies { get; set; }

        public override string ToString()
        {
            return $"Накладна №{ShipmentInvoiceNumber} за {CreationDate} на {TotalSum} грн";
        }
    }
}
