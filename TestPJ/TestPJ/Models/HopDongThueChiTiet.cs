using System;
using System.Collections.Generic;

namespace TestPJ.Models
{
    public partial class HopDongThueChiTiet
    {
        public string MaHdthue { get; set; } = null!;
        public string IdPt { get; set; } = null!;
        public int? Soluongthue { get; set; }

        public virtual PhuongTien IdPtNavigation { get; set; } = null!;
        public virtual HopDongThue MaHdthueNavigation { get; set; } = null!;
    }
}
