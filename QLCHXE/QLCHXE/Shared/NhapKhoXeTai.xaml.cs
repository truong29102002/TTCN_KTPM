using QLCHXE.Models;
using QLCHXE.wwwroot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace QLCHXE.Shared
{
    /// <summary>
    /// Interaction logic for NhapKhoXeTai.xaml
    /// </summary>
    public partial class NhapKhoXeTai : Page
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
        public NhapKhoXeTai()
        {
            InitializeComponent();
            db = new QLCHXeContext();
            loaddtgview();
            //loadcbohsx();
            //cboNcc();
            //cboMau();

        }

        private void btnthem_Click(object sender, RoutedEventArgs e)
        {
            //if (string.IsNullOrEmpty(txtid.Text.Trim()) || string.IsNullOrEmpty(txtten.Text.Trim()) ||
            //    string.IsNullOrEmpty(txtnamsx.Text.Trim()) || string.IsNullOrEmpty(txtdc.Text.Trim()) ||
            //    string.IsNullOrEmpty(txtsdt.Text.Trim()) || string.IsNullOrEmpty(txtgia.Text.Trim()) || string.IsNullOrEmpty(txtsln.Text.Trim())
            //)
            //{
            //    MessageBox.Show("Có ô chưa nhập dữ liệu!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
            //}
            //else
            //{
            //    try
            //    {
            //        string idPt = "PT" + (db.PhuongTiens.Count() + RandomNumberGenerator.GetInt32(1000, 9999)).ToString();
            //        PhuongTien pt = new PhuongTien();
            //        pt.IdPt = idPt;
            //        pt.NamSx = int.Parse(txtnamsx.ToString());
            //        pt.Mamau = ((Mau)cbomau.SelectedItem).Mamau;
            //        pt.TenXe = txtten.Text;
            //        pt.IdHangXe = ((HangXe)cbohang.SelectedItem).IdHangXe;
            //        pt.Soluong = int.Parse(txtsln.ToString());
            //        pt.Gia = float.Parse(txtgia.ToString());
            //        db.Add(pt);
            //        db.SaveChanges();
            //        loaddtgview();
            //        MessageBox.Show("Them thanh cong", "Thong bao");
            //    }
            //    catch (Exception ex)
            //    {
            //        MessageBox.Show("error: " + ex.Message);
            //    }
            //}
        }





        private void btntim_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnsua_Click(object sender, RoutedEventArgs e)
        {
            //if (dtgkhoxemay.SelectedItem != null)
            //{
            //    if (string.IsNullOrEmpty(txtid.Text.Trim()) || string.IsNullOrEmpty(txtten.Text.Trim()) || string.IsNullOrEmpty(txtgia.Text.Trim()) ||
            //        string.IsNullOrEmpty(txtnamsx.Text.Trim()) || string.IsNullOrEmpty(txtsln.Text.Trim()))
            //    {
            //        MessageBox.Show("Có ô chưa nhập dữ liệu!", "Thông báo");
            //    }
            //    else
            //    {
            //        try
            //        {
            //            Type type = dtgkhoxemay.SelectedItem.GetType();
            //            PropertyInfo[] pr = type.GetProperties();
            //            string idpt = pr[1].GetValue(dtgkhoxemay.SelectedItem).ToString();
            //            var pt = db.PhuongTiens.SingleOrDefault(i => i.IdPt.Equals(idpt));
            //            var xm = db.XeMays.SingleOrDefault(i => i.IdPt.Equals(idpt));
            //            pt.NamSx = int.Parse(txtnamsx.Text);
            //            pt.Gia = float.Parse(txtgia.Text);
            //            pt.Soluong = int.Parse(txtsln.Text);
            //            pt.TenXe = txtten.Text;
            //            pt.IdHangXe = ((HangXe)cbohang.SelectedItem).IdHangXe;
            //            pt.Mamau = ((Mau)cbomau.SelectedItem).Mamau;
            //            if (MessageBox.Show("xac nhan sua phuong tien ma:" + idpt, "thong bao", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            //            {
            //                db.SaveChanges();
            //                MessageBox.Show("Da sua phuong tien co ma:" + idpt);
            //                loaddtgview();
            //            }

            //        }
            //        catch (Exception ex)
            //        {
            //            MessageBox.Show("error: " + ex.Message, "error");
            //        }
            //    }
            //}
        }

        private void btnxoa_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
