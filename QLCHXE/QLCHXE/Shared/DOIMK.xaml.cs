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

using QLCHXE.Models;


namespace QLCHXE.Shared
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
                bool checkHashedPassword = BCrypt.Net.BCrypt.Verify(txtMKCu.Text, query.Matkhau);
                if(!checkHashedPassword)
                {
                    MessageBox.Show("Mật khẩu cũ không đúng!", "Thông báo");
                }
                else
                {
                    if(newPass != newPass_compare)
                    {
                        MessageBox.Show("Mật khẩu không khớp nhau!", "Thong bao");
                    }
                    else
                    {
                        string salt = BCrypt.Net.BCrypt.GenerateSalt();
                        string hashPassword = BCrypt.Net.BCrypt.HashPassword(newPass, salt);
                        query.Matkhau = hashPassword;
                        db.SaveChanges();
                        MessageBox.Show("Mật khẩu thay đổi thành công!", "Thông báo");
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
