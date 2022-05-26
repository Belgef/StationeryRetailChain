using System;
using System.Collections.Generic;

namespace StationeryRetailChain.Shared.Models
{
    public partial class User
    {
        public string? UserId { get; set; }
        public string UserName { get; set; } = null!;
        public string Email { get; set; }
    }
}
