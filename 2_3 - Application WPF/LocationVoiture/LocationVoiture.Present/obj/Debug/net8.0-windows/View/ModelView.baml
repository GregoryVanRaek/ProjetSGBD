   M S B A M L   `   `   `  ����R  NLocationVoiture.Present, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullK*clr-namespace:LocationVoiture.Present.ViewLocationVoiture.Present.View  5   	   U/clr-namespace:LocationVoiture.Present.ViewModel!LocationVoiture.Present.ViewModel  5   
   N JLocationVoiture.dal, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullhGclr-namespace:LocationVoiture.dal.Entities;assembly=LocationVoiture.dalLocationVoiture.dal.Entities 5   	   ,    &LocationVoiture.Present.View.ModelView   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   <local*clr-namespace:LocationVoiture.Present.View     5   	   E	viewmodel/clr-namespace:LocationVoiture.Present.ViewModel     5   
   ZmodelsGclr-namespace:LocationVoiture.dal.Entities;assembly=LocationVoiture.dal     5   	     � Title$  	ModelView��5   	   $	��800��6   $
��1200��6(    � WindowStartupLocation$ CenterScreen=�65   .��5      � ���#F5F5F5.+�5   
   �� $��Model Management��6   $��28(�5      $
��Bold"�5      ���#333333$��Center=�5      $	��Top=�5      $��0,10,0,0q�5      �� 5   
   .��5      �� $��0,60,0,0q�6   .@�5      �� $��
All Models��6   .��5      �� .+�5      ��  �� Orientation$ 
Horizontal=�6%   $��Center=�6>   $��10q�6[   .+�5      �� $��Delete��6%   $	��150��6X   $��10q�6d   ���#D70000
�������$��16(�5   I   $��0q�6W    � Cursor$
 Hand��6k   $��7q�6y   ��5   6   �� *DeleteCommand+5   y   5      &   System.Windows.Controls.DataGrid  5         AutoGenerateColumns$ False��6B   $��30q�6^   ���#DDD$��1q�5   ]   $
��1043��6q   ��5   #   �� *Models+ �� SelectedItem 5   #   �� *SelectedModel+   Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5    "   ,  &System.Windows.Controls.DataGridColumn   Header$ 
Model Name��65      Width5  /System.Windows.Controls.DataGridLengthConverter$ * 6b   1  +System.Windows.Controls.DataGridBoundColumn	   Binding	 6I   �� *Name+6b     5!   "   $ Brand��65   $ * 6^   	 6D   �� *Brand+6^     5"   "   $ Seat Number��65   $	 100 6i   	 6J   �� *
SeatNumber+6i     5#   "   $ Category��65   $ * 6f   	 6G   �� *
CategoryId+6f   5%      5&      5'      �� 5)      $��Add New Model��6   .��5*      �� $
��Auto=�6#   $��Disabled=�6F   .��5+      �� $��20q�6%   $	��677��61   .+�5,      �� $ 
Horizontal=�6)   $��5q�6B   .+�5-   "   �� $��
Model Name��6(   $	��146��6=   $��Center=�6I   �� 5.   "   $	��200��6*   ��66   �� *Name+
 �� UpdateSourceTrigger$
 PropertyChanged=�5/      �� 51      $ 
Horizontal=�6)   $��5q�6B   .+�52   "   �� $��Brand��6(   $	��146��68   $��Center=�6D   �� 53   "   $	��200��6*   ��66   �� *Brand+$
 PropertyChanged=�54      �� 56      $ 
Horizontal=�6)   $��5q�6B   .+�57   "   �� $��Seat Number��6(   $	��146��6>   $��Center=�6J   �� 58   "   $	��200��6*   ��66   �� *
SeatNumber+$
 PropertyChanged=�59      �� 5;      $ 
Horizontal=�6)   $��5q�6B   .+�5<   "   �� $��Category��6(   $	��146��6;   $��Center=�6G   �� 5=   "   $	��200��6+    �� DisplayMemberPath$
 Name��5?       �� SelectedValuePath$ Id��5@      ��5>      �� *
Categories+ �� SelectedValue 5A      �� *
CategoryId+5B      �� 5D      $��Create Model��6%   $	��200��6^   ���#0078D7
�������$��16(�5E   I   $��0q�6W   $
 Hand��5F   !   $��20q�6/   $��7q�6;   ��5D   <   �� *CreateCommand+5F   ;   5G      5H      5I      5J      5K      5L      5M      