using System;
using System.Collections.Generic;

namespace TestPJ.Models
{
    public partial class XeMay
    {
        public string IdXeMay { get; set; } = null!;
        public int? CongSuat { get; set; }
        public string? IdPt { get; set; }

        public virtual PhuongTien? IdPtNavigation { get; set; }
    }
}
