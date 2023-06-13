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
using System.Windows.Shapes;
using QLCHXE.Models;
using QLCHXE.Report;
using QLCHXE.wwwroot;


namespace QLCHXE.Shared
{
    /// <summary>
    /// Interaction logic for QLXeTai.xaml
    /// </summary>
    public partial class QLXeTai : Page
    {
        private readonly QLCHXeContext _db;

        private List<GridXeTai> listData;
        public string IdUser { get; set; }

        public void LoadCBXHSX()
        {
            var query = from i in _db.HangXes orderby i.Tenhanngxe ascending select i;
            cbxHangsx.ItemsSource = query.ToList();
            cbxHangsx.SelectedValuePath = "IdHangXe";
            cbxHangsx.DisplayMemberPath = "Tenhanngxe";
            cbxHangsx.SelectedIndex = 0;
        }

        public void CBXYears()
        {
            var years = Enumerable.Range(1950, DateTime.UtcNow.Year - 1949).ToList();
            cbxNamsx.ItemsSource = years;
            cbxNamsx.SelectedItem = years[years.Count - 1];
        }

        public void CBXColors()
        {
            cbxMau.ItemsSource = _db.Maus.Select(i => i).OrderBy(i => i.Tenmau).ToList();
            cbxMau.DisplayMemberPath = "Tenmau";
            cbxMau.SelectedValuePath = "Mamau";
            cbxMau.SelectedIndex = 0;
        }

        public void LoadDtgView()
        {

            var sql = from i in _db.PhuongTiens
                      join j in _db.XeTais on i.IdPt equals j.IdPt
                      join k in _db.HangXes on i.IdHangXe equals k.IdHangXe
                      join m in _db.Maus on i.Mamau equals m.Mamau
                      select new
                      {
                          IdMaXe = j.XeTai1,
                          IdPt = i.IdPt,
                          TenXe = i.TenXe,
                          HangXeDT = k.Tenhanngxe,
                          Namsx = i.NamSx,
                          GiaBan = i.Gia,
                          MauDT = m.Tenmau,
                          TrongTai = j.Trongtai,
                          NgayNhap = i.Ngaynhap.ToString(),
                          SLCon = i.Soluong,
                          DonVi = i.Donvi,
                          MoTa = i.Mota,
                          TinhTrang = (i.Soluong > 0) ? "Con hang" : "Het hang",

                      };


            listData = new List<GridXeTai>();
            listData.Clear();
            foreach (var item in sql)
            {
                listData.Add(new GridXeTai
                {
                    IdPt = item.IdPt,
                    IdMaXe = item.IdMaXe,
                    TenXe = item.TenXe,
                    HangXeDT = item.HangXeDT,
                    Namsx = (int)item.Namsx,
                    GiaBan = (double)item.GiaBan,
                    MauDT = item.MauDT,
                    Trongtai = (double)item.TrongTai,
                    NgayNhap = item.NgayNhap,
                    SLCon = (int)item.SLCon,
                    DonVi = item.DonVi,
                    MoTa = item.MoTa,
                    TinhTrang = item.TinhTrang,
                    isChecked = false
                });
            }


            dtgXeTai.ItemsSource = listData;

        }
        public QLXeTai()
        {
            InitializeComponent();
            _db = new QLCHXeContext();
            LoadCBXHSX();
            LoadDtgView();
            CBXColors();
            CBXYears();
        }

        private void btnThem_Click(object sender, RoutedEventArgs e)
        {

            NhapKhoXeTai khoXeMay = new NhapKhoXeTai { idUSer = this.IdUser};
            khoXeMay.btnBack.Visibility = Visibility.Visible;
            NavigationService.Navigate(khoXeMay);

        }

