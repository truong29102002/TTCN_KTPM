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
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using QLCHXE.Models;
using QLCHXE.wwwroot;
using static System.Net.Mime.MediaTypeNames;

namespace QLCHXE.Shared
{
    /// <summary>
    /// Interaction logic for QLXeMay.xaml
    /// </summary>
    public partial class QLXeMay : Page
    {
        private readonly QLCHXeContext _db;
        private List<DataGridSource> listData;
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
                      join j in _db.XeMays on i.IdPt equals j.IdPt
                      join k in _db.HangXes on i.IdHangXe equals k.IdHangXe
                      join m in _db.Maus on i.Mamau equals m.Mamau
                      select new
                      {
                          IdMaXe = j.IdXeMay,
                          IdPt = i.IdPt,
                          TenXe = i.TenXe,
                          HangXeDT = k.Tenhanngxe,
                          Namsx = i.NamSx,
                          GiaBan = i.Gia,
                          MauDT = m.Tenmau,
                          CongSuat = j.CongSuat,
                          NgayNhap = i.Ngaynhap.ToString(),
                          SLCon = i.Soluong,
                          DonVi = i.Donvi,
                          MoTa = i.Mota,
                          TinhTrang = (i.Soluong > 0) ? "Con hang" : "Het hang",
                      };
            listData = new List<DataGridSource>();
            listData.Clear();
            foreach (var item in sql)
            {
                listData.Add(new DataGridSource
                {
                    IdPt = item.IdPt,
                    IdMaXe = item.IdMaXe,
                    TenXe = item.TenXe,
                    HangXeDT = item.HangXeDT,
                    Namsx = (int)item.Namsx,
                    GiaBan = (double)item.GiaBan,
                    MauDT = item.MauDT,
                    CongSuat = (int)item.CongSuat,
                    NgayNhap = item.NgayNhap,
                    SLCon = (int)item.SLCon,
                    DonVi = item.DonVi,
                    MoTa = item.MoTa,
                    TinhTrang = item.TinhTrang,
                    isChecked = false
                });
            }

            dtgQLXM.ItemsSource = listData;

        }

