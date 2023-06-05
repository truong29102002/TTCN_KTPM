using System;
using System.Collections.Generic;

namespace QLCHXE.Models
{
    public partial class ThongTinKhachHang
    {
        public string MaKh { get; set; } = null!;
        public string? TenKh { get; set; }
        public string? DiaChi { get; set; }
        public string? Sodienthoai { get; set; }
        public int IdKh { get; set; }
    }
}
