using System;
using System.Collections.Generic;

namespace QLCHXE.Models
{
    public partial class PhuongTien
    {
        public PhuongTien()
        {
            Otos = new HashSet<Oto>();
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
        public virtual ICollection<Oto> Otos { get; set; }
        public virtual ICollection<XeMay> XeMays { get; set; }
        public virtual ICollection<XeTai> XeTais { get; set; }
    }
}
