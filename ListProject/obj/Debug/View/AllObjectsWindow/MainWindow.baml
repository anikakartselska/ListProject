   M S B A M L   `   `   `  ����F  BListProject, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullS.clr-namespace:ListProject.ViewModel.Converters ListProject.ViewModel.Converters  5   	   2    ,ListProject.View.AllObjectsWindow.MainWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   E
converters.clr-namespace:ListProject.ViewModel.Converters     5   	     � 
ResizeMode$  	CanResize=�5   	    � Title$ 
MainWindow��5	   	   $	��450��6   $	��800��6)   $	��200��5
   	   $	��200��6    � 	Resources 5      C   =ListProject.ViewModel.Converters.VisibilityToBooleanConverter    VisibilityToBooleanConverter��%   &	          5   
   62   5      .��5      �  � RowDefinitions 5   
   �� 5      $��2* �6   �� 5      $��4* �6   �� 5      $��1* �6   .+�5   
   � $��1��6   .+�5      �� $��Center=�5      $��Center=�68   ��5      �� *
MyDataGrid+5   8   �� 5      $��Center=�6L   $��Center=�5      $��20(�6:   ��5      �� *MyDataGrid.NoDataTextBlock+5   :   5      � 5   
   $��0��6    � ColumnDefinitions 5      �� 5      $��5* �6#   �� 5      $��1* �6#   �� 5      $��5* �6#   .+�5       � $��2��6   .+�5!      �� $
��Auto=�6   $
��Auto=�6B   .��5"      �� $��Select Columns��6   $��Center=�67   $
��Left=�5#      
���   �.��5$      �� ��6'   �� *MyDataGrid.Columns+��5%      �� 5&   "   .R�5'   &   �� ��6/   �� *Header+ d� 	IsChecked 5(   /   �� *MyVisibility+ �� Mode$ TwoWay=� �� 	Converter# [  5)   #   5+      ��5,      �� 5-       �� 
TargetType" ��6$   	 �� Triggers	 5.   "   �� 5/   &   
 �� Binding
 62   �� *MyDataGrid.Columns+ �� Value 6Y   Z� .a�50   *   ��  �� Property � y� �� Value$ 	Collapsed=�6G   51   '   53      55      56      57      �� 58      $��0��6   $
��Auto=�6+   $
��Auto=�6N   .��59      � .+�5:      �� $��Select Object Type��6   $��Center=�6;   $��Right=�5;      
���   �.��5<      �� $��77��5=   H   ��5<   "   �� *Types+ �� SelectedItem 5=   "   �� *SelectedType+��5>      �� 5?   "   .R�5@   &   �� ��60   �� *Name+5A   #   5C      5D      5E      5F      5G      � 5H   
   $��2��6    5I      �� 5J      �� 5K      �� 5L      .+�5N      �� $��2��6   $��Clear��6%   $��Center=�5O      $��70��60   $��Center=�6;   ��5N   5   �� *ClearListAndSelection+5O   ;   �� 5P      $��0��6   $��Go Back��6%   $��Center=�5Q      $��70��60   $��Center=�6;   ��5P   7   �� *GoBackToStartScreen+5Q   ;   5R      5S      5T      