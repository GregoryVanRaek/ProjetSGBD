   M S B A M L   `   `   `  ����W  SLocationVoiture.Presentation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullU/clr-namespace:LocationVoiture.Presentation.View!LocationVoiture.Presentation.View  5      _4clr-namespace:LocationVoiture.Presentation.ViewModel&LocationVoiture.Presentation.ViewModel  5      N JLocationVoiture.dal, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullhGclr-namespace:LocationVoiture.dal.Entities;assembly=LocationVoiture.dalLocationVoiture.dal.Entities 5      2    ,LocationVoiture.Presentation.View.RentalView   5      W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      Alocal/clr-namespace:LocationVoiture.Presentation.View     5      J	viewmodel4clr-namespace:LocationVoiture.Presentation.ViewModel     5      ZmodelsGclr-namespace:LocationVoiture.dal.Entities;assembly=LocationVoiture.dal     5        � 	Resources  5         RentalStatusEnum S� 
MethodName S� 
ObjectType/  )LocationVoiture.dal.Entities.RentalStatus S� MethodParameters M� Type��%m   &	        S� 5   
   $ 	GetValues��5      "  5       5      M� 5      "  6   5      5      .��5      � ���#F5F5F5.+�5   
   �� .��5      �� .@�5      �� $��All Rentals��6     TabItemStyle#��[ 6/   .��5      �� $	��800��6!   .+�5      ��  �� Orientation$ 
Horizontal=�6%   $��Center=�6>   $	��Top=�6[   .+�5      �� $��Load Rentals��6%   $	��150��6\   $��10q�6h     ButtonStyle#��[ 6t   ��6<   �� *ReadCommand+6t   �� 5      $��Update��6%   $	��150��6X   $��10q�6d   #��[ 6p   ��66   �� *UpdateCommand+6p   �� 5      $��Delete��6%   $	��150��6X   $��10q�6d   #��[ 6p   ��66   �� *DeleteCommand+6p   5      &   System.Windows.Controls.DataGrid  5       -   5           AutoGenerateColumns$ False��5    C   $��30q�6_   ���#DDD$��1q�5!   Z   $
��1146��6�   ��5    #   �� *	Rentals+ �� SelectedItem 5!      �� *SelectedRental+   Columns 5"      0  *System.Windows.Controls.DataGridTextColumn  5#   "   ,  &System.Windows.Controls.DataGridColumn	   Header$		 Car��65   
   Width5  /System.Windows.Controls.DataGridLengthConverter$
 * 6^   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6B   �� *	CarInfo+6^     5$   "   $	 Client��65   $
 * 6h    6E   �� *ClientFullName+6h     5%   "   $	 
Start Date��65   $
 * 6y    6I   �� *	StartDate+ �� StringFormat$ d��6y     5&   "   $	 Duration (days)��65   $
 * 6k    6N   �� *
Duration+6k     5'   "   $	 Amount��65   $	
 100 6`    6E   �� *Amount+6`   4  .System.Windows.Controls.DataGridComboBoxColumn  5(   "   $	 Status��69   $
 * 5+   3      ItemsSource 5)   3   ��  �� Source# [     SelectedItemBinding 5*   3   �� *Status+5+   3   5-      5.      5/      �� 51      $��Current rentals��6   #��[ 63   .��52      �� $	��800��6!   .+�53      �� $ 
Horizontal=�6%   $��Center=�6>   $	��Top=�6[   .+�54      �� $��Enclose��6%   $	��150��6Z   $��10q�6f   #��[ 6r   ��67   �� *EncloseCommand+6r   55        57      -   5        $ False��57   J   $��30q�6f   ���#DDD$��1q�58   J   $
��1146��6�   ��57   #   �� *CurrentRentals+ 58      �� *SelectedRental+ 59        5:   "   $		 Car��65   $
 * 6^    6B   �� *	CarInfo+6^     5;   "   $	 Client��65   $
 * 6h    6E   �� *ClientFullName+6h     5<   "   $	 
Start Date��65   $
 * 6�    6I   �� *	StartDate+$ 
dd-MM-yyyy��6�     5=   "   $	 Duration (days)��65   $
 * 6k    6N   �� *
Duration+6k     5>   "   $	 Amount��65   $	
 100 6`    6E   �� *Amount+6`     5?   "   $	 Status��69   $
 * 5B   #    5@   #   �� # [   5A   #   �� *Status+5B   #   5D      5E      5F      �� 5H      $��Add New Rental��6   #��[ 62   .��5I      �� $
��Auto=�6#   $��Disabled=�6F   .��5J      �� $��10q�6%   $	��677��61   .+�5K      �� $��Available Cars��6$   $
��Bold"�6=   $��5q�6O     5L      $ False��6D   $��5q�6`   ���#DDD$��1q�5M   W   $	��150��5N      $
��Auto=�6,   ��5L   '   �� *Cars+ 5M      �� *SelectedCar+ 5O   "     5P   &   $	 ID��69   $
 50 6\    6E   �� *Id+6\     5Q   &   $	 License Plate��69   $
 * 6q    6P   �� *LicensePlate+6q     5R   &   $	 Color��69   $
 * 6b    6H   �� *Color+6b     5S   &   $	 Parking Code��69   $
 * 6o    6O   �� *ParkingCode+6o     5T   &   $	 
Model Name��69   $
 * 6k    6M   �� *	ModelName+6k   5V      �� 5X      $��Available Clients��6$   $
��Bold"�6@   $��5q�6R     5Y      $ False��6G   $��5q�6c   ���#DDD$��1q�5Z   Z   $	��150��5[      $
��Auto=�6,   ��5Y   '   �� *	Clients+ 5Z      �� *SelectedClient+ 5\   "     5]   &   $	 ID��69   $
 50 6\    6E   �� *Id+6\     5^   &   $	 
First Name��69   $
 * 6k    6M   �� *	Firstname+6k     5_   &   $	 	Last Name��69   $
 * 6i    6L   �� *
Lastname+6i     5`   &   $	 Email��69   $
 * 6b    6H   �� *Email+6b     5a   &   $	 License��69   $
 * 6m    6J   �� *DrivingLicense+6m   5c      �� 5e      $ 
Horizontal=�6)   $��5q�6B   .+�5f   "   �� $��
Start Date��6(   $	��147��6=   $��Center=�6I   (  "System.Windows.Controls.DatePicker  5g   "   $	��200��6-      SelectedDate 69   �� *	StartDate+5h      �� 5j      $ 
Horizontal=�6)   $��5q�6B   .+�5k   "   �� $��Duration (days)��6(   $	��146��6B   $��Center=�6N   �� 5l   "   $	��200��6*   ��66   �� *
Duration+ �� UpdateSourceTrigger$ PropertyChanged=�5m      �� 5o      $ 
Horizontal=�6)   $��5q�6B   .+�5p   "   �� $��Amount��6(   $	��146��69   $��Center=�6E   �� 5q   "   $	��200��6*    �� 
IsReadOnly$
 True��6N   ��66   �� *Amount+6N   5r      �� 5t      $ 
Horizontal=�6)   $��5q�6B   .+�5u   "   �� $��Status��6(   $	��100��69   $��Center=�6E   �� 5v   "   $	��200��6+   ��5w      �� # [   5x      �� *Status+5y      �� 5z      $��Create Rental��6%   $	��200��5{      #��[ 6!   ��5z   =   �� *CreateCommand+5{   !   5|      5}      5~      5      5�      5�      5�      