using System;
using System.Collections.Generic;

namespace TestPJ.Models
{
    public partial class PhieuNhapChiTiet
    {
        public string Maphieunhap { get; set; } = null!;
        public string IdPt { get; set; } = null!;
        public int? Soluongnhap { get; set; }
        public double? Dongianhap { get; set; }
        public string? GhiChu { get; set; }

        public virtual PhuongTien IdPtNavigation { get; set; } = null!;
        public virtual Phieunhap MaphieunhapNavigation { get; set; } = null!;
    }
}
