using System;
using System.Collections.Generic;

namespace QLCHXE.Models
{
    public partial class OrderDetail
    {
        public string? TenXeMua { get; set; }
        public double? Gia { get; set; }
        public int? SoLuongMua { get; set; }
        public string Id { get; set; } = null!;
        public string? IdKh { get; set; }
        public string? IdOrder { get; set; }

        public virtual ThongTinKhachHang? IdKhNavigation { get; set; }
        public virtual Order? IdOrderNavigation { get; set; }
    }
}
