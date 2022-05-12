﻿using System;
using System.Collections.Generic;

namespace StationeryRetailChain.Shared.Models
{
    public partial class StationerySale
    {
        public int SaleId { get; set; }
        public int ReceiptId { get; set; }
        public int StockProductId { get; set; }
        public int SellQuantity { get; set; }
        public decimal SellPrice { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
