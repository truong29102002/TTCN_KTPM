using System;
using System.Collections.Generic;

namespace QLCHXE.Models
{
    public partial class KhoPhuongTien
    {
        public KhoPhuongTien()
        {
            OtoKhos = new HashSet<OtoKho>();
            XeMayKhos = new HashSet<XeMayKho>();
            XeTaiKhos = new HashSet<XeTaiKho>();
        }

        public string IdPt { get; set; } = null!;
        public string? IdKho { get; set; }
        public string? IdMau { get; set; }
        public int? NamSx { get; set; }
        public double? Gia { get; set; }
        public string? TenXe { get; set; }
        public int? SoLuongKho { get; set; }
        public string? DonVi { get; set; }
        public string? Description { get; set; }
        public string? IdHangXe { get; set; }

        public virtual HangXe? IdHangXeNavigation { get; set; }
        public virtual Kho? IdKhoNavigation { get; set; }
        public virtual Mau? IdMauNavigation { get; set; }
        public virtual ICollection<OtoKho> OtoKhos { get; set; }
        public virtual ICollection<XeMayKho> XeMayKhos { get; set; }
        public virtual ICollection<XeTaiKho> XeTaiKhos { get; set; }
    }
}
