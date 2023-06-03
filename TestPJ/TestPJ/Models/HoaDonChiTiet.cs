using System;
using System.Collections.Generic;

namespace TestPJ.Models
{
    public partial class HoaDonChiTiet
    {
        public string MaHd { get; set; } = null!;
        public string IdPt { get; set; } = null!;
        public int? Soluong { get; set; }

        public virtual PhuongTien IdPtNavigation { get; set; } = null!;
        public virtual HoaDon MaHdNavigation { get; set; } = null!;
    }
}
