using QLCHXE.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace QLCHXE.Report
{
    /// <summary>
    /// Interaction logic for ThongKe.xaml
    /// </summary>
    public partial class ThongKe : Window
    {
        private readonly QLCHXeContext db;
        public ThongKe()
        {
            InitializeComponent();
            db = new QLCHXeContext();
            LoadList();
            txtNgayMua.Text = DateTime.Now.ToString("dd MMM yy");
        }


        void LoadList()
        {
            var Sell = from i in db.PhuongTiens
                     select new
                     {
                         TenXe = i.TenXe,
                         Gia = i.Gia,
                         SoLuong = i.Soluong,
                        ThanhTien = i.Gia * i.Soluong
                     };
            listSell.ItemsSource = Sell.ToList();
        }

        //public ObservableCollection<RowData> Rows { get; set; }

        //public TableData()
        //{
        //    Rows = new ObservableCollection<RowData>();
        //}
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
    }
}
