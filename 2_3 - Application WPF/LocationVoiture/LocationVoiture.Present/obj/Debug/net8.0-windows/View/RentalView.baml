   M S B A M L   `   `   `  ����R  NLocationVoiture.Present, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullK*clr-namespace:LocationVoiture.Present.ViewLocationVoiture.Present.View  5   	   U/clr-namespace:LocationVoiture.Present.ViewModel!LocationVoiture.Present.ViewModel  5   	   N JLocationVoiture.dal, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullhGclr-namespace:LocationVoiture.dal.Entities;assembly=LocationVoiture.dalLocationVoiture.dal.Entities 5   	   -    'LocationVoiture.Present.View.RentalView   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   <local*clr-namespace:LocationVoiture.Present.View     5   	   E	viewmodel/clr-namespace:LocationVoiture.Present.ViewModel     5   	   ZmodelsGclr-namespace:LocationVoiture.dal.Entities;assembly=LocationVoiture.dal     5   	     � Title$  
RentalView��5   	   $	��800��6   $
��1200��6)    � 	Resources 5         RentalStatusEnum S� 
MethodName S� 
ObjectType/  )LocationVoiture.dal.Entities.RentalStatus S� MethodParameters M� Type��%m   &	        S� 5   
   $ 	GetValues��5      "  5       5      M� 5      "  6   5      5      .��5      � ���#F5F5F5.+�5   
   �� $��Car Rental Management��6   $��28(�5      $
��Bold"�5      ���#333333$��Center=�5      $	��Top=�5      $��0,10,0,0q�5      �� 5   
   .��5      �� $��0,60,0,0q�6   .@�5      �� $��All Rentals��6   .��5       �� .+�5!      ��  �� Orientation$ 
Horizontal=�6%   $��Center=�6>   $��10q�6[   .+�5"      �� $��Load Rentals��6%   $	��150��6\   $��10q�6h   ���#0078D7
�������$��16(�5#   I   $��0q�6W    � Cursor$
 Hand��6k   $��7q�6y   ��5"   <   �� *ReadCommand+5#   y   �� 5$      $��Update��6%   $	��150��6X   $��10q�6d   ���#0078D7
�������$��16(�5%   I   $��0q�6W   $
 Hand��6k   $��7q�6y   ��5$   6   �� *UpdateCommand+5%   y   �� 5&      $��Delete��6%   $	��150��6X   $��10q�6d   ���#D70000
�������$��16(�5'   I   $��0q�6W   $
 Hand��6k   $��7q�6y   ��5&   6   �� *DeleteCommand+5'   y   5(      &   System.Windows.Controls.DataGrid  5*         AutoGenerateColumns$ False��6C   $��30q�6_   ���#DDD$��1q�5+   Z   $
��1146��6n   ��5*   #   �� *	Rentals+	 �� SelectedItem	 5+      �� *SelectedRental+
   Columns
 5,      0  *System.Windows.Controls.DataGridTextColumn  5-   "   ,  &System.Windows.Controls.DataGridColumn   Header$	 Car��65      Width5  /System.Windows.Controls.DataGridLengthConverter$ * 6^   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6B   �� *	CarInfo+6^     5.   "   $ Client��65   $ * 6h    6E   �� *ClientFullName+6h     5/   "   $ 
Start Date��65   $ * 6y    6I   �� *	StartDate+ �� StringFormat$ d��6y     50   "   $ Duration (days)��65   $ * 6k    6N   �� *
Duration+6k     51   "   $ Amount��65   $	 100 6`    6E   �� *Amount+6`   4  .System.Windows.Controls.DataGridComboBoxColumn  52   "   $ Status��69   $ * 55   1      ItemsSource 53   1   ��  �� Source# [     SelectedItemBinding 54   1   �� *Status+55   1   57      58      59      �� 5;      $��Current Rentals��6   .��5<      �� .+�5=      �� $ 
Horizontal=�6%   $��Center=�6>   $��10q�6[   .+�5>      �� $��Enclose��6%   $	��150��6Z   $��10q�6f   ���#0078D7
�������$��16(�5?   I   $��0q�6W   $
 Hand��6k   $��7q�6y   ��5>   7   �� *EncloseCommand+5?   y   5@        5B      $ False��6J   $��30q�6f   ���#DDD$��1q�5C   Z   $
��1146��6n   ��5B   #   �� *CurrentRentals+	 5C      �� *SelectedRental+
 5D        5E   "   $	 Car��65   $ * 6^    6B   �� *	CarInfo+6^     5F   "   $ Client��65   $ * 6h    6E   �� *ClientFullName+6h     5G   "   $ 
Start Date��65   $ * 6�    6I   �� *	StartDate+$ 
dd-MM-yyyy��6�     5H   "   $ Duration (days)��65   $ * 6k    6N   �� *
Duration+6k     5I   "   $ Amount��65   $	 100 6`    6E   �� *Amount+6`     5J   "   $ Status��69   $ * 5M   1    5K   1   �� # [   5L   1   �� *Status+5M   1   5O      5P      5Q      �� 5S      $��Add New Rental��6   .��5T      �� $
��Auto=�6#   $��Disabled=�6F   .��5U      �� $��20q�6%   $	��677��61   .+�5V      �� $��Available Cars��6$   $
��Bold"�6=   $��5q�6O     5W      $ False��6D   $��5q�6`   ���#DDD$��1q�5X   [   $	��150��5Y   #   $
��Auto=�60   ��5W   '   �� *Cars+	 5X   #   �� *SelectedCar+
 5Z   "     5[   &   $ ID��69   $ 50 6\    6E   �� *Id+6\     5\   &   $ License Plate��69   $ * 6q    6P   �� *LicensePlate+6q     5]   &   $ Color��69   $ * 6b    6H   �� *Color+6b     5^   &   $ Parking Code��69   $ * 6o    6O   �� *ParkingCode+6o     5_   &   $ 
