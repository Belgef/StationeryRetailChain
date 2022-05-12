using System;
using System.Collections.Generic;

namespace StationeryRetailChain.Shared.Models
{
    public partial class Color
    {
        public int ColorId { get; set; }
        public string ColorName { get; set; } = null!;
        public string ColorHex { get; set; } = null!;
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
