using QLCHXE.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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

namespace QLCHXE.Report
{
    /// <summary>
    /// Interaction logic for ThongKe.xaml
    /// </summary>
    public partial class ThongKe : Window
    {
        private readonly QLCHXeContext db;


        public List<orderNow> orderNows = new List<orderNow>();
        public double tongtien { get; set; }
        public double soluong { get; set; }
        public ThongKe()
        {
            InitializeComponent();
            db = new QLCHXeContext();
            txtNgayMua.Text = DateTime.Now.ToString("dd MMM yy");

        }


        void LoadList()
        {
            listSell.ItemsSource = orderNows;
            foreach (var orderNow in orderNows)
            {
                tongtien += orderNow.ThanhTien;
                soluong += orderNow.SoLuong;
            }
            txtTongTien.Text = tongtien.ToString();
            txtSoLuong.Text = soluong.ToString();
        }

        //public ObservableCollection<RowData> Rows { get; set; }

        //public TableData()
        //{
        //    Rows = new ObservableCollection<RowData>();
        //}
        private void Button_Click(object sender, RoutedEventArgs e)
        {

            if (MessageBox.Show("Lưu thông tin khách hàng và tạo hóa đơn!", "Thong bao", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                try
                {

                    this.IsEnabled = false;
                    PrintDialog printDialog = new PrintDialog();
                    if (printDialog.ShowDialog() == true)
                    {

                        printDialog.PrintVisual(print, "ThongKe");
                    }


                    ThongTinKhachHang kh = new ThongTinKhachHang();
                    kh.MaKh = "KH" + (db.ThongTinKhachHangs.Count() + RandomNumberGenerator.GetInt32(1000, 9999)).ToString("X");

                    kh.TenKh = txtHoTen.Text;
                    kh.DiaChi = txtDiaChi.Text;
                    kh.Sodienthoai = txtSDT.Text;

                    db.Add(kh);

                    Order order = new Order();
                    order.Id = "OR" + (db.Orders.Count() + RandomNumberGenerator.GetInt32(1000, 9999)).ToString("X");
                    order.SoLuong = soluong.ToString();
                    order.Thanhtien = tongtien.ToString();


                    db.Add(order);

                    foreach (var item in orderNows)
                    {
                        OrderDetail orderDetail = new OrderDetail();

                        orderDetail.Id = "OD" + (db.OrderDetails.Count() + RandomNumberGenerator.GetInt32(1000, 9999)).ToString("X");
                        orderDetail.TenXeMua = item.TenXe;
                        orderDetail.Gia = item.Gia;
                        orderDetail.SoLuongMua = item.SoLuong;
                        orderDetail.IdKh = kh.MaKh;
                        orderDetail.IdOrder = order.Id;
                        db.Add(orderDetail);
                    }


                    db.SaveChanges();


                }
                finally
                {
                    this.IsEnabled = true;
                }
            }
            



        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadList();
        }
    }
}
