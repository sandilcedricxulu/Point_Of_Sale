   M S B A M L   `   `   `  ����>  :POS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:POSPOS  5          POS.SalePage   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      #localclr-namespace:POS     5      V RMaterialDesignThemes.Wpf, Version=2.5.0.1205, Culture=neutral, PublicKeyToken=nullFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5        N� Title$  SalePage��5
      $	��500��6   .��5      �  � RowDefinitions 5   
   �� 5      $
��107* �6   �� 5      $
��118* �6   .+�5   
   ]� ��.  $	��800��6*   $��2��66   .+�5      � $	��Top=�6   
����� �$��65��6:    � ColumnDefinitions 5      �� 5      $
��121* �6'   �� 5      $	��39* �6'   .+�5      �� $��Cashier��6   $
��Left=�6*   $��	364,5,0,0q�6E   $	��Top=�6X    J� RenderTransformOrigin$ -0.865,1.022&�6p   $��30(�5      ���#fff6'   '  !MaterialDesignThemes.Wpf.PackIcon  5      �  �ControlzEx.PackIconBase`1[[MaterialDesignThemes.Wpf.PackIconKind, MaterialDesignThemes.Wpf, Version=2.5.0.1205, Culture=neutral, PublicKeyToken=null]]   Kind$ Trolley=�6*   $��45��69   $��62��6E   $��	79,10,0,0q�6P   ���#fff6c     5      $ AccountCircle=�6*   $��45��6?   $��57��6K   $��	22,10,0,0q�6V   ���#fff6j   �� 5      -   5        $��Logout��5      $
��Left=�6*   $��	88,10,0,0q�6E   $	��Top=�6X   $��86��6p   $ 	0.067,0.6&�6{   $��40��6�   ���#fff���#fff
����� �$��1��6�   6�   �� 5      -   5        $
��Back��5      $
��Left=�6(   $��
602,10,0,0q�6C   $	��Top=�6W   $��86��6o   $ 	0.067,0.6&�6z   $��40��6�   ���#fff���#fff
����� �$��2��6�   5      � 5      $
��Left=�6   ���#fff$	��398��6;   .+�5      &   System.Windows.Controls.DataGrid  -   5        $��invoiceGrid��5         AutoGenerateColumns . $
��Left=�6I   $	��435��6d   $	��Top=�6q   $	��366��6�   ���#fff
����� �
������    Columns 5       5$      5%      � 5&      $��Right=�6   ���#fff$	��403��6<   .+�5'      �� $��Bar Code��6   $
��Left=�6+   $��	10,10,0,0q�6F   $	��Top=�6Y   $��12(�6q   �� 5(      -   5         ��Name$ BarCode��5(      $
��Left=�6+   $��26��6F   $��	10,41,0,0q�6R    �� TextWrapping$
 Wrap=�6e   $�� ��6y   $	��Top=�6�   $	��221��6�   6�   �� 5)      -   5        	 ��Name$	 btnScan��5)      $
��Scan��6*   $��10,74,285,0q�69   $	��Top=�6N   $��27��6f   
����� ����#fff6�   �� 5+      $��Tendered Amount��6   $��12(�62   $
��Left=�6@   $��
10,116,0,0q�6[   $	��Top=�6o   �� 5,      -   5        $ tenderedAmount��5,      $
��Left=�63   $��26��6N   $��
10,146,0,0q�6Z   $
 Wrap=�6n   $�� ��6�   $	��Top=�6�   $	��221��6�   6�   �� 5-      -   5        $	 Purchase��5-      $��Purchase/Get Change��6,   $��10,177,172,0q�6J   $	��Top=�6`   $��27��6x   
����� ����#fff$ 0.331,2.718&�6�   6�   �� 5/      $��12(�6   $��Change:��6&   $
��Left=�68   $��
15,224,0,0q�6S   $	��Top=�6g   �� 50      $��Total Amount (With VAT):��6   $��12(�6;   $
��Left=�6I   $��
15,261,0,0q�6d   $	��Top=�6x   �� 53      -   5        
 ��Name$
 changeLabel��53      $��13(�6-   $
��R...��6;   $
��Left=�6J   $��206,224,0,0q�6e   $	��Top=�6z   �� 54      -	   5        $
 
totalLabel��54      $��13(�6,   $
��R...��6:   $
��Left=�6I   $��206,261,0,0q�6d   $	��Top=�6y   �� 55      -
   5        $	 btnDone��55      $
��Done��6*   $��123,74,172,0q�69   $	��Top=�6O   $��27��6g   
����� ����#fff6�   �� 56      
����� �$
��Left=�6.   $��32��6I   $��	0,341,0,0q�6U   $	��Top=�6h   $	��403��6�   �� 57      -   5        $	 btnClear��57      $��Next Customer��6,   $��10,373,172,0q�6D   $	��Top=�6Z   $��27��6r   
����� ����#fff$ 0.331,2.718&�6�   6�   59      5:      5;      5<      