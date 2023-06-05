using System;
using System.Collections.Generic;

namespace QLCHXE.Models
{
    public partial class OtoKho
    {
        public string IdOtoKho { get; set; } = null!;
        public int? Sochongoi { get; set; }
        public string? KieuDongCo { get; set; }
        public string? IdPtkho { get; set; }

        public virtual KhoPhuongTien? IdPtkhoNavigation { get; set; }
    }
}
