   M S B A M L   `   `   `  ����  POS    POS.MainWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   V RMaterialDesignThemes.Wpf, Version=2.5.0.1205, Culture=neutral, PublicKeyToken=nullFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5   	     � Title$  Aula 4 -TextBox ��5   	   $	��600��6"   $	��800��6/    � WindowStartupLocation$ CenterScreen=�6;    � 
ResizeMode$ NoResize=�6`   $��Regular"�5	   	   $��13(�5
   	   &   System.Windows.Media.TextOptions   TextFormattingMode$ Ideal=�5   	      TextRenderingMode$
 Auto=�5   	   .��5      � $��0,0,0,-1q�6    � RowDefinitions 5   
   �� 5      $
��Auto �6   6,   �� 5      $
��Auto �6   6,   �� 5      $
��Auto �6   6,    � ColumnDefinitions 5   
   �� 5      $
��Auto �6   6.   .+�5   
   �� $��Login��6   $
��Left=�6    $��	341,0,0,0q�6;   $	��Top=�6N    J� RenderTransformOrigin$ 1.487,1&�6f   $��35(�6�   
����� �6�   �� 5   
   $��Username��6   $
��Left=�6#   $��62,174,0,-24q�6>   $��2��6T   $	��Top=�6a   $��20(�6y   
����� �6�   �� 5   
   $��Password��6   $
��Left=�6#   $��62,223,0,-44q�6>   $��2��6T   $	��Top=�6a   $ 0.145,2.297&�6y   $��20(�6�   
����� �6�   �� 5   
   -   5         ��Name$ username��5      $��242,176,226,0q�6$   $ 0.5,0.5&�6;   $��2��6[   
����� �$	��Top=�6z   
������ $��25��6�   '  !MaterialDesignThemes.Wpf.PackIcon  5#   
   �  �ControlzEx.PackIconBase`1[[MaterialDesignThemes.Wpf.PackIconKind, MaterialDesignThemes.Wpf, Version=2.5.0.1205, Culture=neutral, PublicKeyToken=null]]	   Kind$	 AccountCircle=�6"   $
��Left=�67   $��96��6R   $��
322,29,0,0q�6^   $��2��6r   $	��Top=�6   $	��158��6�   
����� �$ 0.388,0.532&�6�   �� 5$   
   -   5        $��Login��5$      $
��Left=�6!   $��242,287,0,-105q�6<   $��2��6T   $	��Top=�6a   $	��332��6y   $��32��6�   
 �� 	IsDefault
 . 
����� ����#fff���#fff6�   G� 5%   
   -   5         G�Name$ password��5%      $
��Left=�6(   $��242,225,0,-39q�6C   $��2��6Z   $	��Top=�6g   $	��332��6   $��25��6�   
������ 
����� �6�   �� 5&   
   $
��Left=�6   $��26��6/   $��0,371,0,-168q�6;   $��2��6Q   $	��Top=�6^   $	��800��6v   
����� �6�   �� 5'   
   $.��(Copyright reserved @ Sandile Cedric 2018��6   $
��Left=�6C   $��212,417,0,-184q�6^   $��2��6v   $	��Top=�6�   $ 0.925,0.426&�6�   $��20(�6�   
����� �6�   �� 5(   
   $
��Left=�6   $��26��6/   $��0,57,0,0q�6;   $	��Top=�6M   $	��790��6e   $��3��6q   
����� �6�   5,      5-      