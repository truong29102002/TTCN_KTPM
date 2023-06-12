using System;
using System.Collections.Generic;

namespace QLCHXE.Models
{
    public partial class Order
    {
        public Order()
        {
            OrderDetails = new HashSet<OrderDetail>();
        }

        public string Id { get; set; } = null!;
        public string? SoLuong { get; set; }
        public string? Thanhtien { get; set; }

        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
