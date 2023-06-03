using System;
using System.Collections.Generic;

namespace QLCHXE.Models
{
    public partial class NhaCungCap
    {
        public NhaCungCap()
        {
            Phieunhaps = new HashSet<Phieunhap>();
        }

        public string MaNcc { get; set; } = null!;
        public string? TenNcc { get; set; }
        public string? DiaChiNcc { get; set; }
        public string? SoDtncc { get; set; }
        public string? MaStncc { get; set; }

        public virtual ICollection<Phieunhap> Phieunhaps { get; set; }
    }
}
