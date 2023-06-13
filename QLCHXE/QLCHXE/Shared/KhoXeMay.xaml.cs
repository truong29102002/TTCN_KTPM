
using QLCHXE.Models;
using QLCHXE.Report;
using System;
using System.Collections.Generic;
using System.IO;
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


using static System.Net.Mime.MediaTypeNames;

namespace QLCHXE.Shared
{
    /// <summary>
    /// Interaction logic for KhoXeMay.xaml
    /// </summary>
    public partial class KhoXeMay : Page
    {
        private readonly QLCHXeContext db;
        public string idUSer { get; set; }
        void LoadCBXBrand()
        {
            var query = from i in db.HangXes orderby i.Tenhanngxe ascending select i;
            cbxBrand.ItemsSource = query.ToList();
            cbxBrand.DisplayMemberPath = "Tenhanngxe";
            cbxBrand.SelectedValuePath = "IdHangXe";
            cbxBrand.SelectedIndex = 0;
        }
        void LoadCBXColor()
        {
            var query = from i in db.Maus orderby i.Tenmau ascending select i;
            cbxColor.ItemsSource = query.ToList();
            cbxColor.DisplayMemberPath = "Tenmau";
            cbxColor.SelectedValuePath = "Mamau";
            cbxColor.SelectedIndex = 0;
        }
        void LoadCbxNTT()
        {
            var query = from i in db.NhaCungCaps orderby i.TenNcc ascending select i;
            cbxNTT.ItemsSource = query.ToList();
            cbxNTT.DisplayMemberPath = "TenNcc";
            cbxNTT.SelectedValuePath = "MaNcc";
            cbxNTT.SelectedIndex = 0;

        }

        void LoadCbxYear()
        {
            var years = Enumerable.Range(1950, DateTime.UtcNow.Year - 1949).ToList();
            cbxNamSX.ItemsSource = years;
            cbxNamSX.SelectedItem = years[years.Count - 1];
        }
        void LoadKHO()
        {
            var tk = db.Accounts.SingleOrDefault(x => x.TaiKhoan == idUSer);
            if (tk?.Quyen == 0)
            {
                txtNhapKHo.Visibility = Visibility.Hidden;
                lbNhapKho.Visibility = Visibility.Hidden;
            }
            else
            {
                txtNhapKHo.Visibility = Visibility.Visible;
                lbNhapKho.Visibility = Visibility.Visible;
            }
        }

        void LoadDataGrid()
        {
            var dt = from i in db.KhoPhuongTiens
                     join j in db.Khos
                     on i.IdKho equals j.Id
                     join k in db.NhaCungCaps
                     on i.MaNcc equals k.MaNcc
                     join h in db.XeMayKhos
                     on i.IdPt equals h.IdPt
                     join l in db.HangXes
                     on i.IdHangXe equals l.IdHangXe
                     join m in db.Maus
                     on i.IdMau equals m.Mamau
                     select new
                     {
                         idPt = i.IdPt,
                         Tenxe = i.TenXe,
                         Tenhangxe = l.Tenhanngxe,
                         Tenmau = m.Tenmau,
                         NamSx = i.NamSx,
                         PhanKhoi = h.CongSuat,
                         NgayNhap = i.CreateAt.ToString(),
                         TenNTT = k.TenNcc,
                         NvThem = j.NhanVienThem,
                         Kho = j.TenKho,
                         Soluongnhap = i.SoLuongKho,
                         Dongianhap = i.Gia,
                         Thanhtien = i.SoLuongKho * i.Gia,
                         MoTa = i.Description,

                         SDTNCC = k.SoDtncc,
                         DiaChi = k.DiaChiNcc,
                         idKho = j.Id,
                         DonVi = i.DonVi,
                         NgayCapNhat = i.UpdateAt.ToString(),
                         MaXe = h.IdXmkho,
                         NvSua = i.NvUpdate

                     };
            dtgkhoxemay.ItemsSource = dt.ToList();
        }

