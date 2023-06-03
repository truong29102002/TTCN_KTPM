using System;
using System.Collections.Generic;

namespace TestPJ.Models
{
    public partial class HoaDon
    {
        public HoaDon()
        {
            HoaDonChiTiets = new HashSet<HoaDonChiTiet>();
        }

        public string MaHd { get; set; } = null!;
        public DateTime? Ngaylap { get; set; }
        public string? MaNv { get; set; }
        public string? MaKh { get; set; }

        public virtual ThongTinKhachHang? MaKhNavigation { get; set; }
        public virtual NhanVien? MaNvNavigation { get; set; }
        public virtual ICollection<HoaDonChiTiet> HoaDonChiTiets { get; set; }
    }
}
