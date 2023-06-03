using System;
using System.Collections.Generic;

namespace QLCHXE.Models
{
    public partial class Oto
    {
        public string IdOto { get; set; } = null!;
        public int? Sochongoi { get; set; }
        public string? Kieudongco { get; set; }
        public string? IdPt { get; set; }

        public virtual PhuongTien? IdPtNavigation { get; set; }
    }
}
