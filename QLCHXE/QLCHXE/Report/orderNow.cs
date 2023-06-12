using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLCHXE.Report
{
    public class orderNow
    {
        public string TenXe { get; set; }
        public double Gia { get; set; }
        public int SoLuong { get; set; }

        public double ThanhTien { get; set; }

        public orderNow(string tx, double gia, int sl, double tt)
        {
            TenXe = tx;
            Gia = gia;
            SoLuong = sl;
            ThanhTien = tt;
        }

    }
}
