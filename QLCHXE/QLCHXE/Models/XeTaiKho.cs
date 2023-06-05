using System;
using System.Collections.Generic;

namespace QLCHXE.Models
{
    public partial class XeTaiKho
    {
        public string IdXeTaiKho { get; set; } = null!;
        public double? TrongTai { get; set; }
        public string? IdPt { get; set; }

        public virtual KhoPhuongTien? IdPtNavigation { get; set; }
    }
}
