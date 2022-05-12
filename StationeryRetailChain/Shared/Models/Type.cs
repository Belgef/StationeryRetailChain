using System;
using System.Collections.Generic;

namespace StationeryRetailChain.Shared.Models
{
    public partial class Type
    {
        public int TypeId { get; set; }
        public string TypeName { get; set; } = null!;
        public int SubcategoryId { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
