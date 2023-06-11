using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
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
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using QLCHXE.Models;
using BCrypt.Net;
using static System.Net.Mime.MediaTypeNames;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace QLCHXE.Admin
{
    /// <summary>
    /// Interaction logic for QLNV.xaml
    /// </summary>
    public partial class QLNV : Page
    {
        private readonly QLCHXeContext db;
        public void LoadDataGrid()
        {
            var nv = from i in db.NhanViens
                     join j in db.Accounts on i.Taikhoan equals j.TaiKhoan
                     select new
                     {
                         MaNv = i.MaNv,
                         TenNv = i.TenNv,

                         Gender = i.Gender,
                         NgaySinh = i.NgaySinh.ToString(),
                         DiaChiNv = i.DiaChiNv,
                         Sodienthoai = i.Sodienthoai,
                         Taikhoan = i.Taikhoan,
                         Matkhau = j.Matkhau,
                         Quyen = (j.Quyen == 0) ? "Nhân viên" : "Quản lý"
                     };
            var data = nv.ToList();


            dtgNV.ItemsSource = nv.ToList();

        }
        public QLNV()
        {

            InitializeComponent();
            db = new QLCHXeContext();
            LoadDataGrid();
        }


        private void btnThem_Click(object sender, RoutedEventArgs e)
        {

            string salt = BCrypt.Net.BCrypt.GenerateSalt();
            string idnv = "NV" + (db.NhanViens.Count() + RandomNumberGenerator.GetInt32(1000, 9999)).ToString();
            if (String.IsNullOrEmpty(txtDiaChi.Text) || String.IsNullOrEmpty(txtNgaySinh.Text) || String.IsNullOrEmpty(txtTenTk.Text) || String.IsNullOrEmpty(txtSodt.Text) || String.IsNullOrEmpty(txtMatKhau.Text))
            {
                MessageBox.Show("Co truong chua nhap dua lieu", "Thong bao", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                try
                {

                    var tksql = db.Accounts.SingleOrDefault(y => y.TaiKhoan == txtTenTk.Text);
                    if (tksql != null)
                    {
                        MessageBox.Show("Tai khoan nhan vien muon them bi trung", "thong bao", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                    else
                    {
                        string hashedPassword = BCrypt.Net.BCrypt.HashPassword(txtMatKhau.Text, salt);


                        DateTime ngaySinh = DateTime.Parse(txtNgaySinh.SelectedDate.Value.ToString("yyyy-MM-dd"));
                        int quyen = (cbxQuyen.Text == "Nhân viên") ? 0 : 1;


                        var result = db.Database.ExecuteSqlRaw("Exec pr_insertNVAC @manv,@hoTen, @ngaysinh, @diachi, @sodt, @taiKhoan, @gender, @matkhau, @quyen", new SqlParameter("@manv", idnv),
                            new SqlParameter("@hoTen", txtTenNV.Text),

                            new SqlParameter("@ngaysinh", ngaySinh),
                            new SqlParameter("@diachi", txtDiaChi.Text),
                            new SqlParameter("@sodt", txtSodt.Text),
                            new SqlParameter("@taiKhoan", txtTenTk.Text),
                            new SqlParameter("@gender", cbxGender.Text),
                            new SqlParameter("@matkhau", hashedPassword),
                            new SqlParameter("@quyen", quyen)
                            );

                        LoadDataGrid();
                        MessageBox.Show("Them thanh cong nhan vien moi", "thong bao", MessageBoxButton.OK);

                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.ToString());
                }

            }
        }


        private void btnSua_Click(object sender, RoutedEventArgs e)
        {

            if (String.IsNullOrEmpty(txtTenNV.Text) || String.IsNullOrEmpty(txtDiaChi.Text) || String.IsNullOrEmpty(txtNgaySinh.Text) || String.IsNullOrEmpty(txtTenTk.Text) || String.IsNullOrEmpty(txtSodt.Text) || String.IsNullOrEmpty(txtMatKhau.Text))
            {
                MessageBox.Show("Co truong chua nhap dua lieu");
            }
            else
            {
                try
                {
                    var manvsql = db.NhanViens.SingleOrDefault(y => y.Taikhoan == txtTenTk.Text);
                    var tksql = db.Accounts.SingleOrDefault(y => y.TaiKhoan == txtTenTk.Text);
                    if (manvsql != null && tksql != null && manvsql.Taikhoan == tksql.TaiKhoan)
                    {
                        string salt = tksql.Matkhau.Substring(0, 29);
                        string hashedPassword = BCrypt.Net.BCrypt.HashPassword(txtMatKhau.Text, salt);
                        DateTime ngaySinh = DateTime.Parse(txtNgaySinh.SelectedDate.Value.ToString("yyyy-MM-dd"));

                        int quyen = (cbxQuyen.Text == "Nhân viên") ? 0 : 1;

                        var result = db.Database.ExecuteSqlRaw("Exec pr_UpdatetNVAC @manv,@hoTen, @ngaysinh, @diachi, @sodt, @taiKhoan, @gender, @matkhau, @quyen", new SqlParameter("@manv", manvsql.MaNv),
                            new SqlParameter("@hoTen", txtTenNV.Text),
                            new SqlParameter("@ngaysinh", ngaySinh),

                            new SqlParameter("@diachi", txtDiaChi.Text),
                            new SqlParameter("@sodt", txtSodt.Text),
                            new SqlParameter("@taiKhoan", txtTenTk.Text),
                            new SqlParameter("@gender", cbxGender.Text),
                            new SqlParameter("@matkhau", hashedPassword),
                            new SqlParameter("@quyen", quyen)
                            );
                        LoadDataGrid();
                        MessageBox.Show("Cap nhat thanh cong thong tin nhan vien co ma: " + manvsql.MaNv, "thong bao", MessageBoxButton.OK);

                    }
                    else
                    {
                        MessageBox.Show("Ma nhan vien or Tai Khoan khong khop trong csdl", "thong bao", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.ToString());
                }

            }
        }

        private void btnXoa_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtTenTk.Text))
            {
                MessageBox.Show("Chon nhan vien can xoa", "Thong bao", MessageBoxButton.OK);
            }
            else
            {
                try
                {
                    var manvsql = db.NhanViens.SingleOrDefault(y => y.Taikhoan == txtTenTk.Text);
                    var tksql = db.Accounts.SingleOrDefault(y => y.TaiKhoan == txtTenTk.Text);
                    if (manvsql != null && tksql != null && manvsql.Taikhoan == tksql.TaiKhoan)
                    {
                        if (MessageBox.Show("Ban co chac muon xoa nv: " + manvsql.MaNv, "Thong bao", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                        {
                            var result = db.Database.ExecuteSqlRaw("Exec pr_DeleteNVAC @manv",
                           new SqlParameter("@manv", manvsql.MaNv)
                           );
                            LoadDataGrid();
                            MessageBox.Show("Da xoa thong tin nhan vien co ma: " + manvsql.MaNv, "thong bao", MessageBoxButton.OK);

                        }

                    }
                    else
                    {
                        MessageBox.Show("Ma nhan vien or Tai Khoan khong khop trong csdl", "thong bao", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.ToString());
                }
            }

        }

        private void txtSodt_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(txtSodt.Text, "[^0-9]"))
            {
                MessageBox.Show("Please enter only phone numbers .");

            }
            if (txtSodt.Text.Length > 10)
            {
                MessageBox.Show("Please enter only 10 digits.");

            }
        }

        private void dtgNV_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            if (dtgNV.SelectedItem != null)
            {
                try
                {
                    Type type = dtgNV.SelectedItem.GetType();
                    PropertyInfo[] propertyInfos = type.GetProperties();

                    txtTenNV.Text = propertyInfos[1].GetValue(dtgNV.SelectedValue).ToString();
                    cbxGender.Text = (string)propertyInfos[2].GetValue(dtgNV.SelectedValue);
                    txtNgaySinh.Text = propertyInfos[3].GetValue(dtgNV.SelectedValue).ToString();
                    txtDiaChi.Text = propertyInfos[4].GetValue(dtgNV.SelectedValue).ToString();
                    txtTenTk.Text = propertyInfos[6].GetValue(dtgNV.SelectedValue).ToString();
                    txtSodt.Text = propertyInfos[5].GetValue(dtgNV.SelectedValue).ToString();
                    txtMatKhau.Text = propertyInfos[7].GetValue(dtgNV.SelectedValue).ToString();
                    cbxQuyen.Text = propertyInfos[8].GetValue(dtgNV.SelectedValue).ToString();

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.ToString());

                }
            }
        }

        private void BtnClear_Click(object sender, RoutedEventArgs e)
        {

            txtDiaChi.Text = "";
            txtNgaySinh.Text = "";
            txtMatKhau.Text = "";
            txtSodt.Text = "";
            txtTenNV.Text = "";
            txtTenTk.Text = "";
            cbxGender.SelectedIndex = 0;
            LoadDataGrid();
        }

        private void btnTim_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtTim.Text))
            {
                MessageBox.Show("Nhập tên nhân viên or mã nhân viên cần tìm", "Thong bao", MessageBoxButton.OK);
            }
            else
            {
                try
                {
                    if (txtTim.Text.Contains("NV"))
                    {
                        var qe = db.NhanViens.SingleOrDefault(x => x.MaNv == txtTim.Text);
                        if (qe == null)
                        {
                            MessageBox.Show("Không tìm thấy mã nhân viên này", "Thong bao", MessageBoxButton.OK);
                            LoadDataGrid();
                        }
                        else
                        {
                            var sql = from i in db.NhanViens
                                      join j in db.Accounts on i.Taikhoan equals j.TaiKhoan
                                      where i.MaNv == txtTim.Text
                                      select new
                                      {
                                          MaNv = i.MaNv,
                                          TenNv = i.TenNv,
                                          Gender = i.Gender,
                                          NgaySinh = i.NgaySinh.ToString(),
                                          DiaChiNv = i.DiaChiNv,
                                          Sodienthoai = i.Sodienthoai,
                                          Taikhoan = i.Taikhoan,
                                          Matkhau = j.Matkhau,
                                          Quyen = (j.Quyen == 0) ? "Nhan Vien" : "Quan ly"
                                      };
                            dtgNV.ItemsSource = sql.ToList();
                        }
                    }
                    else
                    {
                        #region Tim ten
                        int d = 0;
                        var query = from i in db.NhanViens
                                    join j in db.Accounts on i.Taikhoan equals j.TaiKhoan
                                    where i.TenNv.Contains(txtTim.Text)
                                    select new
                                    {
                                        MaNv = i.MaNv,
                                        TenNv = i.TenNv,
                                        Gender = i.Gender,
                                        NgaySinh = i.NgaySinh.ToString(),
                                        DiaChiNv = i.DiaChiNv,
                                        Sodienthoai = i.Sodienthoai,
                                        Taikhoan = i.Taikhoan,
                                        Matkhau = j.Matkhau,
                                        Quyen = (j.Quyen == 0) ? "Nhan Vien" : "Quan ly"
                                    };
                        foreach (var item in query)
                        {
                            if (item.TenNv.Contains(txtTim.Text))
                            {
                                d++;
                            }
                        }
                        if (d == 0)
                        {
                            MessageBox.Show("Không tìm thấy tên nhân viên này", "Thong bao", MessageBoxButton.OK);
                            LoadDataGrid();
                        }
                        else
                        {
                            dtgNV.ItemsSource = query.ToList();
                        }
                        #endregion
                    }


                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.ToString());
                }

            }
        }
    }
}
