   M S B A M L   `   `   `  ����A  =QLCHXE, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null-clr-namespace:QLCHXE.SharedQLCHXE.Shared  5   	       QLCHXE.Shared.QLXeTai   5      W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   -localclr-namespace:QLCHXE.Shared     5   	   $	��900��5   	   $
��1350��6     N� FontSize$  14(�6#   .��5	      �  � RowDefinitions 5
   
   �� 5      $	��200 �6   �� 5      $��* �6    � ColumnDefinitions 5   
   �� 5      $��* �6   �� 5      $��* �6   �� 5      $	��200 �6   .+�5   
   �� $��1��6   $��2��6"   $��2��62   .+�5      �� -   5         ��Name$ btnThem��5   )   $��0q�6   $��Center=�6:   $��20q�6U   .��6w   Nhập hàng từ kho6�   �� 5      -   5        $ btnSua��5   )   $��0q�6   $��Center=�69   $��20q�6T   .��6u   Cập nhật6   �� 5      -   5        $ btnXoa��5   )   $��0q�6   $��20q�69   $��Center=�6E   .��6u   Chuyển vào kho6�   �� 5      -   5        $ 
btnNhapMoi��5   )   $��0q�6   $��Center=�6=   $��20q�6X   .��6}   	Refresh6�   �� 5      -   5        $ btnSell��5   )   $��0q�6   $��Center=�6:   $��20q�6U   .��6w   Bán6|   5      �� 5   
   $��30q�6   $
��Bold"�6    $��15(�62   Nhập thông tin xe tải6W   �� 5   
   $��
50,50,50,0q�6   $��0��6)   $��3��66    �� Orientation$ 
Horizontal=�6J   .+�5       �� $ Vertical=�6   $	��275��60   $��	20,20,0,0q�6<   .+�5"      �  5#      �� 5$      $	��100 �6+   �� 5%      $��* �6+   .+�5(      �� $ Vertical=�6!   $��1��68   .+�5*      �� $��
0,10,10,10q�6%   $��Right=�69   .+�5,      �� $��	Tên xe: ��6(   5.      �� 51      $��
0,15,10,10q�6%   $��Right=�69   .+�53      �� $��Hãng sản xuất: ��6(   54      �� 56      $��
0,15,10,10q�6%   .+�57      �� $��Năm sản xuất: ��6(   58      5:      �� 5;      $��1��6!   .+�5<      �� $��0q�6%   .+�5=      �� -   5         ��Name$ txtTenXe��5=   &   5>      �� 5@      $��
0,17,10,10q�6%   .+�5A      �� -   5         ��Name$ 	cbxHangsx��5A   (   $
��Left=�6;   $	��150��6V   $
��Left=�6b   5B      �� 5D      $��	0,5,10,10q�6%   .+�5E      �� -   5        $ cbxNamsx��5E   N   $	��100��6'   $
��Left=�63   6N   5F      5G      5H      5J      �� 5K      $ Vertical=�6   $	��251��60   $��	20,20,0,0q�6<   .+�5N      �  5O      �� 5P      $	��100 �6+   �� 5Q      $��* �6+   .+�5T      �� $��0��6!   .+�5U      �� $
��0 17q�6%   $��Right=�63   .+�5W      �� $��Giá: ��6(   5X      �� 5Z      $��
10,13,0,10q�6%   $��Right=�69   .+�5[      �� $��Đơn vị: ��6(   5\      5`      �� 5b      $��1��6!   .+�5c      �� $��10q�6%   .+�5d      �� -	   5        $ 	txtGiaban��5d   &   5e      �� 5g      $��10q�6%   .+�5h      �� -
   5        $ txtDonVi��5h   &   5i      5l      5n      5p      �� 5r      $ Vertical=�6   $	��280��60   $��	20,20,0,0q�6<   .+�5u      �  5v      �� 5w      $	��100 �6+   �� 5x      $��* �6+   .+�5{      �� $��0��6!   .+�5|      �� $��0 15 0 0q�6%   $��Right=�67   .+�5~      �� $��Trọng tải: ��6(   $��Center=�6;   5      �� 5�      $��10,13,10,10q�6%   �� 5�      $��10,11,10,10q�6%   $��Right=�6:   .+�5�      �� $��Màu: ��6(   $��Center=�65   5�      5�      �� 5�      $��1��6!   .+�5�      �� $��	0 0 10 30q�6%   .+�5�      �� -   5        $ txtTrongTai��5�   &   5�      �� 5�      .+�5�      �� -   5        $ cbxMau��5�   3   $	��120��6'   $
��Left=�6C   5�      5�      5�      5�      �� 5�      $ Vertical=�6   $	��278��60   $��	20,20,0,0q�6<   .+�5�      �� $ 
Horizontal=�6   .+�5�      �� $��	Mô tả:��6    $��0,0,10,0q�6.   �� 5�      -   5        $ txtMota��5�   @   $	��0.5q�6   $	��153��64   $��98��6Q    �� TextWrapping$
 Wrap=�6]    �� AcceptsReturn$
 True��6q   6�   5�      5�      5�      �� 5�   
   $��1��6   $��2��6"   $ Vertical=�66   .+�5�      �� $��Thông tin xe tải��6   $��16(�60   $
��Bold"�6>   $��50,10,0,-100q�6P   &   System.Windows.Controls.DataGrid  5�      -   5        	  Name$	 dtgXeTai��5�   G      MaterialDesignDataGrid#��[  6   
   
IsReadOnly$
 False��6Y      CanUserDeleteRows$
 True��6l   $��50q�6�      AutoGenerateColumns$ False��6�      CanUserAddRows$ False��6�   $	��500��6�      Columns 5�      0  *System.Windows.Controls.DataGridTextColumn  5�      ,  &System.Windows.Controls.DataGridColumn   Header$ Tên xe��6)   1  +System.Windows.Controls.DataGridBoundColumn   Binding 69   �� *TenXe+  5�      $ Hãng��6)    67   �� *
HangXeDT+  5�      $ Năm sản xuất��6)    6?   �� *Namsx+  5�      $ 	Giá bán��6)    6:   �� *GiaBan+  5�      $
 Màu��6)    66   �� *MauDT+  5�      $ Trọng tải��6)    6<   �� *
Trongtai+  5�      $ Ngày nhập��6)    6<   �� *
NgayNhap+  5�      $ Số lượng��6)    6;   �� *SLCon+  5�      $ 
Đơn vị��6)    69   �� *DonVi+  5�      $ Mô tả��6)    68   �� *MoTa+  5�      $ Tình trạng��6)      Width5  /System.Windows.Controls.DataGridLengthConverter$ * 6=    6G   �� *	TinhTrang+4  .System.Windows.Controls.DataGridCheckBoxColumn  5�      $	  X ��6-    6:   �� *	isChecked+5�      5�      5�      5�      