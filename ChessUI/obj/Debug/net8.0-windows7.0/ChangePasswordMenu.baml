   M S B A M L   `   `   `  ����  ChessUI     ChessUI.ChangePasswordMenu   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	     � Title$  Change Password��5   	   $	��400��5   	   $	��400��6   ���#260000 � WindowStartupLocation$ CenterScreen=�5
   	   $��Segoe UI Light)�6.    � Icon$ /Assets/icon.ico��6J   .��5      � ��5   
   �� 5       �� ImageSource$ /Assets/Background.png��6    j� Stretch$ UniformToFill=�6>   .+�5   
   �� $��Center=�6   $	��Top=�62   $	��250��6J   $��0,85,0,0q�6V   $	��289��6h   .+�5      �� $��	Username:��6   $��0,0,0,5q�6)   
�������$��Segoe UI Light)�6M   �� 5      -   5         ��Name$ UsernameTextBox��5      $��25��6/   $��0,0,0,10q�5      
�������$��Center=�5      ���	#FF707070$��Segoe UI Light)�5   f   ��5      Z�  �� SelectionBrush 5   .   Z�  �� SelectionTextBrush 6H   Z� 6f   �� 5      $��Old Password:��6   $��0,0,0,5q�6-   
�������$��Segoe UI Light)�6Q   G� 5      -   5         G�Name$ OldPasswordBox��5      $��25��62   $��0,0,0,10q�5      
����������	#FF707070$��Segoe UI Light)�5    j   ��5      Z� 	 G� SelectionTextBrush	 5    2   Z� 
 G� SelectionBrush
 6P   Z� 6j   �� 5"      $��New Password:��6   $��0,0,0,5q�6-   
�������6>   G� 5#      -   5        $ NewPasswordBox��5#      $��25��62   $��0,0,0,10q�5$      
����������	#FF707070��5%      Z� 	 5'   2   Z� 
 6P   Z� �� 5)      $��Confirm New Password:��6   $��0,0,0,5q�65   
�������6F   G� 5*      -   5        $ ConfirmNewPasswordBox��5*      $��25��69   $��0,0,0,20q�5+      
����������	#FF707070��5,      Z� 5.      �� 50       �� Orientation$ 
Horizontal=�6   $��Center=�62   $��0,10,0,0q�6O   .+�51      �� -   5        $
��Back��51      $��25��6(   $	��100��52      $��0,0,40,0q�53      
����������	#FF707070��54      Z� 57      �� 58      -   5        $��Change Password��58      $��25��67   $	��100��59      
����������	#FF707070��5:      Z� 5=      5>      5?      5@      5A      