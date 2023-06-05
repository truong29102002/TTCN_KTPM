using QLCHXE.Models;
using QLCHXE.wwwroot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace QLCHXE.Shared
{
    /// <summary>
    /// Interaction logic for PhieuNhapXeMay.xaml
    /// </summary>

    public partial class PhieuNhapXeMay : Page

    {
        

        private readonly QLCHXeContext db;
        private List<DataGridKho> listData;
        //public void loadcbohsx()
        //{
        //    var a = from i in db.HangXes orderby i.Tenhanngxe ascending select i;
        //    cbohang.ItemsSource = a.ToList();
        //    cbohang.SelectedValuePath = "IdHangXe";
        //    cbohang.DisplayMemberPath = "Tenhangxe";
        //    cbohang.SelectedIndex = 0;
        //}
        //public void cboMau()
        //{
        //    cbomau.ItemsSource = db.Maus.Select(i => i.Tenmau).ToList();
        //    cbomau.DisplayMemberPath = "Tenmau";
        //    cbomau.SelectedValuePath = "Mamau";
        //    cbomau.SelectedIndex = 0;
        //}
        //public void cboNcc()
        //{
        //    cboncc.ItemsSource = db.NhaCungCaps.Select(i => i.TenNcc).ToList();
        //    cboncc.DisplayMemberPath = "TenNcc";
        //    cboncc.SelectedValuePath = "MaNcc";
        //    cboncc.SelectedIndex = 0;

        //}
        public void loaddtgview()
        {
            //var sql = from i in db.PhuongTiens
            //          join j in db.PhieuNhapChiTiets
            //         on i.IdPt equals j.IdPt
            //          join k in db.HangXes
            //         on i.IdHangXe equals k.IdHangXe
            //          join h in db.Maus
            //         on i.Mamau equals h.Mamau
            //          select new
            //          {
            //              IdPt = i.IdPt,
            //              Tenxe = i.TenXe,
            //              Tenhangxe = k.Tenhanngxe,
            //              Mau = h.Tenmau,
            //              NamSx = i.NamSx,
            //              Soluongnhap = i.Soluong,
            //              Dongianhap = i.Gia,
            //              Thanhtien = i.Gia * i.Soluong,

            //          };
            //listData = new List<DataGridKho>();
            //listData.Clear();
            //foreach (var i in sql)
            //{
            //    listData.Add(new DataGridKho
            //    {
            //        IdPt = i.IdPt,
            //        TenXe = i.Tenxe,
            //        Tenhangxe = i.Tenhangxe,
            //        Mau = i.Mau,
            //        NamSx = (int)i.NamSx,
            //        Soluongnhap = (int)i.Soluongnhap,
            //        Dongianhap = (double)i.Dongianhap,
            //        Thanhtien = (double)i.Thanhtien,
            //    });

            //}
            //dtgkhoxemay.ItemsSource = listData;
        }
        public PhieuNhapXeMay()
        {
            InitializeComponent();
            db = new QLCHXeContext();
            loaddtgview();
            //loadcbohsx();
            //cboNcc();
            //cboMau();

        }




        
    }
}
