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
using System.Windows.Shapes;

using TestPJ.Models;


namespace TestPJ.Shared
{
    /// <summary>
    /// Interaction logic for DOIMK.xaml
    /// </summary>

    public partial class DOIMK : Page
    {
        public string idUser { get; set; }
        private readonly QLCHXeContext db;
        public DOIMK()
        {
            InitializeComponent();
            db = new QLCHXeContext();
        }

        private void btn_xacNhan_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var query = db.Accounts.SingleOrDefault(x => x.TaiKhoan.Equals(idUser));
                string newPass = txtMKMOI.Password;
                string newPass_compare = txtMKMOI_compare.Password;
                if(txtMKCu.Text.Trim() != query.Matkhau)
                {
                    MessageBox.Show("Mat khau cu khong dung!", "Thong bao");
                }
                else
                {
                    if(newPass != newPass_compare)
                    {
                        MessageBox.Show("Mat khau moi khong khop nhau!", "Thong bao");
                    }
                    else
                    {
                        query.Matkhau = newPass_compare.Trim();
                        db.SaveChanges();
                        MessageBox.Show("Mat khau thay doi thanh cong!", "Thong bao");
                    }
                }
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
            

        }
    }
}