        public KhoXeMay()
        {
            InitializeComponent();
            db = new QLCHXeContext();
            LoadCBXBrand();
            LoadCBXColor();
            LoadCbxNTT();
            LoadCbxYear();
            LoadDataGrid();
            Loaded += Page_Loaded;
        }



       
        private void btnthem_Click(object sender, RoutedEventArgs e)
        {
            #region them
            try
            {
                var tk = db.Accounts.SingleOrDefault(x => x.TaiKhoan == idUSer);

                if (tk?.Quyen == 0)
                {
                    var nv = db.NhanViens.SingleOrDefault(x => x.Taikhoan == tk.TaiKhoan);
                    if (nv != null)
                    {
                        var kh = db.Khos.SingleOrDefault(x => x.NhanVienThem == nv.MaNv);
                        if (kh != null)
                        {
                            try
                            {
                                #region Them Kho_PT admin
                                string idPT = "PT" + (db.KhoPhuongTiens.Count() + RandomNumberGenerator.GetInt32(1000, 9999)).ToString("X");
                                KhoPhuongTien pt = new KhoPhuongTien();

                                pt.IdPt = idPT;
                                pt.IdKho = kh.Id;
                                pt.NamSx = int.Parse(cbxNamSX.SelectedItem.ToString());
                                pt.Gia = float.Parse(txtgia.Text);
                                pt.IdMau = ((Mau)cbxColor.SelectedItem).Mamau;
                                pt.TenXe = txtten.Text;
                                pt.NvUpdate = nv.MaNv;
                                pt.SoLuongKho = int.Parse(txtsln.Text);
                                pt.SoLuongNhap = int.Parse(txtsln.Text);
                                pt.Description = txtMota.Text;
                                pt.DonVi = txtDonVi.Text;
                                pt.IdHangXe = ((HangXe)cbxBrand.SelectedItem).IdHangXe;
                                pt.MaNcc = ((NhaCungCap)cbxNTT.SelectedItem).MaNcc;
                                pt.CreateAt = DateTime.Parse(DateTime.UtcNow.ToLocalTime().ToString("yyyy-MM-dd"));

                                pt.UpdateAt = DateTime.Parse(DateTime.UtcNow.ToLocalTime().ToString("yyyy-MM-dd"));
                                db.Add(pt);
                                #endregion


                                XeMayKho xm = new XeMayKho();

                                xm.IdPt = idPT;
                                xm.CongSuat = int.Parse(txtPhanKhoi.Text);
                                xm.IdXmkho = "XM" + (db.XeMays.Count() + RandomNumberGenerator.GetInt32(1000, 9999)).ToString("X");

                                db.Add(xm);
                                db.SaveChanges();
                                MessageBox.Show("Đã nhập thêm xe mới vào kho", "Thong bao");
                                LoadDataGrid();
                            }
                            catch (Exception ex)
                            {

                                MessageBox.Show(ex.Message);
                            }

                        }
                        else
                        {
                            MessageBox.Show("Bạn không có đủ thẩm quyền để thêm sản phẩm vào kho", "Thong bao");
                        }

                    }


                }
                else
                {
                    var nv = db.NhanViens.SingleOrDefault(x => x.Taikhoan == tk.TaiKhoan);
                    if (nv != null)
                    {
                        var kh = db.Khos.SingleOrDefault(x => x.Id == txtNhapKHo.Text);
                        if (kh != null)
                        {
                            #region Them Kho_PT admin
                            string idPT = "PT" + (db.KhoPhuongTiens.Count() + RandomNumberGenerator.GetInt32(1000, 9999)).ToString("X");
                            KhoPhuongTien pt = new KhoPhuongTien();

                            pt.IdPt = idPT;
                            pt.IdKho = txtNhapKHo.Text;
                            pt.NamSx = int.Parse(cbxNamSX.SelectedItem.ToString());
                            pt.Gia = float.Parse(txtgia.Text);
                            pt.IdMau = ((Mau)cbxColor.SelectedItem).Mamau;
                            pt.TenXe = txtten.Text;
                            pt.NvUpdate = nv.MaNv;
                            pt.SoLuongKho = int.Parse(txtsln.Text);
                            pt.SoLuongNhap = int.Parse(txtsln.Text);
                            pt.Description = txtMota.Text;
                            pt.DonVi = txtDonVi.Text;
                            pt.IdHangXe = ((HangXe)cbxBrand.SelectedItem).IdHangXe;
                            pt.MaNcc = ((NhaCungCap)cbxNTT.SelectedItem).MaNcc;
                            pt.CreateAt = DateTime.Parse(DateTime.UtcNow.ToLocalTime().ToString("yyyy-MM-dd"));

                            pt.UpdateAt = DateTime.Parse(DateTime.UtcNow.ToLocalTime().ToString("yyyy-MM-dd"));
                            db.Add(pt);
                            #endregion


                            XeMayKho xm = new XeMayKho();

                            xm.IdPt = idPT;
                            xm.CongSuat = int.Parse(txtPhanKhoi.Text);
                            xm.IdXmkho = "XM" + (db.XeMays.Count() + RandomNumberGenerator.GetInt32(1000, 9999)).ToString("X");

                            db.Add(xm);
                            db.SaveChanges();
                            MessageBox.Show("Đã nhập thêm xe mới vào kho", "Thong bao");
                            LoadDataGrid();
                        }
                        else
                        {
                            MessageBox.Show("Mã kho không tồn tại!", "Thông báo");
                        }
                    }



                }


            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);

            }
            #endregion
        }

