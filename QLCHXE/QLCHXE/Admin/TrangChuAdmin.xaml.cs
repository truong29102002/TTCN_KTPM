using Microsoft.Win32;
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
using QLCHXE.Shared;

namespace QLCHXE.Admin
{
    /// <summary>
    /// Interaction logic for TrangChuAdmin.xaml
    /// </summary>
    public partial class TrangChuAdmin : Window
    {
        public string idUser { get; set; }

        private Dictionary<Button, Brush> buttonColors;

        public TrangChuAdmin()
        {
            InitializeComponent();
            InitializeButtonColors();
        }
        private void InitializeButtonColors()
        {
            buttonColors = new Dictionary<Button, Brush>();
            buttonColors.Add(Btn_DangXuat, Brushes.Transparent);
            buttonColors.Add(Btn_DoiMK, Brushes.Transparent);
            buttonColors.Add(Btn_NhapOto, Brushes.Transparent);
            buttonColors.Add(Btn_NhapXeMay, Brushes.Transparent);
            buttonColors.Add(Btn_NhapXeTai, Brushes.Transparent);
            buttonColors.Add(Btn_QuanLyBanXe, Brushes.Transparent);
            buttonColors.Add(Btn_QuanLyNV, Brushes.Transparent);
            buttonColors.Add(Btn_QuanLyOto, Brushes.Transparent);
            buttonColors.Add(Btn_QuanLyXeMay, Brushes.Transparent);
            buttonColors.Add(Btn_QuanLyXetai, Brushes.Transparent);
            buttonColors.Add(Btn_ThongKeDT, Brushes.Transparent);
        }
        private void Btn_QuanLyNV_Click(object sender, RoutedEventArgs e)
        {
            mainFrame.Navigate(new QLNV());
            Button clickedButton = sender as Button;
            if (buttonColors.ContainsKey(clickedButton))
            {
                foreach (var button in buttonColors.Keys)
                {
                    if (button == clickedButton)
                        buttonColors[button] = Brushes.AliceBlue; // Màu khi được click
                    else
                        buttonColors[button] = Brushes.Transparent; // Màu khi không được click
                }
            }

            UpdateButtonColors();
        }

        private void Btn_QuanLyXeMay_Click(object sender, RoutedEventArgs e)
        {
            mainFrame.Navigate(new QLXeMay());
            Button clickedButton = sender as Button;
            if (buttonColors.ContainsKey(clickedButton))
            {
                foreach (var button in buttonColors.Keys)
                {
                    if (button == clickedButton)
                        buttonColors[button] = Brushes.AliceBlue; // Màu khi được click
                    else
                        buttonColors[button] = Brushes.Transparent; // Màu khi không được click
                }
            }

            UpdateButtonColors(); ;
        }

        private void Btn_QuanLyOto_Click(object sender, RoutedEventArgs e)
        {
            mainFrame.Navigate(new QLOTO());
            Button clickedButton = sender as Button;
            if (buttonColors.ContainsKey(clickedButton))
            {
                foreach (var button in buttonColors.Keys)
                {
                    if (button == clickedButton)
                        buttonColors[button] = Brushes.AliceBlue; // Màu khi được click
                    else
                        buttonColors[button] = Brushes.Transparent; // Màu khi không được click
                }
            }

            UpdateButtonColors();
        }
        private void Btn_DoiMK_Click(object sender, RoutedEventArgs e)
        {
            mainFrame.Navigate(new DOIMK());
            Button clickedButton = sender as Button;
            if (buttonColors.ContainsKey(clickedButton))
            {
                foreach (var button in buttonColors.Keys)
                {
                    if (button == clickedButton)
                        buttonColors[button] = Brushes.AliceBlue; // Màu khi được click
                    else
                        buttonColors[button] = Brushes.Transparent; // Màu khi không được click
                }
            }

            UpdateButtonColors();
        }
        private void Btn_DangXuat_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            mainWindow.Show();

            this.Close();
        }

        private void close_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }

        private void hidden_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void Btn_QuanLyXetai_Click(object sender, RoutedEventArgs e)
        {
            mainFrame.Navigate(new QLXeTai());
            Button clickedButton = sender as Button;
            if (buttonColors.ContainsKey(clickedButton))
            {
                foreach (var button in buttonColors.Keys)
                {
                    if (button == clickedButton)
                        buttonColors[button] = Brushes.AliceBlue; // Màu khi được click
                    else
                        buttonColors[button] = Brushes.Transparent; // Màu khi không được click
                }
            }

            UpdateButtonColors();
        }

        private void Btn_NhapXeMay_Click(object sender, RoutedEventArgs e)
        {
            Button clickedButton = sender as Button;
            if (buttonColors.ContainsKey(clickedButton))
            {
                foreach (var button in buttonColors.Keys)
                {
                    if (button == clickedButton)
                        buttonColors[button] = Brushes.AliceBlue; // Màu khi được click
                    else
                        buttonColors[button] = Brushes.Transparent; // Màu khi không được click
                }
            }

            UpdateButtonColors();
        }

        private void Btn_NhapOto_Click(object sender, RoutedEventArgs e)
        {
            Button clickedButton = sender as Button;
            if (buttonColors.ContainsKey(clickedButton))
            {
                foreach (var button in buttonColors.Keys)
                {
                    if (button == clickedButton)
                        buttonColors[button] = Brushes.AliceBlue; // Màu khi được click
                    else
                        buttonColors[button] = Brushes.Transparent; // Màu khi không được click
                }
            }

            UpdateButtonColors();
        }

        private void Btn_NhapXeTai_Click(object sender, RoutedEventArgs e)
        {
            Button clickedButton = sender as Button;
            if (buttonColors.ContainsKey(clickedButton))
            {
                foreach (var button in buttonColors.Keys)
                {
                    if (button == clickedButton)
                        buttonColors[button] = Brushes.AliceBlue; // Màu khi được click
                    else
                        buttonColors[button] = Brushes.Transparent; // Màu khi không được click
                }
            }

            UpdateButtonColors();
        }

        private void Btn_ThongKeDT_Click(object sender, RoutedEventArgs e)
        {
            Button clickedButton = sender as Button;
            if (buttonColors.ContainsKey(clickedButton))
            {
                foreach (var button in buttonColors.Keys)
                {
                    if (button == clickedButton)
                        buttonColors[button] = Brushes.AliceBlue; // Màu khi được click
                    else
                        buttonColors[button] = Brushes.Transparent; // Màu khi không được click
                }
            }

            UpdateButtonColors();
        }

        private void Btn_QuanLyBanXe_Click(object sender, RoutedEventArgs e)
        {
            Button clickedButton = sender as Button;
            if (buttonColors.ContainsKey(clickedButton))
            {
                foreach (var button in buttonColors.Keys)
                {
                    if (button == clickedButton)
                        buttonColors[button] = Brushes.AliceBlue; // Màu khi được click
                    else
                        buttonColors[button] = Brushes.Transparent; // Màu khi không được click
                }
            }

            UpdateButtonColors();
        }

        private void UpdateButtonColors()
        {
            foreach (var pair in buttonColors)
            {
                pair.Key.Background = pair.Value;
            }
        }
      

    }
}
