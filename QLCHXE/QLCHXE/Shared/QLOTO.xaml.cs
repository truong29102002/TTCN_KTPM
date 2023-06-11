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
using QLCHXE.wwwroot;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace QLCHXE.Shared
{
    /// <summary>
    /// Interaction logic for QLOTO.xaml
    /// </summary>
    public partial class QLOTO : Page
    {
        private readonly QLCHXeContext _db;
        private List<GridQLOTO> listData;
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
                      join j in _db.Otos on i.IdPt equals j.IdPt
                      join k in _db.HangXes on i.IdHangXe equals k.IdHangXe
                      join m in _db.Maus on i.Mamau equals m.Mamau
                      select new
                      {
                          IdMaXe = j.IdOto,
                          IdPt = i.IdPt,
                          TenXe = i.TenXe,
                          HangXeDT = k.Tenhanngxe,
                          Namsx = i.NamSx,
                          GiaBan = i.Gia,
                          MauDT = m.Tenmau,
                          DongCo = j.Kieudongco,
                          SoChoNgoi = j.Sochongoi,
                          NgayNhap = i.Ngaynhap.ToString(),
                          SLCon = i.Soluong,
                          DonVi = i.Donvi,
                          MoTa = i.Mota,
                          TinhTrang = (i.Soluong > 0) ? "Con hang" : "Het hang"
                      };

            listData = new List<GridQLOTO>();
            listData.Clear();
            foreach (var item in sql)
            {
                listData.Add(new GridQLOTO
                {
                    IdPt = item.IdPt,
                    IdMaXe = item.IdMaXe,
                    TenXe = item.TenXe,
                    HangXeDT = item.HangXeDT,
                    Namsx = (int)item.Namsx,
                    GiaBan = (double)item.GiaBan,
                    MauDT = item.MauDT,
                    Kieudongco = item.DongCo,
                    Sochongoi = (int)item.SoChoNgoi,
                    NgayNhap = item.NgayNhap,
                    SLCon = (int)item.SLCon,
                    DonVi = item.DonVi,
                    MoTa = item.MoTa,
                    TinhTrang = item.TinhTrang,
                    isChecked = false
                });
            }

            dtgQLXoto.ItemsSource = listData;
        }
        public QLOTO()
        {
            InitializeComponent();
            _db = new QLCHXeContext();
            LoadCBXHSX();
            CBXYears();
            CBXColors();
            LoadDtgView();
        }

        private void btnThem_Click(object sender, RoutedEventArgs e)
        {
            NhapKhoOTO khoXeMay = new NhapKhoOTO();
            khoXeMay.btnBack.Visibility = Visibility.Visible;
            NavigationService.Navigate(khoXeMay);

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
                if (dtgQLXoto.SelectedItem != null)
                {
                    try
                    {
                        Type type = dtgQLXoto.SelectedItem.GetType();
                        PropertyInfo[] properties = type.GetProperties();

                        string idPT = properties[1].GetValue(dtgQLXoto.SelectedItem).ToString();

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
                        var query = _db.Otos.SingleOrDefault(i => i.IdPt.Equals(idPT));



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
                                var query = _db.Otos.SingleOrDefault(i => i.IdPt.Equals(idPT));

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
            txtSoluong.Text = "";
            txtMota.Text = "";
            txtGiaban.Text = "";
            txtDonVi.Text = "";
            txtDongCo.Text = "";
            txtSoChoNgoi.Text = "";
            cbxHangsx.SelectedIndex = 0;
            cbxMau.SelectedIndex = 0;
            cbxNamsx.SelectedIndex = cbxNamsx.Items.Count - 1;
            
            LoadDtgView();
        }

        private void btnSua_Click(object sender, RoutedEventArgs e)
        {
            if (dtgQLXoto.SelectedItem != null)
            {
                if (string.IsNullOrEmpty(txtDongCo.Text.Trim()) || string.IsNullOrEmpty(txtDonVi.Text.Trim()) || string.IsNullOrEmpty(txtGiaban.Text.Trim()) || string.IsNullOrEmpty(txtMota.Text.Trim()) || string.IsNullOrEmpty(txtSoChoNgoi.Text.Trim()) || string.IsNullOrEmpty(txtSoluong.Text.Trim()) || string.IsNullOrEmpty(txtTenXe.Text.Trim()) )
                {
                    MessageBox.Show("Có ô chưa nhập dữ liệu!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                else
                {
                    try
                    {
                        Type type = dtgQLXoto.SelectedItem.GetType();
                        PropertyInfo[] properties = type.GetProperties();

                        string idPT = properties[1].GetValue(dtgQLXoto.SelectedItem).ToString();

                        var pt = _db.PhuongTiens.SingleOrDefault(i => i.IdPt.Equals(idPT));
                        var xm = _db.Otos.SingleOrDefault(i => i.IdPt.Equals(idPT));

                        pt.NamSx = int.Parse(cbxNamsx.SelectedItem.ToString());
                        pt.Gia = float.Parse(txtGiaban.Text);
                        pt.Mamau = ((Mau)cbxMau.SelectedItem).Mamau;
                        pt.TenXe = txtTenXe.Text;
                        
                        pt.Soluong = int.Parse(txtSoluong.Text);
                        pt.Mota = txtMota.Text;
                        pt.Donvi = txtDonVi.Text;
                        pt.IdHangXe = ((HangXe)cbxHangsx.SelectedItem).IdHangXe;


                        xm.Sochongoi = int.Parse(txtSoChoNgoi.Text);
                        xm.Kieudongco = txtDongCo.Text;

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
            else
            {
                MessageBox.Show("Chua chon thong tin xe can sua!", "Thong bao");
            }
        }

        private void dtgQLXoto_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            if (dtgQLXoto.SelectedItem != null)
            {
                try
                {
                    Type type = dtgQLXoto.SelectedItem.GetType();
                    PropertyInfo[] propertyInfos = type.GetProperties();

                    txtTenXe.Text = propertyInfos[2].GetValue(dtgQLXoto.SelectedValue).ToString();
                    txtGiaban.Text = propertyInfos[5].GetValue(dtgQLXoto.SelectedValue).ToString();
                    txtDonVi.Text = propertyInfos[11].GetValue(dtgQLXoto.SelectedValue).ToString();
                    txtDongCo.Text = propertyInfos[7].GetValue(dtgQLXoto.SelectedValue).ToString();
                    txtSoChoNgoi.Text = propertyInfos[8].GetValue(dtgQLXoto.SelectedValue).ToString();
                    txtSoluong.Text = propertyInfos[10].GetValue(dtgQLXoto.SelectedValue).ToString();
                    txtMota.Text = propertyInfos[12].GetValue(dtgQLXoto.SelectedValue).ToString();
                    
                    cbxHangsx.Text = propertyInfos[3].GetValue(dtgQLXoto.SelectedValue).ToString();
                    cbxNamsx.Text = propertyInfos[4].GetValue(dtgQLXoto.SelectedValue).ToString();
                    cbxMau.Text = propertyInfos[6].GetValue(dtgQLXoto.SelectedValue).ToString();


                }
                catch (Exception ex)
                {

                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }
    }
}