   M S B A M L   `   `   `  ����H  DTDUserControl, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null-clr-namespace:TDUserControlTDUserControl  5   	       TDUserControl.Pager   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   -localclr-namespace:TDUserControl     5   	     �� CornerRadius$  6��5   	   $��0q�6   
������� � 	Resources 5         PageButtonStyle �� 
TargetType �� Property �� Value �� 
TargetType �� Padding �� Triggers P� Property	 P� Value
 �� 
TargetName  CurrentPageButtonStyle��%  &	        &	 �    �� 5	   
   " ��6(   .�5
      ��  �@�OverridesDefaultStyle . 6G   �� 5       � ��$ Center=�6C   �� 5       � ��$  ��67   �� 5       � ��$ 40��6:   �� 5       � ��$	 3,0q�68   �� 5       � �� 5      �� 5      " ��6&   .R�5      �� $��border��6!   $  4��6/   $��30��6@   $��0q�6L   ._�5      �� $��	textBlock��6(   $��Center=�69   $��Center=�6T   ���#000$��14(�5      $	 6,0q�6(   �� 5      5      5       5      P� 5       �@J�IsMouseOver	 . . �5   "   �� $
 border��6)    � �� �#0689f56Z   �� 5   "   $
 	textBlock��6)    � �� �#FFF6`   5      5      5       5!      �� 5"   
   " ��6/   .�5#      ��  �@�OverridesDefaultStyle . 6G   �� 5$       � ��$ Center=�6C   �� 5%       � ��$  ��67   �� 5&       � ��$ 40��6:   �� 5'       � ��$	 3,0q�68   �� 5(       � �� 5)      �� 5*      " ��6&   .R�5+      �� $��border��6!   ���#0689f5$  4��6D   $��30��6U   $��0q�6a   ._�5,      �� $��	textBlock��6(   $��Center=�69   $��Center=�6T   ���#FFF$��14(�5-      $	 6,0q�6(   �� 5.      5/      50      51      53      54      55      ._�56      ��  �� Orientation$ 
Horizontal=�6   $	��6,3q�6*   .+�57   
   �� -   5         ��Name$ btnFirstPage��57   *   $��首页��6@   #��[  58      �� 59   
   -   5        $ btnPrevPage��59   *   $��
<上一页��6?   #��[  5:      �� 5;   
   -   5         ��Name$ pageNumbers��5;      $ 
Horizontal=�6*   �� 5<   
   -   5        $ btnNextPage��5<   *   $��
下一页>��6?   #��[  5=      �� 5>   
   -   5        $ btnLastPage��5>   *   $��尾页��6?   #��[  6L   �� 5?   
   -   5        $ lbTotal��5?      #��[  6$   5@      5A      