   M S B A M L   `   `   `  ����  CivilizationWPF#    CivilizationWPF.ConfigureForm   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   c _Microsoft.Expression.Effects, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35=ee4http://schemas.microsoft.com/expression/2010/effects  5   	   :d2http://schemas.microsoft.com/expression/blend/2008  5   	   Dmc;http://schemas.openxmlformats.org/markup-compatibility/2006  6F   -   5          � Title$  ConfigureForm��5   	   $	��300��6   $	��300��6,    � 
ResizeMode$ NoResize=�6Q    � WindowStyle$
 None=�6g   $��3q�6�   ��5      �� 5   
    �� EndPoint$	 1,1&�6    �� 
StartPoint$	 0,0&�6-   .��5	      �  � Color$ Cornsilk��6    � Offset$ 0Q�6,   � 5
      $ 	BurlyWood��6   $ 1Q�6-   5       � 	Resources 5         	textBrush3  -System.Windows.Media.Effects.DropShadowEffect  CheckedEffect   
BlurRadius	   Color  
NullEffect
   ShadowDepth  RadioButtonStyle1 �� 
TargetType �� Property �� Value �� 
TargetType � 	Resources  Key'  !System.Windows.VisualStateManager   VisualStateGroups%  System.Windows.VisualStateGroup   States   System.Windows.VisualState �� RadiusY �� RadiusX 
� GradientOrigin 
� RadiusY 
� RadiusX J� SnapsToDevicePixels �� Triggers P� Property P� Value��%�  &	        &	 �     &	 �     &	 	    7   �� 5   
   $ 1,0.5&�61   $ 0,0.5&�6B   .��5      � $ Black��6   $ 0Q�6)   � 5      $ SaddleBrown��6   $ 1Q�6/   5        5   
   $ 20Q�62   $	 DarkBlue��6B     5   
   $ 0Q�6/   $
 0Q�6>   $	 Transparent��6O   �� 5   
   " 	�6*   .�5      ��  � �� 5      �� 5      " 	�6&    5      ���� 5      OnMouseEnter1 6)   5      .R�5      �  5        5    "   CommonStates��. 5!   &     
Normal��62     5"   &   	MouseOver��62     5#   &   Pressed��62     5$   &   Disabled��62   5%   #   .+�5'      �� $ 20��6(   $ 20��65   $��0��6B   ��5(   "   
� 5)   &   $ 	0.35,0.15&�6:   $	 0.5Q�6U   $	 0.5Q�6c   .��5*   *   � $ White��67   $ 0Q�6E   � 5+   *   $ #deb887��67   $ 1Q�6G   5,   '   5.      �� 5/      $��Center=�6/   $��Center=�6K   ��. # z 6�   51      52      54      56      �� 57       � ��$ 40��6&   �� 58       � ��$ 40��6'   �� 59       � ��$ 10q�6'   �� 5:       � ��$
 Bold"�64   �� 5;       � ��8   67    5<      P� 5=       �@d�	IsChecked$
 TrueY�6/   . �5>      ��  �@J�Effect# �  6/   5?      P� 5@       �@d�	IsChecked$ FalseY�6/   . �5A      ��  �@J�Effect# �  6/   5B      5E      5F      ��5H      �� 5I   
   $ 1,0.5&�6   $ 0,0.5&�6/    �� Opacity$ 1Q�6B   .��5J      � $ 	LightBlue��6   $ 0Q�6-   � 5K      $ 	#E00064FF��6   $ 1Q�6-   5L      .��5R      �  � RowDefinitions 5S   
   �� 5T      $
��235* �6   �� 5U      $	��26* �6   .+�5W   
   � -   5        $��
WrapPanel1��5W      $��0��6&   .+�5X      	� -   5        $��checkBox_Human��5X   f   #���  6   $��Human��6H   $��15q�6Z   6f   	� 5Y      -   5        $��checkBox_Grass��5Y   c   #���  6   $��Grass��6F   $��15q�6W   6c   	� 5Z      -   5        $��checkBox_Water��5Z   b   #���  6   $��Water��6F   $��15q�6V   6b   	� 5[      -   5        $��checkBox_Desert��5[   c   #���  6   $��Desert��6F   $��15q�6W   6c   	� 5\      -   5        $��checkBox_Forest��5\   c   #���  6   $��Forest��6F   $��15q�6W   6c   	� 5]      -   5        $��checkBox_Mountain��5]   e   #���  6   $��Mountain��6F   $��15q�6Y   6e   	� 5^      -	   5        $��checkBox_Ice��5^   `   #���  6   $	��Ice��6F   $��15q�6T   6`   5_      �� 5`   
   -
   5        $��button_exit��5`   �   $
��Back��6   #��[  6    $��1��6I   $��23��6V   $
��Left=�6b   $��0,3,0,0q�6}   $	��Top=�6�   $��75��6�   ��5a      
� 5b      $ 	0.35,0.15&�6&   $	 0.5Q�6A   $	 0.5Q�6O   .��5c      � $ White��6#   $ 0Q�61   � 5d      $ #deb887��6#   $ 1Q�63   5e      5g      5h      5i      