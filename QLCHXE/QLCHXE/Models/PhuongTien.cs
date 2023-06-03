using System;
using System.Collections.Generic;

namespace QLCHXE.Models
{
    public partial class PhuongTien
    {
        public PhuongTien()
        {
            HoaDonChiTiets = new HashSet<HoaDonChiTiet>();
            HopDongThueChiTiets = new HashSet<HopDongThueChiTiet>();
            HopDongThues = new HashSet<HopDongThue>();
            Otos = new HashSet<Oto>();
            PhieuNhapChiTiets = new HashSet<PhieuNhapChiTiet>();
            XeMays = new HashSet<XeMay>();
            XeTais = new HashSet<XeTai>();
        }

        public string IdPt { get; set; } = null!;
        public int? NamSx { get; set; }
        public double? Gia { get; set; }
        public string? Mamau { get; set; }
        public string? TenXe { get; set; }
        public DateTime? Ngaynhap { get; set; }
        public int? Soluong { get; set; }
        public string? Mota { get; set; }
        public string? Donvi { get; set; }
        public string? IdHangXe { get; set; }

        public virtual HangXe? IdHangXeNavigation { get; set; }
        public virtual Mau? MamauNavigation { get; set; }
        public virtual ICollection<HoaDonChiTiet> HoaDonChiTiets { get; set; }
        public virtual ICollection<HopDongThueChiTiet> HopDongThueChiTiets { get; set; }
        public virtual ICollection<HopDongThue> HopDongThues { get; set; }
        public virtual ICollection<Oto> Otos { get; set; }
        public virtual ICollection<PhieuNhapChiTiet> PhieuNhapChiTiets { get; set; }
        public virtual ICollection<XeMay> XeMays { get; set; }
        public virtual ICollection<XeTai> XeTais { get; set; }
    }
}
