using System;
using System.Collections.Generic;
using System.IO;
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
using System.Windows.Shapes;
using QLCHXE.Shared;

namespace QLCHXE.NhanVien
{
    /// <summary>
    /// Interaction logic for TrangChuNV.xaml
    /// </summary>
    public partial class TrangChuNV : Window
    {
        public string idUser { get; set; }

        public TrangChuNV()
        {
            InitializeComponent();
            
        }

        private void close_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }

        private void hidden_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void Btn_QuanLyXeMay_Click(object sender, RoutedEventArgs e)
        {
            mainFrame.Navigate(new QLXeMay());

        }

        private void Btn_QuanLyOto_Click(object sender, RoutedEventArgs e)
        {
            mainFrame.Navigate(new QLOTO());

        }

        private void Btn_QuanLyXetai_Click(object sender, RoutedEventArgs e)
        {
            mainFrame.Navigate(new QLXeTai());

        }

        private void Btn_NhapXeMay_Click(object sender, RoutedEventArgs e)
        {
            mainFrame.Navigate(new KhoXeMay { idUSer = this.idUser });

        }

        private void Btn_NhapOto_Click(object sender, RoutedEventArgs e)
        {
            mainFrame.Navigate(new NhapKhoOTO { idUSer = this.idUser });
        }

        private void Btn_NhapXeTai_Click(object sender, RoutedEventArgs e)
        {

            mainFrame.Navigate(new NhapKhoXeTai { idUSer = this.idUser });
        }

        private void Btn_ThongKeDT_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Btn_QuanLyBanXe_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Btn_DoiMK_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Btn_DangXuat_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            mainWindow.Show();

            this.Close();
        }
    }
}
