using System;
using System.Collections.Generic;

namespace TestPJ.Models
{
    public partial class NhanVien
    {
        public NhanVien()
        {
            HoaDons = new HashSet<HoaDon>();
            HopDongThues = new HashSet<HopDongThue>();
            Phieunhaps = new HashSet<Phieunhap>();
        }

        public string MaNv { get; set; } = null!;
        public string? TenNv { get; set; }
        public DateTime? NgaySinh { get; set; }
        public string? DiaChiNv { get; set; }
        public string? Sodienthoai { get; set; }
        public string? Taikhoan { get; set; }
        public string? Gender { get; set; }

        public virtual Account? TaikhoanNavigation { get; set; }
        public virtual ICollection<HoaDon> HoaDons { get; set; }
        public virtual ICollection<HopDongThue> HopDongThues { get; set; }
        public virtual ICollection<Phieunhap> Phieunhaps { get; set; }
    }
}
