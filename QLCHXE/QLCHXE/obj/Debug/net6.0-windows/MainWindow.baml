   M S B A M L   `   `   `  ����A  =QLCHXE, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:QLCHXEQLCHXE  5   	       QLCHXE.MainWindow   5   	   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   &localclr-namespace:QLCHXE     5   	   _ [MaterialDesignThemes.Wpf, Version=4.9.0.0, Culture=neutral, PublicKeyToken=df2a72020bd7962aEmaterilDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5   	   $	��300��5	   	   $	��400��6     � 
ResizeMode$  CanMinimize=�6"    � WindowStartupLocation$ CenterScreen=�5
   	   .��5      � $��10q�6    � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $��* �6    � ColumnDefinitions 5   
   �� 5      $	��150 �6   �� 5      $��1* �6   .+�5   
   �� $��0��6   $��0��6"    �� Orientation$ 
Horizontal=�63   .+�5      '  !MaterialDesignThemes.Wpf.PackIcon     Kind$ Person=�6%   $��50(�63   $��48��6A   $��44��6M   �� 5      $��Login��6   $��30(�6$   5      �� 5   
   $��1��6   .+�5      �� $��_Tài khoản:��6   $��Right=�6O   $��0,10,0,0q�6k    �� Target 6*   ��  �� ElementName$ txtTk��6k   �� 5      $��_Mật khẩu:��6   $��Right=�6N   $��0,10,0,0q�6j    6)   �� $ txtMK��6j   5      �� 5   
   $��1��6   $��1��6"   $ Vertical=�62   .+�5      �� .+�5       �� -   5         ��Name$ txtTk��5       $��0,5,0,0q�6)   $��0,5,0,5q�6:   $	��160��6L   $
��Left=�6X   G� 5!      -   5        	 G�Name$	 txtMK��5!      $��0,5,0,0q�6-   $��0,5,0,5q�6>   $	��160��6P   $
��Left=�6\   �� 5#      -   5        
 ��Name$
 btnDN��5#       � Cursor$
 Hand��6,   $	��110��6:   $��	0,20,10,0q�6F   .��5$      �� $ 
Horizontal=�6!   .+�5%      �� $��0q�6    ���	#DDF6F6F6$��0 0 5 0q�6C   .��6T   Login6[     5&      $ Login=�61   $��Stretch=�6>   5'      5(      5+      �� 5-      $ 
Horizontal=�6   $��Right=�62   .+�5.      �� -   5         ��Name$ lbFeedB��5.      $
 Hand��6*   ���	#FF0E3DD5$��Italic$�6O   $��0q�6b   $��14(�6n   $��	0,50,10,0q�6|   .��5/      ��    
labelStyle#���   6    $��Times New Roman)�6E   FeedBack to Admin6x   50      51      52       � 	Resources 54   
    �� Property �� Value��%T   &	        �� 55      .�56      ��  � ��$ 	Underline{�6>   6R   58      59      5:      5;      