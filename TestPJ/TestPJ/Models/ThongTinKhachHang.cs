using System;
using System.Collections.Generic;

namespace TestPJ.Models
{
    public partial class ThongTinKhachHang
    {
        public ThongTinKhachHang()
        {
            HoaDons = new HashSet<HoaDon>();
            HopDongThues = new HashSet<HopDongThue>();
        }

        public string MaKh { get; set; } = null!;
        public string? TenKh { get; set; }
        public string? DiaChi { get; set; }
        public string? Sodienthoai { get; set; }
        public int IdKh { get; set; }

        public virtual ICollection<HoaDon> HoaDons { get; set; }
        public virtual ICollection<HopDongThue> HopDongThues { get; set; }
    }
}
