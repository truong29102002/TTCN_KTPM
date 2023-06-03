using System;
using System.Collections.Generic;

namespace TestPJ.Models
{
    public partial class TrangThaiThue
    {
        public TrangThaiThue()
        {
            HopDongThues = new HashSet<HopDongThue>();
        }

        public string Mathue { get; set; } = null!;
        public DateTime? Ngaybatdau { get; set; }
        public DateTime? Ngayketthuc { get; set; }
        public int? Trangthai { get; set; }

        public virtual ICollection<HopDongThue> HopDongThues { get; set; }
    }
}