Model Name��69   $ * 6k    6M   �� *	ModelName+6k   5a      �� 5c      $��Available Clients��6$   $
��Bold"�6@   $��5q�6R     5d      $ False��6G   $��5q�6c   ���#DDD$��1q�5e   ^   $	��150��5f   #   $
��Auto=�60   ��5d   '   �� *	Clients+	 5e   #   �� *SelectedClient+
 5g   "     5h   &   $ ID��69   $ 50 6\    6E   �� *Id+6\     5i   &   $ 
First Name��69   $ * 6k    6M   �� *	Firstname+6k     5j   &   $ 	Last Name��69   $ * 6i    6L   �� *
Lastname+6i     5k   &   $ Email��69   $ * 6b    6H   �� *Email+6b     5l   &   $ License��69   $ * 6m    6J   �� *DrivingLicense+6m   5n      �� 5p      $ 
Horizontal=�6)   $��5q�6B   .+�5q   "   �� $��
Start Date��6(   $	��147��6=   $��Center=�6I   (  "System.Windows.Controls.DatePicker  5r   "   $	��200��6-      SelectedDate 69   �� *	StartDate+5s      �� 5u      $ 
Horizontal=�6)   $��5q�6B   .+�5v   "   �� $��Duration (days)��6(   $	��146��6B   $��Center=�6N   �� 5w   "   $	��200��6*   ��66   �� *
Duration+ �� UpdateSourceTrigger$ PropertyChanged=�5x      �� 5z      $ 
Horizontal=�6)   $��5q�6B   .+�5{   "   �� $��Amount��6(   $	��146��69   $��Center=�6E   �� 5|   "   $	��200��6*    �� 
IsReadOnly$
 True��6N   ��66   �� *Amount+6N   5}      �� 5      $ 
Horizontal=�6)   $��5q�6B   .+�5�   "   �� $��Status��6(   $	��100��69   $��Center=�6E   �� 5�   "   $	��200��6+   ��5�   '   �� # [  	 5�   '   �� *Status+5�      �� 5�      $��Create Rental��6%   $	��200��6_   ���#0078D7
�������$��16(�5�   I   $��0q�6W   $
 Hand��5�   !   $��20q�6/   $��7q�6;   ��5�   =   �� *CreateCommand+5�   ;   5�      5�      5�      5�      5�      5�      5�      