        private void btnsua_Click(object sender, RoutedEventArgs e)
        {
            #region Update
            if (dtgkhoxemay.SelectedItem != null)
            {
                try
                {
                    Type type = dtgkhoxemay.SelectedItem.GetType();
                    PropertyInfo[] propertyInfos = type.GetProperties();

                    string id = propertyInfos[0].GetValue(dtgkhoxemay.SelectedValue).ToString();
                    string idXe = propertyInfos[19].GetValue(dtgkhoxemay.SelectedValue).ToString();
                    string idNv = propertyInfos[8].GetValue(dtgkhoxemay.SelectedValue).ToString();

                    #region Update
                    try
                    {
                        var tk = db.Accounts.SingleOrDefault(x => x.TaiKhoan == idUSer);

                        if (tk?.Quyen == 0)
                        {
                            var nv = db.NhanViens.SingleOrDefault(x => x.Taikhoan == tk.TaiKhoan && x.MaNv == idNv);
                            if (nv != null)
                            {

                                try
                                {
                                    #region Sua Kho_PT 
                                    var pt = db.KhoPhuongTiens.SingleOrDefault(x => x.IdPt == id);
                                    if (pt != null)
                                    {
                                        pt.NamSx = int.Parse(cbxNamSX.SelectedItem.ToString());
                                        pt.Gia = float.Parse(txtgia.Text);
                                        pt.IdMau = ((Mau)cbxColor.SelectedItem).Mamau;
                                        pt.TenXe = txtten.Text;
                                        pt.NvUpdate = nv.MaNv;
                                        pt.SoLuongKho = int.Parse(txtsln.Text);
                                        pt.SoLuongNhap = int.Parse(txtsln.Text);

                                        pt.Description = txtMota.Text;
                                        pt.DonVi = txtDonVi.Text;
                                        pt.IdHangXe = ((HangXe)cbxBrand.SelectedItem).IdHangXe;
                                        pt.MaNcc = ((NhaCungCap)cbxNTT.SelectedItem).MaNcc;
                                        pt.UpdateAt = DateTime.Parse(DateTime.UtcNow.ToLocalTime().ToString("yyyy-MM-dd"));
                                    }


                                    #endregion


                                    var xm = db.XeMayKhos.SingleOrDefault(x => x.IdXmkho == idXe);
                                    if (xm != null)
                                    {
                                        xm.CongSuat = int.Parse(txtPhanKhoi.Text);
                                    }




                                    if (MessageBox.Show("Xác nhận sửa đổi!", "Thông báo", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                                    {
                                        db.SaveChanges();
                                        MessageBox.Show("Đã cập nhật thành công!", "Thong bao");
                                        LoadDataGrid();
                                    }
                                }
                                catch (Exception ex)
                                {

                                    MessageBox.Show(ex.Message);
                                }



                            }
                            else
                            {
                                MessageBox.Show("Bạn không có đủ thẩm quyền để sửa sản phẩm trong kho của người khác quản lý", "Thong bao");

                            }


                        }
                        else
                        {
                            var nv = db.NhanViens.SingleOrDefault(x => x.Taikhoan == tk.TaiKhoan);
                            if (nv != null)
                            {
                                var kh = db.Khos.SingleOrDefault(x => x.Id == txtNhapKHo.Text);
                                if (kh != null)
                                {
                                    #region Sua Kho_PT admin
                                    var pt = db.KhoPhuongTiens.SingleOrDefault(x => x.IdPt == id);
                                    if (pt != null)
                                    {
                                        pt.IdKho = kh.Id;
                                        pt.NamSx = int.Parse(cbxNamSX.SelectedItem.ToString());
                                        pt.Gia = float.Parse(txtgia.Text);
                                        pt.IdMau = ((Mau)cbxColor.SelectedItem).Mamau;
                                        pt.TenXe = txtten.Text;
                                        pt.NvUpdate = nv.MaNv;
                                        pt.SoLuongKho = int.Parse(txtsln.Text);
                                        pt.SoLuongNhap = int.Parse(txtsln.Text);

                                        pt.Description = txtMota.Text;
                                        pt.DonVi = txtDonVi.Text;
                                        pt.IdHangXe = ((HangXe)cbxBrand.SelectedItem).IdHangXe;
                                        pt.MaNcc = ((NhaCungCap)cbxNTT.SelectedItem).MaNcc;
                                        pt.UpdateAt = DateTime.Parse(DateTime.UtcNow.ToLocalTime().ToString("yyyy-MM-dd"));
                                    }
                                    #endregion


                                    var xm = db.XeMayKhos.SingleOrDefault(x => x.IdXmkho == idXe);
                                    if (xm != null)
                                    {
                                        xm.CongSuat = int.Parse(txtPhanKhoi.Text);
                                    }
                                    if (MessageBox.Show("Xác nhận sửa đổi!", "Thông báo", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                                    {
                                        db.SaveChanges();
                                        MessageBox.Show("Đã cập nhật thành công!", "Thong bao");
                                        LoadDataGrid();
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("Mã kho không tồn tại!", "Thông báo");

                                }
                            }
                        }


                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error: " + ex.Message);

                    }
                    #endregion
                }
                catch (Exception ex)
                {

                    MessageBox.Show("Error: " + ex.Message);
                }
            }
            #endregion
        }

        private void btnxoa_Click(object sender, RoutedEventArgs e)
        {
            if (dtgkhoxemay.SelectedItem != null)
            {
                try
                {
                    Type type = dtgkhoxemay.SelectedItem.GetType();
                    PropertyInfo[] propertyInfos = type.GetProperties();

                    string id = propertyInfos[0].GetValue(dtgkhoxemay.SelectedValue).ToString();
                    string idXe = propertyInfos[19].GetValue(dtgkhoxemay.SelectedValue).ToString();
                    string idNv = propertyInfos[8].GetValue(dtgkhoxemay.SelectedValue).ToString();
                    #region Delete
                    try
                    {
                        var tk = db.Accounts.SingleOrDefault(x => x.TaiKhoan == idUSer);

                        if (tk.Quyen == 0)
                        {
                            var nv = db.NhanViens.SingleOrDefault(x => x.Taikhoan == tk.TaiKhoan && x.MaNv == idNv);
                            if (nv != null)
                            {

                                try
                                {
                                    #region Sua Kho_PT admin
                                    var pt = db.KhoPhuongTiens.SingleOrDefault(x => x.IdPt == id);
                                    #endregion


                                    var xm = db.XeMayKhos.SingleOrDefault(x => x.IdXmkho == idXe);

                                    if (MessageBox.Show("Xac nhan xóa?", "Thong Bao", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                                    {
                                        db.Remove(xm);
                                        db.Remove(pt);
                                        db.SaveChanges();
                                        MessageBox.Show("Đã xóa thành công!", "Thong bao");
                                        LoadDataGrid();
                                    }
                                }
                                catch (Exception ex)
                                {

                                    MessageBox.Show(ex.Message);
                                }



                            }
                            else
                            {
                                MessageBox.Show("Bạn không có đủ thẩm quyền để xóa sản phẩm trong kho của người khác quản lý", "Thong bao");
                            }


                        }
                        else
                        {
                            var pt = db.KhoPhuongTiens.SingleOrDefault(x => x.IdPt == id);
                            var xm = db.XeMayKhos.SingleOrDefault(x => x.IdXmkho == idXe);
                            if (MessageBox.Show("Xac nhan xóa?", "Thong Bao", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                            {
                                db.Remove(xm);
                                db.Remove(pt);
                                db.SaveChanges();
                                MessageBox.Show("Đã xóa thành công!", "Thong bao");
                                LoadDataGrid();
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error: " + ex.Message);

                    }
                    #endregion
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }


        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            LoadKHO();
        }

        private void dtgkhoxemay_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            if (dtgkhoxemay.SelectedItem != null)
            {
                try
                {
                    Type type = dtgkhoxemay.SelectedItem.GetType();
                    PropertyInfo[] propertyInfos = type.GetProperties();

                    cbxBrand.Text = propertyInfos[2].GetValue(dtgkhoxemay.SelectedValue).ToString();
                    cbxColor.Text = propertyInfos[3].GetValue(dtgkhoxemay.SelectedValue).ToString();
                    txtDonVi.Text = propertyInfos[17].GetValue(dtgkhoxemay.SelectedValue).ToString();
                    txtgia.Text = propertyInfos[11].GetValue(dtgkhoxemay.SelectedValue).ToString();
                    txtMota.Text = propertyInfos[13].GetValue(dtgkhoxemay.SelectedValue).ToString();
                    txtNhapKHo.Text = propertyInfos[16].GetValue(dtgkhoxemay.SelectedValue).ToString();
                    txtPhanKhoi.Text = propertyInfos[5].GetValue(dtgkhoxemay.SelectedValue).ToString();
                    txtsln.Text = propertyInfos[10].GetValue(dtgkhoxemay.SelectedValue).ToString();
                    txtten.Text = propertyInfos[1].GetValue(dtgkhoxemay.SelectedValue).ToString();
                    cbxNamSX.Text = propertyInfos[4].GetValue(dtgkhoxemay.SelectedValue).ToString();
                    cbxNTT.Text = propertyInfos[7].GetValue(dtgkhoxemay.SelectedValue).ToString();


                }
                catch (Exception ex)
                {

                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {

            NavigationService.GoBack();
        }

        private void btnChuyen_Click(object sender, RoutedEventArgs e)
        {
            if (dtgkhoxemay.SelectedItem != null)
            {
                try
                {
                    Type type = dtgkhoxemay.SelectedItem.GetType();
                    PropertyInfo[] propertyInfos = type.GetProperties();

                    string id = propertyInfos[0].GetValue(dtgkhoxemay.SelectedValue).ToString();
                    string idXe = propertyInfos[19].GetValue(dtgkhoxemay.SelectedValue).ToString();

                    #region Update
                    try
                    {
                        try
                        {
                            var check = db.PhuongTiens.SingleOrDefault(x => x.IdPt == id);
                            var pt = db.KhoPhuongTiens.SingleOrDefault(x => x.IdPt == id);
                            if (check == null)
                            {
                                #region Sua Kho_PT 
                                PhuongTien ptCH = new PhuongTien();


                                if (pt != null)
                                {


                                    ptCH.IdPt = pt.IdPt;
                                    ptCH.NamSx = pt.NamSx;
                                    ptCH.Gia = pt.Gia;
                                    ptCH.Mamau = pt.IdMau;
                                    ptCH.TenXe = pt.TenXe;
                                    ptCH.Ngaynhap = pt.CreateAt;

                                    ptCH.Soluong = pt.SoLuongKho;
                                    ptCH.Mota = pt.Description;
                                    ptCH.Donvi = pt.DonVi;
                                    ptCH.IdHangXe = pt.IdHangXe;

                                    pt.SoLuongKho = 0;
                                }


                                #endregion

                                XeMay xmk = new XeMay();
                                var xm = db.XeMayKhos.SingleOrDefault(x => x.IdXmkho == idXe);
                                if (xm != null)
                                {


                                    xmk.IdPt = xm.IdPt;
                                    xmk.CongSuat = xm.CongSuat;
                                    xmk.IdXeMay = xm.IdXmkho;


                                }

                                if (MessageBox.Show("Xác nhận chuyển phương tiện này lên của hàng?", "Thong Bao", MessageBoxButton.YesNo, MessageBoxImage.Information) == MessageBoxResult.Yes)
                                {
                                    db.Add(xmk);
                                    db.Add(ptCH);
                                    db.SaveChanges();
                                    MessageBox.Show("Thành công!", "Thong bao");
                                    LoadDataGrid();
                                }
                            }
                            else
                            {
                                check.Soluong += pt.SoLuongKho;
                                pt.SoLuongKho = 0;
                                if (MessageBox.Show("Xác nhận chuyển thêm số lượng phương tiện này lên của hàng?", "Thong Bao", MessageBoxButton.YesNo, MessageBoxImage.Information) == MessageBoxResult.Yes)
                                {

                                    db.SaveChanges();
                                    MessageBox.Show("Thành công!", "Thong bao");
                                    LoadDataGrid();
                                }
                            }

                        }
                        catch (Exception ex)
                        {

                            MessageBox.Show(ex.Message);
                        }


                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error: " + ex.Message);

                    }
                    #endregion
                }
                catch (Exception ex)
                {

                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }
    }
}
