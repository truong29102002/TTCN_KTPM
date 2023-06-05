using System;
using System.Collections.Generic;

namespace QLCHXE.Models
{
    public partial class HangXe
    {
        public HangXe()
        {
            KhoPhuongTiens = new HashSet<KhoPhuongTien>();
            PhuongTiens = new HashSet<PhuongTien>();
        }

        public string IdHangXe { get; set; } = null!;
        public string? Tenhanngxe { get; set; }
        public string? Loaixe { get; set; }

        public virtual ICollection<KhoPhuongTien> KhoPhuongTiens { get; set; }
        public virtual ICollection<PhuongTien> PhuongTiens { get; set; }
    }
}
