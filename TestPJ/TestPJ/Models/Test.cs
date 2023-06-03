using System;
using System.Collections.Generic;

namespace TestPJ.Models
{
    public partial class Test
    {
        public string MaHd { get; set; } = null!;
        public DateTime? Ngaylap { get; set; }
        public string? TenXe { get; set; }
        public string? TenNv { get; set; }
        public string? TenKh { get; set; }
        public int? Soluong { get; set; }
        public double? Gia { get; set; }
        public double? ThanhTien { get; set; }
    }
}