        private void btnSua_Click(object sender, RoutedEventArgs e)
        {

            if (dtgXeTai.SelectedItem != null)
            {
                if (string.IsNullOrEmpty(txtTrongTai.Text.Trim()) || string.IsNullOrEmpty(txtDonVi.Text.Trim()) || string.IsNullOrEmpty(txtGiaban.Text.Trim()) || string.IsNullOrEmpty(txtMota.Text.Trim()) || string.IsNullOrEmpty(txtTenXe.Text.Trim()))
                {
                    MessageBox.Show("Có ô chưa nhập dữ liệu!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                else
                {
                    try
                    {
                        Type type = dtgXeTai.SelectedItem.GetType();
                        PropertyInfo[] properties = type.GetProperties();

                        string idPT = properties[1].GetValue(dtgXeTai.SelectedItem).ToString();

                        var pt = _db.PhuongTiens.SingleOrDefault(i => i.IdPt.Equals(idPT));
                        var xm = _db.XeTais.SingleOrDefault(i => i.IdPt.Equals(idPT));

                        pt.NamSx = int.Parse(cbxNamsx.SelectedItem.ToString());
                        pt.Gia = float.Parse(txtGiaban.Text);
                        pt.Mamau = ((Mau)cbxMau.SelectedItem).Mamau;
                        pt.TenXe = txtTenXe.Text;
                        
                        
                        pt.Mota = txtMota.Text;
                        pt.Donvi = txtDonVi.Text;
                        pt.IdHangXe = ((HangXe)cbxHangsx.SelectedItem).IdHangXe;


                        xm.Trongtai = int.Parse(txtTrongTai.Text);

                        if (MessageBox.Show("Xac nhan sua phuong tien co ma: " + idPT, "Thong Bao", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                        {

                            _db.SaveChanges();
                            MessageBox.Show("Da sua phuong tien co ma: " + idPT);
                            LoadDtgView();
                        }



                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error: " + ex.Message, "Error");
                    }
                }

            }
        }

        private void btnXoa_Click(object sender, RoutedEventArgs e)
        {

            #region xoaXe
            int count = 0;
            foreach (var item in listData)
            {
                if (item.isChecked)
                {
                    count++;
                }
            }
            if (count <= 0)
            {
                if (dtgXeTai.SelectedItem != null)
                {
                    try
                    {
                        Type type = dtgXeTai.SelectedItem.GetType();
                        PropertyInfo[] properties = type.GetProperties();

                        string idPT = properties[1].GetValue(dtgXeTai.SelectedItem).ToString();

                        var sql = _db.PhuongTiens.SingleOrDefault(i => i.IdPt.Equals(idPT));
                        if (sql != null)
                        {
                            try
                            {
                                #region Them Kho_PT admin
                                var xeKho = _db.KhoPhuongTiens.SingleOrDefault(x => x.IdPt == sql.IdPt);
                                xeKho.SoLuongKho += sql.Soluong;
                                xeKho.UpdateAt = DateTime.Parse(DateTime.UtcNow.ToLocalTime().ToString("yyyy-MM-dd"));
                                #endregion
                            }
                            catch (Exception ex)
                            {

                                MessageBox.Show(ex.Message);
                            }
                        }
                        var query = _db.XeTais.SingleOrDefault(i => i.IdPt.Equals(idPT));



                        if (MessageBox.Show("Xác nhận chuyển xe vào kho ", "Thong Bao", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                        {
                            _db.Remove(query);
                            _db.Remove(sql);
                            _db.SaveChanges();
                            LoadDtgView();
                            MessageBox.Show("Thành công");

                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error: " + ex.Message, "Error");
                    }
                }
                else
                {
                    MessageBox.Show("Chua chon xe can xoa!", "Thong bao");
                }
            }
            else
            {
                #region delete muliple
                try
                {
                    if (MessageBox.Show("Xac nhan xoa tat ca cac phuong tien da chon!", "Thong bao", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                    {
                        foreach (var item in listData)
                        {
                            if (item.isChecked)
                            {


                                string idPT = item.IdPt;

                                var sql = _db.PhuongTiens.SingleOrDefault(i => i.IdPt.Equals(idPT));
                                var query = _db.XeTais.SingleOrDefault(i => i.IdPt.Equals(idPT));

                                if (sql != null)
                                {
                                    try
                                    {
                                        #region Them Kho_PT admin
                                        var xeKho = _db.KhoPhuongTiens.SingleOrDefault(x => x.IdPt == sql.IdPt);
                                        xeKho.SoLuongKho += sql.Soluong;
                                        xeKho.UpdateAt = DateTime.Parse(DateTime.UtcNow.ToLocalTime().ToString("yyyy-MM-dd"));
                                        #endregion

                                    }
                                    catch (Exception ex)
                                    {

                                        MessageBox.Show(ex.Message);
                                    }
                                }

                                _db.Remove(query);
                                _db.Remove(sql);
                                _db.SaveChanges();
                                LoadDtgView();

                            }
                        }

                        MessageBox.Show("Các phương tiện được chọn đã chuyển vào kho!", "Thong bao");
                    }



                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message, "Error");
                }
                #endregion
            }


            #endregion

        }

        private void btnNhapMoi_Click(object sender, RoutedEventArgs e)
        {

            txtTenXe.Text = "";
            
            txtMota.Text = "";
            txtGiaban.Text = "";
            txtDonVi.Text = "";
            txtTrongTai.Text = "";
            cbxHangsx.SelectedIndex = 0;
            cbxMau.SelectedIndex = 0;
            cbxNamsx.SelectedIndex = cbxNamsx.Items.Count - 1;
            
            LoadDtgView();
        }

        private void dtgXeTai_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            if (dtgXeTai.SelectedItem != null)
            {
                try
                {
                    Type type = dtgXeTai.SelectedItem.GetType();
                    PropertyInfo[] propertyInfos = type.GetProperties();

                    txtTenXe.Text = propertyInfos[2].GetValue(dtgXeTai.SelectedValue).ToString();
                    txtGiaban.Text = propertyInfos[5].GetValue(dtgXeTai.SelectedValue).ToString();
                    txtDonVi.Text = propertyInfos[10].GetValue(dtgXeTai.SelectedValue).ToString();
                    txtTrongTai.Text = propertyInfos[7].GetValue(dtgXeTai.SelectedValue).ToString();
                    
                    txtMota.Text = propertyInfos[11].GetValue(dtgXeTai.SelectedValue).ToString();
                    
                    cbxHangsx.Text = propertyInfos[3].GetValue(dtgXeTai.SelectedValue).ToString();
                    cbxNamsx.Text = propertyInfos[4].GetValue(dtgXeTai.SelectedValue).ToString();
                    cbxMau.Text = propertyInfos[6].GetValue(dtgXeTai.SelectedValue).ToString();


                }
                catch (Exception ex)
                {

                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }
        public List<orderNow> listOr = new List<orderNow>();
        private void btnSell_Click(object sender, RoutedEventArgs e)
        {
            #region Ban xe
            var check = 0;

            orderNow order;
            int count = 0;
            foreach (var item in listData)
            {
                if (item.isChecked)
                {
                    count++;
                }
            }
            if (count <= 0)
            {
                if (dtgXeTai.SelectedItem != null)
                {
                    try
                    {
                        Type type = dtgXeTai.SelectedItem.GetType();
                        PropertyInfo[] properties = type.GetProperties();

                        string idPT = properties[1].GetValue(dtgXeTai.SelectedItem).ToString();

                        var sql = _db.PhuongTiens.SingleOrDefault(i => i.IdPt.Equals(idPT));
                        if (sql != null)
                        {
                            try
                            {
                                #region Them Kho_PT admin
                                var xeKho = _db.PhuongTiens.SingleOrDefault(x => x.IdPt == sql.IdPt);
                                if (xeKho != null)
                                {


                                    if (xeKho.Soluong == 0)
                                    {
                                        MessageBox.Show("Hàng này đã hết!", "Thông báo");
                                    }
                                    else
                                    {
                                        if (MessageBox.Show("Xác nhận tạo hóa đơn cho đơn hàng này ", "Thong Bao", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                                        {
                                            listOr.Clear();

                                            order = new orderNow(xeKho.TenXe, (double)xeKho.Gia, 1, (double)xeKho.Gia * 1);
                                            listOr.Add(order);
                                            xeKho.Soluong += -1;
                                            _db.SaveChanges();
                                            LoadDtgView();
                                            ThongKe thongKe = new ThongKe { orderNows = this.listOr };
                                            thongKe.Show();
                                        }
                                    }
                                }

                                #endregion
                            }
                            catch (Exception ex)
                            {

                                MessageBox.Show(ex.Message);
                            }
                        }





                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error: " + ex.Message, "Error");
                    }
                }
                else
                {
                    MessageBox.Show("Chưa chọn xe để bán!", "Thong bao");
                }
            }
            else
            {
                #region delete muliple
                try
                {
                    if (MessageBox.Show("Xac nhan tạo hóa đơn!", "Thong bao", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                    {
                        listOr.Clear();
                        foreach (var item in listData)
                        {
                            if (item.isChecked)
                            {


                                string idPT = item.IdPt;

                                var sql = _db.PhuongTiens.SingleOrDefault(i => i.IdPt.Equals(idPT));


                                if (sql != null)
                                {
                                    try
                                    {
                                        #region Them Kho_PT admin
                                        var xeKho = _db.PhuongTiens.SingleOrDefault(x => x.IdPt == sql.IdPt);
                                        if (xeKho != null)
                                        {



                                            if (xeKho.Soluong == 0)
                                            {
                                                MessageBox.Show("Hàng được chọn đã bán hết, vui lòng chọn lại!", "Thông báo");
                                                check = 0;
                                                break;
                                            }
                                            else
                                            {

                                                order = new orderNow(xeKho.TenXe, (double)xeKho.Gia, 1, (double)xeKho.Gia * 1);
                                                xeKho.Soluong += -1;
                                                listOr.Add(order);
                                                check++;
                                            }
                                        }

                                        #endregion

                                    }
                                    catch (Exception ex)
                                    {

                                        MessageBox.Show(ex.Message);
                                    }
                                }





                            }
                        }
                        if (check != 0)
                        {
                            _db.SaveChanges();
                            LoadDtgView();
                            ThongKe thongKe = new ThongKe { orderNows = this.listOr };
                            thongKe.Show();
                        }

                    }



                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message, "Error");
                }
                #endregion
            }


            #endregion
        }
    }
}
