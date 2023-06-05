using System;
using System.Collections.Generic;

namespace QLCHXE.Models
{
    public partial class NhaCungCap
    {
        public NhaCungCap()
        {
            Khos = new HashSet<Kho>();
        }

        public string MaNcc { get; set; } = null!;
        public string? TenNcc { get; set; }
        public string? DiaChiNcc { get; set; }
        public string? SoDtncc { get; set; }

        public virtual ICollection<Kho> Khos { get; set; }
    }
}
