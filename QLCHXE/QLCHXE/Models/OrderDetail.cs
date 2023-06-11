using System;
using System.Collections.Generic;

namespace QLCHXE.Models
{
    public partial class OrderDetail
    {
        public string? TenXeMua { get; set; }
        public double? Gia { get; set; }
        public int? SoLuongMua { get; set; }
        public string Id { get; set; } = null!;
    }
}