        public QLXeMay()
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
            if (string.IsNullOrEmpty(txtCongsuat.Text.Trim()) || string.IsNullOrEmpty(txtDonVi.Text.Trim()) || string.IsNullOrEmpty(txtGiaban.Text.Trim()) || string.IsNullOrEmpty(txtMota.Text.Trim()) || string.IsNullOrEmpty(txtSoluong.Text.Trim()) || string.IsNullOrEmpty(txtTenXe.Text.Trim()) || string.IsNullOrEmpty(dateNgayNhap.Text.Trim()))
            {
                MessageBox.Show("Có ô chưa nhập dữ liệu!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
                try
                {
                    string idPT = "PT" + (_db.PhuongTiens.Count() + RandomNumberGenerator.GetInt32(1000, 9999)).ToString();
                    PhuongTien pt = new PhuongTien();

                    pt.IdPt = idPT;
                    pt.NamSx = int.Parse(cbxNamsx.SelectedItem.ToString());
                    pt.Gia = float.Parse(txtGiaban.Text);
                    pt.Mamau = ((Mau)cbxMau.SelectedItem).Mamau;
                    pt.TenXe = txtTenXe.Text;
                    pt.Ngaynhap = DateTime.Parse(dateNgayNhap.SelectedDate.Value.ToString("yyyy-MM-dd"));
                    pt.Soluong = int.Parse(txtSoluong.Text);
                    pt.Mota = txtMota.Text;
                    pt.Donvi = txtDonVi.Text;
                    pt.IdHangXe = ((HangXe)cbxHangsx.SelectedItem).IdHangXe;

                    _db.Add(pt);

                    XeMay xm = new XeMay();

                    xm.IdPt = idPT;
                    xm.CongSuat = int.Parse(txtCongsuat.Text);
                    xm.IdXeMay = "XM" + (_db.XeMays.Count() + RandomNumberGenerator.GetInt32(1000, 9999)).ToString();

                    _db.Add(xm);
                    _db.SaveChanges();
                    LoadDtgView();
                    MessageBox.Show("Them thanh cong xe moi", "Thong bao");

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);

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
                if (dtgQLXM.SelectedItem != null)
                {
                    try
                    {
                        Type type = dtgQLXM.SelectedItem.GetType();
                        PropertyInfo[] properties = type.GetProperties();

                        string idPT = properties[1].GetValue(dtgQLXM.SelectedItem).ToString();

                        var sql = _db.PhuongTiens.SingleOrDefault(i => i.IdPt.Equals(idPT));
                        var query = _db.XeMays.SingleOrDefault(i => i.IdPt.Equals(idPT));
                        var qr = _db.HoaDonChiTiets.SingleOrDefault(i => i.IdPt.Equals(idPT));

                        if (qr != null && sql != null && query != null)
                        {
                            if (MessageBox.Show("Xac nhan xoa phuong tien co ma: " + idPT, "Thong Bao", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                            {
                                _db.Remove(query);
                                _db.Remove(qr);
                                _db.Remove(sql);
                                _db.SaveChanges();
                                LoadDtgView();
                                MessageBox.Show("Da xoa phuong tien co ma: " + idPT);
                                LoadDtgView();
                            }

                        }
                        else
                        {
                            if (MessageBox.Show("Xac nhan xoa phuong tien co ma: " + idPT, "Thong Bao", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                            {
                                _db.Remove(query);
                                _db.Remove(sql);
                                _db.SaveChanges();
                                MessageBox.Show("Da xoa phuong tien co ma: " + idPT);
                                LoadDtgView();
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
                    MessageBox.Show("Chua chon xe can xoa!", "Thong bao");
                }
            }
            else
            {
                #region delete muliple
                try
                {
                    if(MessageBox.Show("Xac nhan xoa tat ca cac phuong tien da chon!","Thong bao", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                    {
                        foreach (var item in listData)
                        {
                            if (item.isChecked)
                            {


                                string idPT = item.IdPt;

                                var sql = _db.PhuongTiens.SingleOrDefault(i => i.IdPt.Equals(idPT));
                                var query = _db.XeMays.SingleOrDefault(i => i.IdPt.Equals(idPT));
                                var qr = _db.HoaDonChiTiets.SingleOrDefault(i => i.IdPt.Equals(idPT));

                                if (qr != null && sql != null && query != null)
                                {
                                    _db.Remove(query);
                                    _db.Remove(qr);
                                    _db.Remove(sql);
                                    _db.SaveChanges();
                                    LoadDtgView();
                                }
                                else
                                {

                                    _db.Remove(query);
                                    _db.Remove(sql);
                                    _db.SaveChanges();
                                    LoadDtgView();
                                }
                            }
                        }

                        MessageBox.Show("Da xoa tat ca phuong tien duoc chon", "Thong bao");
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

        private void btnSua_Click(object sender, RoutedEventArgs e)
        {
            if (dtgQLXM.SelectedItem != null)
            {
                if (string.IsNullOrEmpty(txtCongsuat.Text.Trim()) || string.IsNullOrEmpty(txtDonVi.Text.Trim()) || string.IsNullOrEmpty(txtGiaban.Text.Trim()) || string.IsNullOrEmpty(txtMota.Text.Trim()) || string.IsNullOrEmpty(txtSoluong.Text.Trim()) || string.IsNullOrEmpty(txtTenXe.Text.Trim()) || string.IsNullOrEmpty(dateNgayNhap.Text.Trim()))
                {
                    MessageBox.Show("Có ô chưa nhập dữ liệu!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                else
                {
                    try
                    {
                        Type type = dtgQLXM.SelectedItem.GetType();
                        PropertyInfo[] properties = type.GetProperties();

                        string idPT = properties[1].GetValue(dtgQLXM.SelectedItem).ToString();

                        var pt = _db.PhuongTiens.SingleOrDefault(i => i.IdPt.Equals(idPT));
                        var xm = _db.XeMays.SingleOrDefault(i => i.IdPt.Equals(idPT));

                        pt.NamSx = int.Parse(cbxNamsx.SelectedItem.ToString());
                        pt.Gia = float.Parse(txtGiaban.Text);
                        pt.Mamau = ((Mau)cbxMau.SelectedItem).Mamau;
                        pt.TenXe = txtTenXe.Text;
                        pt.Ngaynhap = DateTime.Parse(dateNgayNhap.SelectedDate.Value.ToString("yyyy-MM-dd"));
                        pt.Soluong = int.Parse(txtSoluong.Text);
                        pt.Mota = txtMota.Text;
                        pt.Donvi = txtDonVi.Text;
                        pt.IdHangXe = ((HangXe)cbxHangsx.SelectedItem).IdHangXe;


                        xm.CongSuat = int.Parse(txtCongsuat.Text);

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

        private void btnNhapMoi_Click(object sender, RoutedEventArgs e)
        {
            txtTenXe.Text = "";
            txtSoluong.Text = "";
            txtMota.Text = "";
            txtGiaban.Text = "";
            txtDonVi.Text = "";
            txtCongsuat.Text = "";
            cbxHangsx.SelectedIndex = 0;
            cbxMau.SelectedIndex = 0;
            cbxNamsx.SelectedIndex = cbxNamsx.Items.Count - 1;
            dateNgayNhap.Text = "";
            LoadDtgView();
        }

        private void dtgQLXM_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            if (dtgQLXM.SelectedItem != null)
            {
                try
                {
                    Type type = dtgQLXM.SelectedItem.GetType();
                    PropertyInfo[] propertyInfos = type.GetProperties();

                    txtTenXe.Text = propertyInfos[2].GetValue(dtgQLXM.SelectedValue).ToString();
                    txtGiaban.Text = propertyInfos[5].GetValue(dtgQLXM.SelectedValue).ToString();
                    txtDonVi.Text = propertyInfos[10].GetValue(dtgQLXM.SelectedValue).ToString();
                    txtCongsuat.Text = propertyInfos[7].GetValue(dtgQLXM.SelectedValue).ToString();
                    txtSoluong.Text = propertyInfos[9].GetValue(dtgQLXM.SelectedValue).ToString();
                    txtMota.Text = propertyInfos[11].GetValue(dtgQLXM.SelectedValue).ToString();
                    dateNgayNhap.Text = propertyInfos[8].GetValue(dtgQLXM.SelectedValue).ToString();
                    cbxHangsx.Text = propertyInfos[3].GetValue(dtgQLXM.SelectedValue).ToString();
                    cbxNamsx.Text = propertyInfos[4].GetValue(dtgQLXM.SelectedValue).ToString();
                    cbxMau.Text = propertyInfos[6].GetValue(dtgQLXM.SelectedValue).ToString();


                }
                catch (Exception ex)
                {

                    MessageBox.Show("Error: " + ex.Message);
                }
            }

        }
    }
}
