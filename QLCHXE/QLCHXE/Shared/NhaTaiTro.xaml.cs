using QLCHXE.Models;
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
    /// Interaction logic for NhaTaiTro.xaml
    /// </summary>
    public partial class NhaTaiTro : Page
    {
        private readonly QLCHXeContext db;

        void LoadDataGrid()
        {

        }

        public NhaTaiTro()
        {
            InitializeComponent();
            db = new QLCHXeContext();
        }

        private void btnThem_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnSua_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnXoa_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
