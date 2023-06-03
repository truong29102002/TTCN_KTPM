using System;
using System.Collections.Generic;

namespace QLCHXE.Models
{
    public partial class Account
    {
        public Account()
        {
            NhanViens = new HashSet<NhanVien>();
        }

        public string TaiKhoan { get; set; } = null!;
        public string? Matkhau { get; set; }
        public int? Quyen { get; set; }

        public virtual ICollection<NhanVien> NhanViens { get; set; }
    }
}
