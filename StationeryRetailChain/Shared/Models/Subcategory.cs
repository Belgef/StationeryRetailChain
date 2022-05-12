using System;
using System.Collections.Generic;

namespace StationeryRetailChain.Shared.Models
{
    public partial class Subcategory
    {
        public int SubcategoryId { get; set; }
        public string SubcategoryName { get; set; } = null!;
        public int CategoryId { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
