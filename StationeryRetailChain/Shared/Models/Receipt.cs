using System;
using System.Collections.Generic;

namespace StationeryRetailChain.Shared.Models
{
    public partial class Receipt
    {
        public int ReceiptId { get; set; }
        public int ReceiptNumber { get; set; }
        public int SellerId { get; set; }
        public int CustomerId { get; set; }
        public Customer? Customer { get; set; }
        public DateTime PurchaseDate { get; set; }
        public decimal PurchaseSum { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
