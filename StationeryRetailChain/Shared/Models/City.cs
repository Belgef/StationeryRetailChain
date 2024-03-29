﻿using System;
using System.Collections.Generic;

namespace StationeryRetailChain.Shared.Models
{
    public partial class City
    {
        public int? CityId { get; set; }
        public string CityName { get; set; } = null!;
        public int StateId { get; set; }
        public State? State { get; set; }
    }
}
