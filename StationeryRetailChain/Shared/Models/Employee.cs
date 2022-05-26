using System;
using System.Collections.Generic;

namespace StationeryRetailChain.Shared.Models
{
    public partial class Employee
    {
        public int? EmployeeId { get; set; }
        public string EmployeeName { get; set; } = null!;
        public string? EmployeePhoneNumber { get; set; }
        public int ShopId { get; set; }
        public StationeryShop? Shop { get; set; }
        public int JobId { get; set; }
        public Job? Job { get; set; }
    }
}
