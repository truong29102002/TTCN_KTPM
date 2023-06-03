using System;
using System.Collections.Generic;

namespace TestPJ.Models
{
    public partial class XeTai
    {
        public string XeTai1 { get; set; } = null!;
        public double? Trongtai { get; set; }
        public string? IdPt { get; set; }

        public virtual PhuongTien? IdPtNavigation { get; set; }
    }
}
