using System;
using System.Collections.Generic;

namespace QLCHXE.Models
{
    public partial class TrangThaiThue
    {
        public string Mathue { get; set; } = null!;
        public DateTime? Ngaybatdau { get; set; }
        public DateTime? Ngayketthuc { get; set; }
        public int? Trangthai { get; set; }
    }
}
