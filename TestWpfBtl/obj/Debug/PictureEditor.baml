   M S B A M L   `   `   `  ����E  ATestWpfBtl, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'clr-namespace:TestWpfBtl
TestWpfBtl  5   	       TestWpfBtl.PictureEditor   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   *localclr-namespace:TestWpfBtl     5   	     � Title$  PictureEditor��5   	   $	��630��6   $	��800��6,   .��5	      �  � ColumnDefinitions 5
   
   �� 5      6!   �� 5      $
��0.5* �6   6.   �� 5      6!    � RowDefinitions 5   
   �� 5      $
��0.3* �6   6,   �� 5      6   .+�5   
   �� -   5         ��Name$ 	btnSource��5      $��Source��5      $��0��6"   $��0��62   6Y   �� 5   
   -   5        $ btnSave��5      $
��Save��5      $��2��6    $��0��60   6V   �� 5   
   -   5         ��Name$ imageSource��5      $��0��6%   $��1��65   6E   �� 5   
   -   5        $ 	imageFake��5      $��2��6#   $��1��63   6C   � 5   
   $��1��6   $��2��6   .+�5      �� 
���  ��$��2q�6'   ._�5      �� -   5         ��Name$ toolbox��5      $��Option��6,   .��5      �� .+�5      �� -   5        $ OpFilter��5   #   $��Filter��65   .��5      �� .+�5    "   	� -   5         	�Name$ rbGray��5    .   $
��Gray��6>   	� 5!   "   -   5        $ rbSepia��5!   .   $��Sepia��6?   	� 5"   "   -	   5        $ rbHue��5"   .   $��HueModifier��6=   	� 5#   "   -
   5        $ rbChanel��5#   .   $��Chanel��6@   	� 5$   "   -   5        $ rbInvert��5$   .   $��Invert��6@   �� 5%   "   $ ��---Linear color filters---��6(   	� 5&   "   -   5        $ rb1RGB��5&   .   $��
RGB linear��6>   	� 5'   "   -   5        $ rb1Bri��5'   .   $��
Brightness��6>   	� 5(   "   -   5        $ rb1Con��5(   .   $��Contrast��6>   	� 5)   "   -   5        $ rb1Satu��5)   .   $��
Saturation��6?   �� 5*   "   $#��---Nonlinear color filters---��6(   	� 5+   "   -   5        $ rb2Color��5+   .   $��Color remapping��6@   	� 5,   "   -   5        $ rb2Gamma��5,   .   $��Gamma correction��6@   �� 5-   "   $��---Pixel filtering---��6(   	� 5.   "   -   5        $ rb3Color��5.   .   $��Color Filtering��6@   	� 5/   "   -   5        $ 	rb3Chanel��5/   .   $��Channel Filtering��6A   	� 50   "   -   5        $ rb3HSL��50   .   $��HSL Filtering��6>   	� 51   "   -   5        $ rb3YCB��51   .   $��YCbCr Filtering��6>   �� 52   "   $��---Convolution filters---��6(   	� 53   "   -   5        $ rb4Convolution��53   .   $��convolution��6F   	� 54   "   -   5        $ rb4Blur��54   .   $
��blur��6?   	� 55   "   -   5        $ 	rb4Shapen��55   .   $��Sharpen��6A   	� 56   "   -   5        $ rb4Edges��56   .   $��Edges��6@   	� 57   "   -   5        $ rbNone��57   .   $
��None��6>   58      59      �� 5:      -   5        $ btnFake��5:   !   $
��Fake��62   6Y   5;      5<      5=      5>      5@      5A      