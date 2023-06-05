using System;
using System.Collections.Generic;

namespace QLCHXE.Models
{
    public partial class Mau
    {
        public Mau()
        {
            KhoPhuongTiens = new HashSet<KhoPhuongTien>();
            PhuongTiens = new HashSet<PhuongTien>();
        }

        public string Mamau { get; set; } = null!;
        public string? Tenmau { get; set; }

        public virtual ICollection<KhoPhuongTien> KhoPhuongTiens { get; set; }
        public virtual ICollection<PhuongTien> PhuongTiens { get; set; }
    }
}
