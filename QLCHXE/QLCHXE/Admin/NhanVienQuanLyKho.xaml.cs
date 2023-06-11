using QLCHXE.Models;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace QLCHXE.Admin
{
    /// <summary>
    /// Interaction logic for NhanVienQuanLyKho.xaml
    /// </summary>
    public partial class NhanVienQuanLyKho : Page
    {
        private readonly QLCHXeContext db;

        void LoadDataGrid()
        {
            var qery = from i in db.Khos
                       select new
                       {
                           idKho = i.Id,
                           TenKho = i.TenKho,
                           NV = i.NhanVienThem,
                           DiaChi = i.DiaChiKho,
                           DienTich = i.DienTich
                       };
            
            dtgNVKHo.ItemsSource = qery.ToList();
        }

        public NhanVienQuanLyKho()
        {
            InitializeComponent();
            db = new QLCHXeContext();
            LoadDataGrid();
            HorizontalAlignment = HorizontalAlignment.Stretch;
            VerticalAlignment = VerticalAlignment.Stretch;
        }

        private void btnThem_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var query = db.NhanViens.SingleOrDefault(x => x.MaNv == txtMANV.Text);
                if (query == null)
                {
                    MessageBox.Show("Nhân viên không tồn tại", "Thông báo");
                }
                else
                {
                    try
                    {
                        var qr = db.Khos.FirstOrDefault(x=>x.TenKho.Equals(txtTenKHo.Text) && x.NhanVienThem == txtMANV.Text);
                        if (qr != null)
                        {
                            MessageBox.Show("Nhân viên được thêm đã quản lý kho này rồi","Thông báo");
                        }
                        else
                        {
                            Kho kho = new Kho();
                            kho.NhanVienThem = query.MaNv;
                            kho.DiaChiKho = txtDiaChi.Text;
                            kho.TenKho = txtTenKHo.Text;
                            kho.DienTich = float.Parse(txtDienTich.Text);
                            kho.Id = "KH" + (RandomNumberGenerator.GetInt32(1000, 9999)).ToString("X");
                            db.Add(kho);
                            db.SaveChanges();
                            LoadDataGrid();
                            MessageBox.Show("Đã thêm một nhân viên quản lý kho", "Thông báo");
                        }
                        
                    }
                    catch (Exception ex)
                    {

                        MessageBox.Show("Error: " + ex.Message);
                    }
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error: " + ex.Message);

            }
        }

        private void btnSua_Click(object sender, RoutedEventArgs e)
        {
            if (dtgNVKHo.SelectedItem != null)
            {
                try
                {
                    Type type = dtgNVKHo.SelectedItem.GetType();
                    PropertyInfo[] propertyInfos = type.GetProperties();


                    string idKho = propertyInfos[0].GetValue(dtgNVKHo.SelectedValue).ToString();


                    try
                    {
                        var query = db.NhanViens.SingleOrDefault(x => x.MaNv == txtMANV.Text);
                        if (query == null)
                        {
                            MessageBox.Show("Nhân viên không tồn tại", "Thông báo");
                        }
                        else
                        {
                            try
                            {
                                var qr = db.Khos.FirstOrDefault(x => x.TenKho.Equals(txtTenKHo.Text) && x.NhanVienThem == txtMANV.Text);
                                if (qr != null)
                                {
                                    MessageBox.Show("Nhân viên đã quản lý kho này rồi", "Thông báo");
                                }
                                else
                                {

                                    var qer = db.Khos.SingleOrDefault(x => x.Id == idKho);
                                    if (MessageBox.Show("Xác nhận thay đổi !", "Thong Bao", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                                    {
                                        qer.NhanVienThem = txtMANV.Text;
                                        qer.DiaChiKho = txtDiaChi.Text;
                                        qer.TenKho = txtTenKHo.Text;
                                        qer.DienTich = float.Parse(txtDienTich.Text);
                                        db.SaveChanges();
                                        MessageBox.Show("Cập nhật thành công");
                                        LoadDataGrid();
                                    }
                                }

                            }
                            catch (Exception ex)
                            {

                                MessageBox.Show("Error: " + ex.Message);
                            }
                        }
                    }
                    catch (Exception ex)
                    {

                        MessageBox.Show("Error: " + ex.Message);

                    }

                    


                }
                catch (Exception ex)
                {

                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }

        private void btnXoa_Click(object sender, RoutedEventArgs e)
        {
            if (dtgNVKHo.SelectedItem != null)
            {
                try
                {
                    Type type = dtgNVKHo.SelectedItem.GetType();
                    PropertyInfo[] propertyInfos = type.GetProperties();


                    string idKho = propertyInfos[0].GetValue(dtgNVKHo.SelectedValue).ToString();

                    var qr = db.Khos.SingleOrDefault(x=>x.Id == idKho);
                    if (MessageBox.Show("Xac nhan xoa ","Thong Bao", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                    {
                        db.Remove(qr);
                        db.SaveChanges();
                        MessageBox.Show("Xóa thành công");
                        LoadDataGrid();
                    }


                }
                catch (Exception ex)
                {

                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }

        private void dtgNVKHo_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            if (dtgNVKHo.SelectedItem != null)
            {
                try
                {
                    Type type = dtgNVKHo.SelectedItem.GetType();
                    PropertyInfo[] propertyInfos = type.GetProperties();

                    
                    txtMANV.Text = propertyInfos[2].GetValue(dtgNVKHo.SelectedValue).ToString();
                    txtTenKHo.Text = propertyInfos[1].GetValue(dtgNVKHo.SelectedValue).ToString();
                    txtDienTich.Text = propertyInfos[4].GetValue(dtgNVKHo.SelectedValue).ToString();
                    txtDiaChi.Text = propertyInfos[3].GetValue(dtgNVKHo.SelectedValue).ToString();
                   


                }
                catch (Exception ex)
                {

                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }
    }
}
