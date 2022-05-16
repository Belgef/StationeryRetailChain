using System;
using System.Collections.Generic;

namespace StationeryRetailChain.Shared.Models
{
    public partial class State
    {
        public int StateId { get; set; }
        public string StateName { get; set; } = null!;
        public int CountryId { get; set; }
        public Country? Country { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
