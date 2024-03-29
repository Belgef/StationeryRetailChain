﻿using System;
using System.Collections.Generic;

namespace StationeryRetailChain.Shared.Models
{
    public partial class StationerySupply
    {
        public int StationerySupplyId { get; set; }
        public int DeliveryInvoiceId { get; set; }
        public int ShipmentSupplyId { get; set; }
        public ShipmentSupply? ShipmentSupply { get; set; }
        public int StockProductId { get; set; }
        public StockProduct? StockProduct { get; set; }
        public int Quantity { get; set; }
    }
}
