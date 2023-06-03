using System;
using System.Collections.Generic;

namespace TestPJ.Models
{
    public partial class HangXe
    {
        public HangXe()
        {
            PhuongTiens = new HashSet<PhuongTien>();
        }

        public string IdHangXe { get; set; } = null!;
        public string? Tenhanngxe { get; set; }
        public string? Loaixe { get; set; }

        public virtual ICollection<PhuongTien> PhuongTiens { get; set; }
    }
}
