using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace StationeryRetailChain.Shared.Models
{
    public partial class Receipt
    { 
        public int ReceiptId { get; set; }
        public int ReceiptNumber { get; set; }
        public int SellerId { get; set; }
        public Employee? Seller { get; set; }
        public int CustomerId { get; set; }
        public Customer? Customer { get; set; }
        public DateTime PurchaseDate { get; set; }
        public decimal PurchaseSum { get; set; }
        public IEnumerable<StationerySale>? Items { get; set; }
    }
}
