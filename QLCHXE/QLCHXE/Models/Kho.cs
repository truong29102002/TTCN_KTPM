using System;
using System.Collections.Generic;

namespace QLCHXE.Models
{
    public partial class Kho
    {
        public Kho()
        {
            KhoPhuongTiens = new HashSet<KhoPhuongTien>();
        }

        public string Id { get; set; } = null!;
        public string? NhanVienThem { get; set; }
        public string? TenKho { get; set; }
        public string? DiaChiKho { get; set; }
        public double? DienTich { get; set; }

        public virtual ICollection<KhoPhuongTien> KhoPhuongTiens { get; set; }
    }
}
