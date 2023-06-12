using QLCHXE.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Printing;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Xps.Packaging;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;


namespace QLCHXE.Report
{
    /// <summary>
    /// Interaction logic for ReportReview.xaml
    /// </summary>
    public partial class ReportReview : Window
    {
        private readonly QLCHXeContext db;
        public ReportReview()
        {
            InitializeComponent();
            db = new QLCHXeContext();
            LoadCbxKho();

        }

        void LoadCbxKho()
        {
            var qr = db.Khos.Select(x => x);
            cbxKho.ItemsSource = qr.ToList();
            cbxKho.SelectedValuePath = "Id";
            cbxKho.DisplayMemberPath = "TenKho";
            cbxKho.SelectedIndex = 0;
        }



        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                this.IsEnabled = false;
                PrintDialog printDialog = new PrintDialog();
                if (printDialog.ShowDialog() == true)
                {

                    printDialog.PrintVisual(print, "ThongKe");
                }


            }
            finally
            {
                this.IsEnabled = true;
            }
        }

        public double TongTien { get; set; }
        public int SoLuongT { get; set; }

        private void btnReview_Click(object sender, RoutedEventArgs e)
        {
            TongTien = 0;
            SoLuongT = 0;
            string idKho = ((Kho)cbxKho.SelectedItem).Id;
            txtTenKho.Text = cbxKho.Text.ToString();
            txtHoTen.Text = txtHoTenNV.Text;
            txtSDT.Text = txtSDTNV.Text;
            if (DateTime.Parse(txtDateFrom.Text) <= DateTime.Parse(txtDateTo.Text))
            {
                txtNgayMua.Text = DateTime.Parse(txtDateFrom.Text).ToString("dd/MM/yy") + " to " + DateTime.Parse(txtDateTo.Text).ToString("dd/MM/yy");


                if (btnCheckOto.IsChecked == true)
                {
                    txtLoaiXe.Text = "Xe ô tô";

                    var qr = from i in db.KhoPhuongTiens
                             join j in db.OtoKhos on i.IdPt equals j.IdPtkho
                             join k in db.Khos on i.IdKho equals k.Id
                             where k.Id == idKho
                             where i.CreateAt <= DateTime.Parse(txtDateTo.Text)
                             where i.CreateAt >= DateTime.Parse(txtDateFrom.Text)

                             select new
                             {
                                 TenXe = i.TenXe,
                                 Gia = i.Gia,
                                 SoLuong = i.SoLuongNhap,
                                 ThanhTien = i.Gia * i.SoLuongNhap
                             };

                    listSell.ItemsSource = qr.ToList();
                    foreach (var item in qr)
                    {
                        TongTien += (double)item.ThanhTien;
                        SoLuongT += (int)item.SoLuong;
                    }
                    txtTongTien.Text = TongTien.ToString();
                    txtSoLuong.Text = SoLuongT.ToString();

                    if (txtTongTien.Text == "0")
                    {
                        MessageBox.Show("Không có đơn hàng nào được nhập trong khoảng thời gian hoặc kho đã chọn!", "Thông Báo", MessageBoxButton.OKCancel, MessageBoxImage.Error);

                    }

                }
                else if (btnCheckXeMay.IsChecked == true)
                {
                    txtLoaiXe.Text = "Xe máy";
                    var qr = from i in db.KhoPhuongTiens
                             join j in db.XeMayKhos on i.IdPt equals j.IdPt
                             join k in db.Khos on i.IdKho equals k.Id
                             where k.Id == idKho
                             where i.CreateAt <= DateTime.Parse(txtDateTo.Text)
                             where i.CreateAt >= DateTime.Parse(txtDateFrom.Text)

                             select new
                             {
                                 TenXe = i.TenXe,
                                 Gia = i.Gia,
                                 SoLuong = i.SoLuongNhap,
                                 ThanhTien = i.Gia * i.SoLuongNhap
                             };

                    listSell.ItemsSource = qr.ToList();
                    foreach (var item in qr)
                    {
                        TongTien += (double)item.ThanhTien;
                        SoLuongT += (int)item.SoLuong;
                    }
                    txtTongTien.Text = TongTien.ToString();
                    txtSoLuong.Text = SoLuongT.ToString();
                    if (txtTongTien.Text == "0")
                    {
                        MessageBox.Show("Không có đơn hàng nào được nhập trong khoảng thời gian hoặc kho đã chọn!", "Thông Báo", MessageBoxButton.OKCancel, MessageBoxImage.Error);

                    }
                }
                else if (btncheckXeTai.IsChecked == true)
                {
                    txtLoaiXe.Text = "Xe tải";
                    var qr = from i in db.KhoPhuongTiens
                             join j in db.XeTaiKhos on i.IdPt equals j.IdPt
                             join k in db.Khos on i.IdKho equals k.Id
                             where k.Id == idKho
                             where i.CreateAt <= DateTime.Parse(txtDateTo.Text)
                             where i.CreateAt >= DateTime.Parse(txtDateFrom.Text)

                             select new
                             {
                                 TenXe = i.TenXe,
                                 Gia = i.Gia,
                                 SoLuong = i.SoLuongNhap,
                                 ThanhTien = i.Gia * i.SoLuongNhap
                             };

                    listSell.ItemsSource = qr.ToList();
                    foreach (var item in qr)
                    {
                        TongTien += (double)item.ThanhTien;
                        SoLuongT += (int)item.SoLuong;
                    }
                    txtTongTien.Text = TongTien.ToString();
                    txtSoLuong.Text = SoLuongT.ToString();
                    if (txtTongTien.Text == "0")
                    {
                        MessageBox.Show("Không có đơn hàng nào được nhập trong khoảng thời gian hoặc kho đã chọn!", "Thông Báo", MessageBoxButton.OKCancel, MessageBoxImage.Error);

                    }

                }
                else
                {
                    MessageBox.Show("Vui lòng chọn loại xe cần thiết!", "Thông Báo", MessageBoxButton.OKCancel, MessageBoxImage.Error);

                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn đúng thông tin ngày tháng!", "Thông Báo", MessageBoxButton.OKCancel, MessageBoxImage.Error);
            }
        }

        private void btnCheckOto_Checked(object sender, RoutedEventArgs e)
        {
            if (btnCheckOto.IsChecked == true)
            {
                btnCheckXeMay.IsChecked = false;
                btncheckXeTai.IsChecked = false;
            }
        }

        private void btnCheckXeMay_Checked(object sender, RoutedEventArgs e)
        {
            if (btnCheckXeMay.IsChecked == true)
            {
                btnCheckOto.IsChecked = false;
                btncheckXeTai.IsChecked = false;
            }
        }

        private void btncheckXeTai_Checked(object sender, RoutedEventArgs e)
        {
            if (btncheckXeTai.IsChecked == true)
            {
                btnCheckXeMay.IsChecked = false;
                btnCheckOto.IsChecked = false;
            }
        }
    }
}
