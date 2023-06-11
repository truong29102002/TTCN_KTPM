using System;
using System.Collections.Generic;

namespace QLCHXE.Models
{
    public partial class Order
    {
        public string Id { get; set; } = null!;
        public string? NhanVienBanHang { get; set; }
        public DateTime? NgayBan { get; set; }
    }
}
