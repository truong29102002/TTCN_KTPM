   M S B A M L   `   `   `  ����A  =QLCHXE, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:QLCHXE.AdminQLCHXE.Admin  5      $    QLCHXE.Admin.NhanVienQuanLyKho   5      W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      ,localclr-namespace:QLCHXE.Admin     5      _ [MaterialDesignThemes.Wpf, Version=4.9.0.0, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5        N� Title$  NhanVienQuanLyKho��5
      .��5      �  � RowDefinitions 5   
   �� 5      $��50 �6   �� 5      $��1* �6   .+�5   
   �� $��0��6    �� Orientation$ 
Horizontal=�6"   $��Center=�6;   .+�5      �� $
��Left=�6   $��30(�63   $
��Wrap=�6A   $&�� Nhân viên quản lý trong kho��6U   $
��Bold"�6x   $	��Top=�6�   ���	#FFA3AE2E6�   5      �� 5   
   $��1��6   $ Vertical=�6"   .+�5      �� $ 
Horizontal=�6   $��20q�62   .+�5      �� $
��Left=�6   $
��Wrap=�67   $��Mã Nhân viên: ��6K   $	��Top=�6a   �� 5      -   5         ��Name$ txtMANV��5   I   $
��Left=�6    �� TextWrapping$
 Wrap=�65   $	��Top=�6Z   $	��120��6r   $��	0 -10 0 0q�6~   5      �� 5      $ 
Horizontal=�6   $��20q�62   .+�5      �� $
��Left=�6   $
��Wrap=�67   $��	Tên kho:��6K   $	��Top=�6[   �� 5      -   5        $ 	txtTenKHo��5   I   $
��Left=�6   $
 Wrap=�65   $	��Top=�6\   $	��120��6t   $��	5 -10 0 0q�6�   5      �� 5      $ 
Horizontal=�6   $��20q�62   .+�5      �� $
��Left=�6   $
��Wrap=�67   $��Địa chỉ:��6K   $	��Top=�6[   �� 5!      -   5        $ 	txtDiaChi��5!   I   $
��Left=�6   $
 Wrap=�65   $	��Top=�6]   $	��120��6u   $��	5 -10 0 0q�6�   5"      �� 5%      $ 
Horizontal=�6   $��20q�62   .+�5&      �� $
��Left=�6   $
��Wrap=�67   $��Diện tích kho:��6K   $	��Top=�6a   �� 5'      -   5        $ txtDienTich��5'   I   $
��Left=�6   $
 Wrap=�65   $	��Top=�6^   $	��120��6v   $��	5 -10 0 0q�6�   5(      �� 5*      $ 
Horizontal=�6   $��20q�62   .+�5+      �� -   5         ��Name$ btnThem��5+   ,   $��	20 0 20 0q�6   $
��Left=�6=   $	��Top=�6X   $��28��6p   .��5,      �� $ 
Horizontal=�6!   .+�5-      '  !MaterialDesignThemes.Wpf.PackIcon     Kind$	 Add=�62   �� 5.      $��Thêm��6$   5/      50      �� 51      -   5        $ btnSua��51   ,   $��	20 0 20 0q�6   $
��Left=�6<   $	��Top=�6W   $��28��6o   .��52      �� $ 
Horizontal=�6!   .+�53        $ Create=�62   �� 54      $��Update��6$   55      56      �� 57      -   5        $ btnXoa��57   ,   $��	20 0 20 0q�6   $
��Left=�6<   $	��Top=�6W   $��28��6o   .��58      �� $ 
Horizontal=�6!   .+�59        $ Delete=�62   �� 5:      $
��Xóa��6$   5;      5<      5=      �� 5?      $��20q�6   .+�5@      &   System.Windows.Controls.DataGrid  -   5          Name$ dtgNVKHo��5@   K      MaterialDesignDataGrid#��[  6      
IsReadOnly$
 True��6]   $	��200��6o   	   AutoGenerateColumns$	 False��6|   
   Columns
 5A      0  *System.Windows.Controls.DataGridTextColumn  5B      ,  &System.Windows.Controls.DataGridColumn   Header$ Mã kho��6-   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6=   �� *idKho+  5C      $ Tên kho��6-    6>   �� *TenKho+  5D      $ Nhân viên phụ trách��6-    6J   �� *NV+  5E      $ Địa chỉ��6-    6>   �� *DiaChi+  5F      $ Diện tích kho (m2)��6-      Width5  /System.Windows.Controls.DataGridLengthConverter$ * 6I    6S   �� *
DienTich+5I      5J      5L      5N      5P      