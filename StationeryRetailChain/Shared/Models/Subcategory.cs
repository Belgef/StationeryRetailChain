using System;
using System.Collections.Generic;

namespace StationeryRetailChain.Shared.Models
{
    public partial class Subcategory
    {
        public int? SubcategoryId { get; set; }
        public string SubcategoryName { get; set; } = null!;
        public int CategoryId { get; set; }
        public Category? Category { get; set; }
    }
}
