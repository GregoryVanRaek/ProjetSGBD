   M S B A M L   `   `   `  ����R  NLocationVoiture.Present, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullK*clr-namespace:LocationVoiture.Present.ViewLocationVoiture.Present.View  5   	   U/clr-namespace:LocationVoiture.Present.ViewModel!LocationVoiture.Present.ViewModel  6B   -    'LocationVoiture.Present.View.ClientView   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   <local*clr-namespace:LocationVoiture.Present.View     5   	   E	viewmodel/clr-namespace:LocationVoiture.Present.ViewModel     6B     � Title$  
ClientView��5	   	   $	��800��6   $
��1200��6)    � WindowStartupLocation$ CenterScreen=�66   .��5
      � .+�5   
   �� $��Client Management��6   $��28(�5      $
��Bold"�5      ���#333333$��Center=�5      $	��Top=�5      $��0,10,0,0q�5      �� 5   
   $��	0,40,40,0q�6   .@�5      �� $��Existing Clients��6   .��5      �� .+�5      ��  �� Orientation$ 
Horizontal=�6!   $��Center=�6:   $��10q�6W   .+�5      �� $��Update��6!   $	��150��6T   $��10q�6`   ���#0078D7
�������$��16(�5   I   $��0q�6W    � Cursor$
 Hand��6k   $��7q�6y   ��5   2   �� *UpdateCommand+5   y   �� 5      $��Delete��6!   $	��150��6T   $��10q�6`   ���#D70000
�������$��16(�5   I   $��0q�6W   $
 Hand��6k   $��7q�6y   ��5   2   �� *DeleteCommand+5   y   5      &   System.Windows.Controls.DataGrid  5         AutoGenerateColumns$ False��6?   $��10q�6[   ���#DDD$��1q�5   Z   ��5      �� *	Clients+ �� SelectedItem 5      �� *SelectedClient+   Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5       ,  &System.Windows.Controls.DataGridColumn   Header$ 
First Name��61      Width5  /System.Windows.Controls.DataGridLengthConverter$ * 6c   1  +System.Windows.Controls.DataGridBoundColumn	   Binding	 6E   �� *	Firstname+6c     5!      $ 	Last Name��61   $ * 6a   	 6D   �� *
Lastname+6a     5"      $ Email��61   $ * 6Z   	 6@   �� *Email+6Z     5#      $ Street��61   $ * 6d   	 6A   �� *Address.Street+6d     5$      $ Postal Code��61   $ * 6m   	 6F   �� *Address.PostalCode+6m     5%      $
 City��61   $ * 6`   	 6?   �� *Address.City+6`     5&      $ Country��61   $ * 6f   	 6B   �� *Address.Country+6f     5'      $ Driving License��61   $ * 6m   	 6J   �� *DrivingLicense+6m     5(      $ 
Birth Date��61   $ * 6~   	 6E   �� *	BirthDate+
 �� StringFormat$
 
dd-MM-yyyy��6~   5*      5+      5,      �� 5.      $��Add New Client��6   .��5/      �� $
��Auto=�6   $��Disabled=�6B   $��	0,0,-40,0q�6k   .��50      �� $��20q�6!   $	��614��6-   .+�51      A�  A� Columns$ 2��6&    A� Rows$ 9��62   $��Center=�6;   $
��Auto��6X   .+�52      �� $��First Name:��6$   $
��Bold"�6:   $��Center=�6L   $��5q�6g   �� 53      $	��250��6A   $��5q�6M   ��6&   �� *	Firstname+6M   �� 54      $��
Last Name:��6$   $
��Bold"�69   $��Center=�6K   $��5q�6f   �� 55      $	��250��6@   $��5q�6L   ��6&   �� *
Lastname+6L   �� 56      $��Email:��6$   $
��Bold"�65   $��Center=�6G   $��5q�6b   �� 57      $	��250��6=   $��5q�6I   ��6&   �� *Email+6I   �� 58      $��Street:��6$   $
��Bold"�66   $��Center=�6H   $��5q�6c   �� 59      $	��250��6>   $��5q�6J   ��6&   �� *Street+6J   �� 5:      $��PostalCode:��6$   $
��Bold"�6:   $��Center=�6L   $��5q�6g   �� 5;      $	��250��6B   $��5q�6N   ��6&   �� *
PostalCode+6N   �� 5<      $��City:��6$   $
��Bold"�64   $��Center=�6F   $��5q�6a   �� 5=      $	��250��6<   $��5q�6H   ��6&   �� *City+6H   �� 5>      $��Country:��6$   $
��Bold"�67   $��Center=�6I   $��5q�6d   �� 5?      $	��250��6?   $��5q�6K   ��6&   �� *	Country+6K   �� 5@      $��License:��6$   $
��Bold"�67   $��Center=�6I   $��5q�6d   �� 5A      $	��250��6?   $��5q�6K   ��6&   �� *	License+6K   �� 5B      $��
Birth Date��6$   $
��Bold"�69   $��Center=�6K   $��5q�6f   (  "System.Windows.Controls.DatePicker  5C      -   5        $��BirthDateInput��5C   )   $	��250��6?   $��5q�6K      SelectedDate 6V   �� *	BirthDate+5D      �� 5E      $��Create Client��6!   $	��150��6[   $��20q�6g   ���#0078D7
�������$��16(�5F   C   $��0q�6Q   $
 Hand��6e   $��7q�6s   ��5E   9   �� *CreateCommand+5F   s   5G      5H      5I      5J      5K      5L      