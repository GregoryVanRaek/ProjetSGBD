   M S B A M L   `   `   `  ����R  NLocationVoiture.Present, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullK*clr-namespace:LocationVoiture.Present.ViewLocationVoiture.Present.View  5   	   U/clr-namespace:LocationVoiture.Present.ViewModel!LocationVoiture.Present.ViewModel  5   	   /    )LocationVoiture.Present.View.CategoryView   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   <local*clr-namespace:LocationVoiture.Present.View     5   	   E	viewmodel/clr-namespace:LocationVoiture.Present.ViewModel     5   	     � Title$  Category��5
   	   $	��800��6   $	��800��6'    � WindowStartupLocation$ CenterScreen=�63   .��5      � .+�5   
   �� $��Category Management��6   $��28(�5      $
��Bold"�5      ���#333333$��Center=�5      $	��Top=�5      $��0,10,0,0q�5      �� 5   
   $��	0,40,40,0q�6   .@�5      �� $��Existing Categories��6   .��5      �� .+�5      ��  �� Orientation$ 
Horizontal=�6!   $��Center=�6:   $��10q�6W   .+�5      �� $��Update��6!   $	��150��6T   $��10q�6`   ���#0078D7
�������$��16(�5   I   $��0q�6W    � Cursor$
 Hand��6k   $��7q�6y   ��5   2   �� *UpdateCommand+5   y   �� 5      $��Delete��6!   $	��150��6T   $��10q�6`   ���#D70000
�������$��16(�5   I   $��0q�6W   $
 Hand��6k   $��7q�6y   ��5   2   �� *DeleteCommand+5   y   5      &   System.Windows.Controls.DataGrid  5         AutoGenerateColumns$ False��6B   $��10q�6^   ���#DDD$��1q�5   \   ��5      �� *
Categories+ �� SelectedItem 5      �� *SelectedCategory+   Columns 5       0  *System.Windows.Controls.DataGridTextColumn  5!      ,  &System.Windows.Controls.DataGridColumn   Header$ Category Name��61      Width5  /System.Windows.Controls.DataGridLengthConverter$ * 6a   1  +System.Windows.Controls.DataGridBoundColumn	   Binding	 6H   �� *Name+6a     5"      $ 
Daily Rate��61   $ * 6c   	 6E   �� *	DailyRate+6c   5$      5%      5&      �� 5(      $��Add New Category��6   .��5)      �� $
��Auto=�6   $��Disabled=�6B   $��	0,0,-40,0q�6k   .��5*      �� $��20q�6!   $	��614��6-   .+�5+      A� 
 A� Columns$
 2��6&    A� Rows$ 2��62   $��Center=�6;   $
��Auto��6X   .+�5,      �� $��Category Name:��6$   $
��Bold"�6=   $��Center=�6O   $��5q�6j   �� 5-      $	��250��6<   $��5q�6H   ��6&   �� *Name+6H   �� 5.      $��Daily Rate:��6$   $
��Bold"�6:   $��Center=�6L   $��5q�6g   �� 5/      $	��250��6A   $��5q�6M   ��6&   �� *	DailyRate+6M   50      �� 51      $��Create Category��6!   $	��150��6]   $��20q�6i   ���#0078D7
�������$��16(�52   I   $��0q�6W   $
 Hand��6k   $��7q�6y   ��51   ;   �� *CreateCommand+52   y   53      54      55      56      57      58      