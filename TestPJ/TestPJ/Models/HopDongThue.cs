using System;
using System.Collections.Generic;

namespace TestPJ.Models
{
    public partial class HopDongThue
    {
        public HopDongThue()
        {
            HopDongThueChiTiets = new HashSet<HopDongThueChiTiet>();
        }

        public string MaHdthue { get; set; } = null!;
        public string? Makh { get; set; }
        public string? MaNv { get; set; }
        public string? Mathue { get; set; }
        public string? IdPt { get; set; }

        public virtual PhuongTien? IdPtNavigation { get; set; }
        public virtual NhanVien? MaNvNavigation { get; set; }
        public virtual ThongTinKhachHang? MakhNavigation { get; set; }
        public virtual TrangThaiThue? MathueNavigation { get; set; }
        public virtual ICollection<HopDongThueChiTiet> HopDongThueChiTiets { get; set; }
    }
}
