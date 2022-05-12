﻿using System;
using System.Collections.Generic;

namespace StationeryRetailChain.Shared.Models
{
    public partial class Job
    {
        public int JobId { get; set; }
        public string JobName { get; set; } = null!;
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
