using System;
using System.Collections.Generic;

namespace QLCHXE.Models
{
    public partial class XeMayKho
    {
        public string IdXmkho { get; set; } = null!;
        public int? CongSuat { get; set; }
        public string? IdPt { get; set; }

        public virtual KhoPhuongTien? IdPtNavigation { get; set; }
    }
}
