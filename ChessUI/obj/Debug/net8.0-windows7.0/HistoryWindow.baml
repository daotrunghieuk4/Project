   M S B A M L   `   `   `  ����  ChessUI    ChessUI.HistoryWindow   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	     � Title$  Game History��5   	   $	��410��6   $	��620��6+   ���#260000 � Icon$ Assets/icon.ico��5   	    � WindowStartupLocation$ CenterScreen=�5   	   .��5	      � ��5
   
   �� 5       �� ImageSource$ /Assets/Background.png��6    j� Stretch$ UniformToFill=�6>   .+�5   
   �� $��5q�6   .+�5      �� $��Game History��6   $��24(�5      $
��Bold"�5      $��0,0,0,20q�5      
�������$��Segoe UI Light)�5      $��Center=�5      &   System.Windows.Controls.DataGrid  5      -   5          Name$ HistoryDataGrid��5         AutoGenerateColumns$ False��5         
IsReadOnly$
 True��5      
�������
���   �   GridLinesVisibility$	 All=�5      ���	#FF707070$��Segoe UI Light)�5      $	��250��5      	 � 	Resources	 5       =  7System.Windows.Controls.Primitives.DataGridColumnHeader
 �� 
TargetType �� Property �� Value��%V   '        �� 5"      "
  6   .�5#      ��  � ��$ Center=�6G   5$      5%         Columns 5'      0  *System.Windows.Controls.DataGridTextColumn  5)      ,  &System.Windows.Controls.DataGridColumn   Header$ White Player��6)      Width5  /System.Windows.Controls.DataGridLengthConverter$
 1.5* 6_   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6?   �� *WhitePlayer+   ElementStyle 5*      �� 5+      "
 ��6$   .�5,   "   ��  �@��TextAlignment$ Center=�6B   5-      5/        52      $ Black Player��6)   $
 1.5* 6_    6?   �� *BlackPlayer+ 53      �� 54      "
 ��6$   .�55   "   ��  �@��TextAlignment$ Center=�6B   56      58        5;      $ Result��6)   $ 2* 6T    69   �� *Result+ 5<      �� 5=      "
 ��6$   .�5>   "   ��  �@��TextAlignment$ Center=�6B   5?      5A        5D      $ Date Played��6)   $ 2* 6]    6>   �� *
DatePlayed+ 5E      �� 5F      "
 ��6$   .�5G   "   ��  �@��TextAlignment$ Center=�6B   5H      5J      5L      �� 5O      -   5        $
��Back��5O      $��30��6$   $	��100��5P      $��0,20,0,0q�5Q      
����������	#FF707070$��Segoe UI Light)�5V      $��Center=�5W      ��5S      Z� 5W      5X      5Y      5Z      