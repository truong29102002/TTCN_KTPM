   M S B A M L   `   `   `  ����A  =QLCHXE, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:QLCHXE.AdminQLCHXE.Admin  5   	        QLCHXE.Admin.TrangChuAdmin   5   	   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   ,localclr-namespace:QLCHXE.Admin     5   	   _ [MaterialDesignThemes.Wpf, Version=4.9.0.0, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5   	     � Title$  TrangChuAdmin��5	   	    � WindowState$ 	Maximized=�5
       � WindowStyle$
 None=�5   	   .��5      � .+�5   
   �  � ColumnDefinitions 5      �� 5      $	��230 �6#   �� 5      $��* �6#    � RowDefinitions 5      �� 5      $��30 �6    �� 5      $��* �6    .+�5      ]� $��Stretch=�6   $��Stretch=�66   $��0��6R   $��2��6_   ���	#FFDFDFDF.+�5      �� $��10 0 0 0q�6   $��16(�6.   $
��Left=�6<   $
��Left=�6W   OneG Manager6{   �� 5      $��60��6    �� Orientation$ 
Horizontal=�6(   $��Right=�6A   .+�5      �� -   5         ��Name$ hidden��5   i      
HoverStyle#��[  6     � Cursor$
 Hand��6D   $��30��6R   $��30��6]   '  !MaterialDesignThemes.Wpf.PackIcon  6�      Kind$ Minimize=�6�   $��7q�6�   6�   �� 5       -   5        $ close��5    i   #��[  6    $
 Hand��6D   $��30��6R   $��30��6]     6�   $ Close=�6�   $��7q�6�   6�   5!      5$      �� 5&      $��0��6   $��1��6+     MaterialDesignScrollViewer#��[ 68   $��Hidden=�6x   $��Hidden=�6�   .��5)      �  5*      �� 5+      $��1* �6+   �� 5,      $��1 �6+   .+�5.      �� $��0��6!   .+�56      �� ���#2E333A$ Vertical=�57      $
��Left=�58      .+�5<      � $��50��6#   $	��Top=�5=      $��0,30,0,0q�5>      .+�5@   "   �� ���#D61734$��25��5A      	 �� CornerRadius$	 3,0,0,3��5B      $��6��5C      $��Center=�5D      $��Right=�5E      �� 5G   "   $��A D M I N     O N E G��6,   ���#FFFFFF$��Fonts/#Orbitron Medium)�5I      $��19(�5J      $��Center=�5K      $��Center=�5L      5N      o� 5R      $	��900��6#   ���#2E333A$��0,40,0,0q�5T      ��5V   "   �� 5X   &   .R�5Z   *   $� 
 $� Orientation$
 Vertical=�6A   5\   '   .@�5b   "   m� -   5        $
��Home��5b   +     Menu_Template#��[ 5c       m� Icon 5e   &     5g   *   $
 Home=�6B   
�������$��30��6a   $��37��6m   5k   #   m� 5o   "   $��Nhân viên��6+   $
 Hand��6>     Menu_SubMenu_Template#��[ 5p       5r   &     5t   *   $ Human=�6B   
�������$��32��6b   $��30��6n   .@�5z   &   m� -   5        $��All Nhân viên��5z   /   $
 Hand��6a     Item_Template#��[ 5{       5}   *     5   .   $ PeopleGroup=�6F   
�������$��32��6l   $��30��6x   5�   '   m� 5�   &   -   5        $��
Quản kho��5�   /   $
 Hand��6Z   #��[ 5�       5�   *     5�   .   $ NaturePeople=�6F   
�������$��32��6m   $��30��6y   5�   '   5�   #   m� 5�   "   $��Cửa hàng��6+   #��[ 5�       5�   &     5�   *   $ Store=�6B   
�������$��30��6b   $��37��6n   .@�5�   &   m� -   5        $��
QL Xe máy��5�   /   $
 Hand��6`   #��[ 6n    5�   *     5�   .   $ 	Motorbike=�6F   
�������$��32��6j   $��30��6v   5�   '   m� 5�   &   -   5        $��	QL Ô tô��5�   /   $
 Hand��6\   #��[ 6j    5�   *     5�   .   $	 Car=�6F   
�������$��32��6d   $��30��6p   5�   '   m� 5�   &   -   5        $��QL Xe tải��5�   /   $
 Hand��6`   #��[ 6n    5�   *     5�   .   $ 
TruckCheck=�6F   
�������$��32��6k   $��30��6w   5�   '   5�   #   m� 5�   "   $	��Kho��6+   #��[ 5�       5�   &     5�   *   $ 	StoreEdit=�6B   
�������$��30��6f   $��32��6r   .@�5�   &   m� -	   5        $��Kho xe máy��5�   /   #��[ 6C    5�   *     5�   .   $ 	Motorbike=�6F   
�������$��32��6j   $��30��6v   5�   '   m� 5�   &   -
   5        $��Kho xe tải��5�   /   #��[ 6C    5�   *     5�   .   $ Truck=�6F   
�������$��32��6f   $��30��6r   5�   '   m� 5�   &   -   5        $��
Kho xe oto��5�   /   #��[ 6C    5�   *     5�   .   $	 Car=�6F   
�������$��32��6d   $��30��6p   5�   '   5�   #   m� 5�   "   -   5        $��Nhà cung cấp��5�   +   #��[ 5�       5�   &     5�   *   $ Offer=�6B   
�������$��30��6b   $��31��6n   5�   #   m� 5�   "   -   5        $��Thống kê��5�   +   $
 Hand��6V   #��[ 5�       5�   &     5�   *   $ ReportAreaspline=�6B   
�������$��31��6m   $��32��6y   5�   #   m� 5�   "   -   5        $��Đổi mật khẩu��5�   +   $
 Hand��6Y   #��[ 5�       5�   &     5  *   $ PasswordSecure=�6B   
�������$��27��6k   $��31��6w   5  #   m� 5	  "   -   5        $
��Exit��5	  +   #��[ 5
      5  &     5  *   $ Logout=�6B   
�������$��36��6c   $��Center=�6p   $��29��6�   5  #   5     5     5     5     5      � 5      -   5          �Name$ 	mainFrame��5        � NavigationUIVisibility$ Hidden=�6'   $��1��6G   $��1��6W   5"     5(     5)     5,     