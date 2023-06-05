using System;
using System.Collections.Generic;

namespace QLCHXE.Models
{
    public partial class NhanVien
    {
        public string MaNv { get; set; } = null!;
        public string? TenNv { get; set; }
        public DateTime? NgaySinh { get; set; }
        public string? DiaChiNv { get; set; }
        public string? Sodienthoai { get; set; }
        public string? Taikhoan { get; set; }
        public string? Gender { get; set; }

        public virtual Account? TaikhoanNavigation { get; set; }
    }
}
