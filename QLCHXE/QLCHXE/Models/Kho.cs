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
        public string? MaNcc { get; set; }
        public DateTime? CreateAt { get; set; }
        public DateTime? UpdateAt { get; set; }
        public string? NhanVienThem { get; set; }

        public virtual NhaCungCap? MaNccNavigation { get; set; }
        public virtual ICollection<KhoPhuongTien> KhoPhuongTiens { get; set; }
    }
}
