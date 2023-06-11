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
            var query = from i in db.NhaCungCaps
                        select new
                        {
                            MaNTT = i.MaNcc,
                            TenNhaTaiTro = i.TenNcc,
                            sdt = i.SoDtncc,
                            diachi = i.DiaChiNcc
                        };
            dtgNTT.ItemsSource = query.ToList();
        }

        public NhaTaiTro()
        {
            InitializeComponent();
            db = new QLCHXeContext();
            LoadDataGrid();
        }

        private void btnThem_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                NhaCungCap nhaCungCap = new NhaCungCap();
                nhaCungCap.MaNcc = "TT" + (db.NhaCungCaps.Count() + RandomNumberGenerator.GetInt32(1000, 9999)).ToString();
                nhaCungCap.TenNcc = txtTen.Text;
                nhaCungCap.DiaChiNcc = txtDiachi.Text;
                nhaCungCap.SoDtncc = txtSDT.Text;
                db.Add(nhaCungCap);
                db.SaveChanges();
                LoadDataGrid();
                MessageBox.Show("Thêm thành công nhà tài trợ mới","Noti");
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void btnSua_Click(object sender, RoutedEventArgs e)
        {
            if (dtgNTT.SelectedItem != null)
            {
                try
                {
                    Type type = dtgNTT.SelectedItem.GetType();
                    PropertyInfo[] propertyInfos = type.GetProperties();
                    string idNTT = propertyInfos[0].GetValue(dtgNTT.SelectedValue).ToString();

                    var query = db.NhaCungCaps.SingleOrDefault(x => x.MaNcc == idNTT);
                    query.TenNcc = txtTen.Text;
                    query.SoDtncc = txtSDT.Text;
                    query.DiaChiNcc = txtDiachi.Text;
                    db.SaveChanges();
                    LoadDataGrid();
                    MessageBox.Show("Cập nhật thành công nhà tài trợ có mã: " + idNTT, "Thong Bao");

                }
                catch (Exception ex)
                {

                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }

        private void btnXoa_Click(object sender, RoutedEventArgs e)
        {
            if (dtgNTT.SelectedItem != null)
            {
                try
                {
                    Type type = dtgNTT.SelectedItem.GetType();
                    PropertyInfo[] propertyInfos = type.GetProperties();
                    string idNTT = propertyInfos[0].GetValue(dtgNTT.SelectedValue).ToString();

                    if (MessageBox.Show("Xác nhận xóa nhà tài trợ đã chọn!", "Thong bao", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                    {
                        var query = db.NhaCungCaps.SingleOrDefault(x => x.MaNcc == idNTT);
                        db.Remove(query);
                        db.SaveChanges();
                        LoadDataGrid();
                        MessageBox.Show("Xoa thành công nhà tài trợ có mã: " + idNTT, "Thong Bao");
                    }

                }
                catch (Exception ex)
                {

                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }

        private void dtgNTT_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            if (dtgNTT.SelectedItem != null)
            {
                try
                {
                    Type type = dtgNTT.SelectedItem.GetType();
                    PropertyInfo[] propertyInfos = type.GetProperties();
                    txtTen.Text = propertyInfos[1].GetValue(dtgNTT.SelectedValue).ToString();
                    txtDiachi.Text = propertyInfos[3].GetValue(dtgNTT.SelectedValue).ToString();
                    txtSDT.Text = propertyInfos[2].GetValue(dtgNTT.SelectedValue).ToString();
                }
                catch (Exception ex)
                {

                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }
    }
}
