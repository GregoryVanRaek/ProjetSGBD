   M S B A M L   `   `   `  ����R  NLocationVoiture.Present, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullK*clr-namespace:LocationVoiture.Present.ViewLocationVoiture.Present.View  5   	   U/clr-namespace:LocationVoiture.Present.ViewModel!LocationVoiture.Present.ViewModel  5   	   N JLocationVoiture.dal, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullhGclr-namespace:LocationVoiture.dal.Entities;assembly=LocationVoiture.dalLocationVoiture.dal.Entities 5   	   0    *LocationVoiture.Present.View.CarDetailView   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   <local*clr-namespace:LocationVoiture.Present.View     5   	   E	viewmodel/clr-namespace:LocationVoiture.Present.ViewModel     5   	   ZmodelsGclr-namespace:LocationVoiture.dal.Entities;assembly=LocationVoiture.dal     5   	     � Title$  CarView��5   	   $	��800��6   $
��1200��6&   .��5      � ���#F5F5F5.+�5   
   �� $��Car Management��6   $��28(�5      $
��Bold"�5      ���#333333$��Center=�5      $	��Top=�5      $��0,10,0,0q�5      �� 5   
   .��5      �� $��0,60,0,0q�6   .@�5      �� $��Car List��6   .��5      �  � RowDefinitions 5      �� 5      $
��Auto �6,   �� 5      $��* �6,   �� 5      $
��Auto �6,   .+�5      &   System.Windows.Controls.DataGrid  $��1��6#      AutoGenerateColumns$ False��5!   #   $��30q�5"   #   ���#DDD$��1q�5$   #   $
��1146��5%   #   ��5   #   �� *Cars+ �� SelectedItem 5    #   �� *SelectedCar+   Columns 5&      0  *System.Windows.Controls.DataGridTextColumn  5'   "   ,  &System.Windows.Controls.DataGridColumn   Header$ License Plate��65      Width5  /System.Windows.Controls.DataGridLengthConverter$ * 6m   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6L   �� *LicensePlate+6m     5(   "   $ Color��65   $ * 6^    6D   �� *Color+6^     5)   "   $ Parking Code��65   $ * 6k    6K   �� *ParkingCode+6k     5*   "   $ 
Model Name��65   $ * 6g    6I   �� *	ModelName+6g   5,      �� 5.      $��2��6%    �� Orientation$ 
Horizontal=�62   $��Center=�6K   $��10q�6h   	 � 	Resources	 5/      
 �� 
TargetType �� Property �� Value��%  '��       �� 50   "   "
 ��6(   .�51   &   ��  � ��$	 150��6>   �� 52   &    � ��$ 10q�6?   �� 53   &    � ��$ 16(�6A   �� 54   &    � ��$ 0q�6H   �� 55   &    �@�Cursor$
 Hand��6?   �� 56   &    � ��$ 7q�6@   57   #   58      .+�59      �� $��Update��6%   ���#0078D7
���������5<   %   �� *UpdateCarCommand+�� 5=      $��Delete��6%   ���#D70000
���������5@   %   �� *DeleteCarCommand+5A      5B      5C      �� 5E      $��
Create Car��6   .��5F      �� $��20q�6!   .+�5G      �� $ 
Horizontal=�6%   $��5q�6>   .+�5H      �� $��License Plate��6$   $	��150��6<   $��Center=�6H   �� 5I      $��5q�6Q   $	��300��6\   ��6&   �� *LicensePlate+ �� Mode$ TwoWay=�6\   5J      �� 5L      $ 
Horizontal=�6%   $��5q�6>   .+�5M      �� $��Color��6$   $	��150��64   $��Center=�6@   �� 5N      $��5q�6J   $	��300��6U   ��6&   �� *Color+$ TwoWay=�6U   5O      �� 5Q      $ 
Horizontal=�6%   $��5q�6>   .+�5R      �� $��Model��6$   $	��150��64   $��Center=�6@   �� 5S       �� DisplayMemberPath$
 Name��5T   '    �� SelectedValuePath$ Id��5U   '   $��5q�5W   '   $	��300��5X   '   ��5S   '   �� *Models+ �� SelectedValue 5V   '   �� *	ModelId+$ TwoWay=�5X   '   5Y      �� 5[      $ 
Horizontal=�6%   $��5q�6>   .+�5\      �� $��Parking��6$   $	��150��66   $��Center=�6B   �� 5]      $
 Code��5^   '   $ Id��5_   '   $��5q�5a   '   $	��300��5b   '   ��5]   '   �� *
Parkings+ 5`   '   �� *	ParkingId+$ TwoWay=�5b   '   5c      �� 5e      $��
Create Car��6!   $	��200��5g   !   ���#0078D7
�������$��16(�5j   !   $��0q�5k   !    � Cursor$
 Hand��5l   !   $��20q�5m   !   $��7q�5n   !   ��5f   !   �� *CreateCarCommand+5n   !   5o      5p      5q      5r      5s      5t      