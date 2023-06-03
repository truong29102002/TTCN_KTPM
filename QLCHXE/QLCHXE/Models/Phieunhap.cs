using System;
using System.Collections.Generic;

namespace QLCHXE.Models
{
    public partial class Phieunhap
    {
        public Phieunhap()
        {
            PhieuNhapChiTiets = new HashSet<PhieuNhapChiTiet>();
        }

        public string Maphieunhap { get; set; } = null!;
        public DateTime? Ngaynhap { get; set; }
        public string? MaNcc { get; set; }
        public string? MaNv { get; set; }

        public virtual NhaCungCap? MaNccNavigation { get; set; }
        public virtual NhanVien? MaNvNavigation { get; set; }
        public virtual ICollection<PhieuNhapChiTiet> PhieuNhapChiTiets { get; set; }
    }
}
