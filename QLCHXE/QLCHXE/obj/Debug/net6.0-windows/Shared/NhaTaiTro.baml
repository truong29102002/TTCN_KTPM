   M S B A M L   `   `   `  ����A  =QLCHXE, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null-clr-namespace:QLCHXE.SharedQLCHXE.Shared  5          QLCHXE.Shared.NhaTaiTro   5      W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      -localclr-namespace:QLCHXE.Shared     5      _ [MaterialDesignThemes.Wpf, Version=4.9.0.0, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5      .��5      �   � RowDefinitions  5   
   �� 5      $��50 �6   �� 5      $��1* �6   .+�5   
   ��  �� Orientation$ 
Horizontal=�6   $��0��6.   $��Center=�6;   $��Center=�6V   .+�5      �� $��20(�6   $
��Bold"�6"   .��64   Thông tin nhà cung cấp6L   5      �� 5   
   $��1��6   .+�5      �� $ 
Horizontal=�6   $��20q�62   .+�5      �� $
��Left=�6   $
��Wrap=�67   $��Tên nhà cung cấp: ��6K   $	��Top=�6e   �� 5      -   5         ��Name$ txtTen��5   I   $
��Left=�6    �� TextWrapping$
 Wrap=�65   $	��Top=�6Y   $	��400��6q   $��	10 -5 0 0q�6}   5      �� 5      $ 
Horizontal=�6   $��20q�62   .+�5      �� $
��Left=�6   $
��Wrap=�67   $��Số điện thoại: ��6K   $	��Top=�6b   �� 5      -   5        $ txtSDT��5   J   $
��Left=�6   $
 Wrap=�66   $	��Top=�6Z   $	��120��6r   $��	10 -5 0 0q�6~   5       �� 5"      $ 
Horizontal=�6   $��20q�62   .+�5#      �� $
��Left=�6   $
��Wrap=�67   $��Địa chỉ: ��6K   $	��Top=�6\   �� 5$      -   5        $ 	txtDiachi��5$   I   $
��Left=�6   $
 Wrap=�65   $	��Top=�6\   $	��400��6t   $��	10 -5 0 0q�6�   5%      &   System.Windows.Controls.DataGrid  5'      -   5          Name$ dtgNTT��5'   G      MaterialDesignDataGrid#��[  6      CanUserAddRows$ False��6W      AutoGenerateColumns$ False��6n   $��20q�6�      CanUserDeleteRows$
 True��6�      
IsReadOnly$
 True��6�   $	��200��6�   $��Center=�6�   	   Columns	 5(      0  *System.Windows.Controls.DataGridTextColumn  5)      ,  &System.Windows.Controls.DataGridColumn
   Header$
 Tên nhà cung cấp��6)   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6C   �� *TenNhaTaiTro+  5*      $
 Số điện thoại��6)    6@   �� *sdt+  5+      $
 Địa chỉ��6)      Width5  /System.Windows.Controls.DataGridLengthConverter$ * 6:    6D   �� *diachi+5-      �� 5.      $��10q�6   $ 
Horizontal=�6%   .+�5/      �� $��10q�6   .+�51      �� -   5         ��Name$ btnThem��51   _    � Cursor$
 Hand��6   $
��Left=�6+   $	��Top=�6G   .��52      �� $ 
Horizontal=�6%   .+�53      '  !MaterialDesignThemes.Wpf.PackIcon     Kind$	 Add=�66   $��0 5 0 0q�6A   �� 54      
�������.��67   Add6<   55      56      57      �� 59      $��10q�6   .+�5;      �� -   5        $ btnSua��5;   _   $
 Hand��6   $
��Left=�6+   $	��Top=�6G   .��5<      �� $ 
Horizontal=�6%   .+�5=        $ Update=�66   $��0 3 0 0q�6D   �� 5>      
�������$��1 1 1 0q�67   .��6I   Update6Q   5?      5@      5A      �� 5C      $��10q�6   .+�5E      �� -   5        $ btnXoa��5E   ^   $
 Hand��6   $
��Left=�6+   $	��Top=�6F   .��5F      �� $ 
Horizontal=�6%   .+�5G        $ Delete=�66   $��0 3 0 0q�6D   �� 5H      
�������$��1 1 1 0q�67   .��6I   Delete6Q   5I      5J      5K      5N      5P      5S      5T      