   M S B A M L   `   `   `  ����  ChessUI    ChessUI.RegisterMenu   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	     � Title$  Register Menu��5   	   $	��400��5   	   $	��400��6   ���#260000 � Icon$ Assets/icon.ico��5
   	    � WindowStartupLocation$ CenterScreen=�5   	   $��Segoe UI Light)�6.   .��5      � ��5   
   �� 5       �� ImageSource$ /Assets/Background.png��6    j� Stretch$ UniformToFill=�6>   .+�5   
   �� $��Center=�6   $	��Top=�62   $	��250��6J   $��	0,143,0,0q�6V   $	��220��6i   .+�5      �� $��	Username:��6   $��0,0,0,5q�5      
�������$��Segoe UI Light)�5   +   �� 5      -   5         ��Name$ UsernameTextBox��5      $��25��6/   $��0,0,0,10q�5      
�������$��Center=�5       �� SelectionBrush �	#FF707070 �� SelectionTextBrush
 ��������	#FF707070$��Segoe UI Light)�6�   ��5      Z� 5   �   �� 5      $��	Password:��6   $��0,0,0,5q�5      
�������$��Segoe UI Light)�5   +   G� 5      -   5         G�Name$ PasswordBox��5      $��25��6/   $��0,0,0,10q�5       $��Center=�5"      ���	#FF707070	 G� SelectionBrush	 �	#FF707070
 G� SelectionTextBrush

 �����
���������5!      Z� 5"   �   �� 5$      $��Confirm Password:��6   $��0,0,0,5q�5%      
�������$��Segoe UI Light)�5&   +   G� 5'      -   5        $ ConfirmPasswordBox��5'      $��25��66   $��0,0,0,20q�5(      
�������$��Center=�5+      ���	#FF707070	 �	#FF707070

 �����$��Segoe UI Light)�6�   ��5)      Z� 5+   �   �� 5-       �� Orientation$ 
Horizontal=�6   $��Center=�62   .+�5.      �� -   5        $
��Back��5.      $��25��6(   $	��100��50      $��0,0,40,0q�51      
����������	#FF707070$��Segoe UI Light)�53   D   ��52      Z� 53   D   �� 54      -   5        $��Register��54      $��25��6,   $	��100��56      
����������	#FF707070$��Segoe UI Light)�58   D   ��57      Z� 58   D   59      5:      5;      5<      