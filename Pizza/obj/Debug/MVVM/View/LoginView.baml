   M S B A M L   `   `   `  ����@  <Pizza, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null1clr-namespace:Pizza.MVVM.ViewPizza.MVVM.View  5   	   ;"clr-namespace:Pizza.MVVM.ViewModelPizza.MVVM.ViewModel  5   	   E'clr-namespace:Pizza.MVVM.CustomControlsPizza.MVVM.CustomControls  5   	       Pizza.MVVM.View.LoginView   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   /localclr-namespace:Pizza.MVVM.View     5   	   8	viewModel"clr-namespace:Pizza.MVVM.ViewModel     5   	   Bcustomcontrols'clr-namespace:Pizza.MVVM.CustomControls     5   	   -   5          � Title$  	LoginView��5
   	   $	��650��6   $	��650��6(    � WindowStyle$
 None=�5   	    � 
ResizeMode$ NoResize=�5   	    � WindowStartupLocation$ CenterScreen=�5   	   
������  � AllowsTransparency .  � DataContext 5      )   #Pizza.MVVM.ViewModel.LoginViewModel  5   
    � 	Resources 5         BooleanToVisibility��%   &	        �� 5   
   6'   5      y�5      �� 5   
    �� Path$ IsViewVisible�6    �� Mode$ TwoWay=�6'   	 �� 	Converter#	 [  65   .��5      �� 
 �� CornerRadius$
 50��6   $��0q�6   ��5   
   �� 5        �� ImageSource$' !../../Images/Login/Background.jpg��6    �� Opacity$ 1Q�6I   ._�5#   
   �� $
 50��6   $��0q�6#   ��5%      �� 5&      $	 0.5Q�6&   .��5'      �  � Color$ Black��6#   63   5(      ._�5*      �  � RowDefinitions 5,      �� 5-      $��30 �6    �� 5.      .+�51      �  � ColumnDefinitions 52      �� 53      �� 54      $��25 �6'   �� 55      $��25 �6'   �� 56      $��25 �6'   .+�59      �� -   5         ��Name$ btnMinimize��5:      $��0q�5;       � Cursor$
 Hand��5<      $��1��5=      ��5?      �� 5@      -6'   ��5B      �� 5C       �� 
TargetType" ��6    .�5D      ��  �� Property � �� �� Value
 �����6O    �� Triggers 5E      P� 5F   "    P� Property �@J�IsMouseOver P� Value . . �5G   &   ��  � ��
 ��İ�6\   5H   #   5J      ��5M      �� 5N       �� 
TargetType" ��6*   .R�5O      �� $��18��6%   $��18��60   $
 9��5P   %   #��z��5Q   %   ._�5R   "   �� $��Center=�63   $��Center=�5S   3   6R   5T      5U      5X      �� 5Y      -   5        $ btnClose��5Z      
�������$��0q�5\      $
 Hand��5]      $��2��5^      ��5`      �� 5a      x6'   ��5c      �� 5d      " ��6    .�5e      ��  � ��
 �����6O    5f      P� 5g   "    �@J�IsMouseOver . . �5h   &   ��  � ��
 ��İ�6\   5i   #   5k      ��5n      �� 5o      " ��6*   .R�5p      �� $��18��6%   $��18��60   $
 9��5q   %   #��z��5r   %   ._�5s   "   �� $��Center=�63   $��Center=�5t   3   6R   5u      5v      5x      5y      �� 5{      $	��250��6   $��2��5|       �� Orientation$ Vertical=�5}      $��0,35,0,0q�5~      .+�5      �� $��Log in to your account��6    
�������$��30(�5�      $��Medium"�5�      $��Roboto)�5�      $��Center=�5�      $
��Wrap=�5�       �� TextAlignment$ Center=�5�      $��0,70,0,0q�5�      60   �� 5�      $��username��6   
�������$��12(�5�      $��Medium"�5�      $��Roboto)�5�      $��0,35,0,0q�5�      60   �� 5�      -   5         ��Name$ usernameTextBox��5�      $��13(�5�      $��Medium"�5�      $��Roboto)�5�      
������� �� 
CaretBrush
 �   �
�������$��0,0,0,2q�5�      $��28��5�      $��Center=�5�      $��0,5,0,0q�5�      $��30,0,0,0q�5�      ��5�      �� *
Username+.��5�   -   ��5�      �� 5�      $' !../../Images/Login/Icons/user.png��6%    j� Stretch$ Uniform=�5�   %    j� 
AlignmentX$
 Left=�5�   %    j� ViewboxUnits$ RelativeToBoundingBox=�67     j� ViewportUnits$  RelativeToBoundingBox=�6\   .��5�   *   5�      �� 5�      $��password��6   
�������$��12(�5�      $��Medium"�5�      $��Roboto)�5�      $��0,10,0,0q�5�      60   3   -Pizza.MVVM.CustomControls.BindablePasswordBox  5�      $��28��5�   5   $��0,5,0,0q�5�   5   !   Password! 5�   9   �� *
Password+$ TwoWay=�" �� UpdateSourceTrigger$" PropertyChanged=�5�      �� 5�      ���#D7596D$��12(�5�      $��Medium"�5�      $��
Montserrat)�5�      $��0,10,0,0q�5�      $
��Wrap=�5�      ��5�       �� *ErrorMessage+5�      �� 5�      -   5        $ btnLogin��5�      $��0q�5�      $
 Hand��5�      $��1��5�      $��0,15,0,0q�5�      ��5�      �� *LoginCommand+��5�      �� 5�      Log In60   ��5�      �� 5�      " ��6$   .�5�   "   ��  � ��
 �����6S    5�   "   P� 5�   &    �@J�IsMouseOver . . �5�   *   ��  � ��
 ��İ�6`   5�   '   5�      ��5�      �� 5�      " ��6.   .R�5�   "   �� $	��150��6)   $��25��65   $

 12.5��5�   %   #��z��5�   %   ._�5�   &   �� $��Center=�67   $��Center=�5�   3   6R   5�   #   5�      5�      �� 5�      $ 
Horizontal=�6!   $��Center=�5�   !   $��0,15,0,0q�5�   !   .+�5�      �� $��Forgot password?��6    
�������$��12(�5�      $��Medium"�5�      $��Roboto)�5�      62   �� 5�      $��Reset��6    
�������$��12(�5�      $��Medium"�5�      $��Roboto)�5�      $
 Hand��5�      $��10,0,0,0q�5�      60   5�      5�      5�      5�      5�      5�      