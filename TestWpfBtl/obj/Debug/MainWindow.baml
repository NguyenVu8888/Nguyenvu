   M S B A M L   `   `   `  ����E  ATestWpfBtl, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:VideoRecoderVideoRecoder  5   	       TestWpfBtl.MainWindow   5   	   $localclr-namespace:VideoRecoder  5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   _ [MaterialDesignThemes.Wpf, Version=4.8.0.0, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5   	     � Title$  
MainWindow��5	   	   $	��600��6   $	��800��6)    � DataContext 5      &   VideoRecoder.MainWindowViewModel  5   
   6&   .��5      �  � RowDefinitions 5   
   �� 5      $��* �6   6)   �� 5      $
��auto �6   6,   �� 5      $
��auto �6   6,   �� 5      $
��auto �6   6,   �� 5      $
��auto �6   6,   .+�5   
   �  5      �� 5      $
��Auto �6    60   �� 5      $��* �6    6-   .+�5      �� $��1��5       ��5!      �� *Image+�� 5"      $��Video player��6   6-   5#      �� 5&   
   $��1��6   $��source selection��6   6:   � 5'   
   $��2��6    5(      �� 5)      $��30 �6    6.   �� 5*      $��30 �6    6.   �� 5+      $��30 �6    6.   �� 5,      $
��Auto �6    60   .+�5.      �� $��0��6   $
��Left=�5/       �� Orientation$ 
Horizontal=�50      .+�51      �� $��0 5 0 0q�52      $��Center=�6+   $��Desktop��53       d� 	IsChecked 54      �� *IsDesktopSource+57      �� 58      $��1��6   $
��Left=�59      $ 
Horizontal=�5:      .+�5;      �� $��0 5 0 0q�6   $��Center=�5<      $��WebCam��5=       5>      �� *IsWebCamSource+�� 5@      $	��100��5A      $
��Left=�6#   $��Center=�5B       �� DisplayMemberPath$
 Name��5C      ��5D      �� *VideoDevices+ �� SelectedItem 5E      �� *CurrentDevice+5G      5I      �� 5J      $��2��6   $
��Left=�5K      $ 
Horizontal=�5L      .+�5M      �� $��0 0 5 0q�5N      $��Center=�5O      $��	Ip Camera��5P       5Q      �� *IsIpCameraSource+�� 5R      $��Center=�6   ��5S      �� *IpCameraUrl+5U      �� 5X      $��3��6   $��Center=�6&   $ 
Horizontal=�5Y      .+�5Z      �� $��0��6   $	��110��6&    ,  (MaterialDesignRaisedSecondaryLightButton#��[  5\       � ToolTip$= 7Resource name: MaterialDesignRaisedSecondaryLightButton��5]      $��30��5^      $��5q�6%   $��Right=�5_      $��Start Video��5a      ���	#FF16DCF0{�5[      �� *DataContext.ControlsEnabled+ �� RelativeSource �� *FindAncestor+	 �� AncestorType$	 WindowL���5`      �� *StartSourceCommand+5a   /   �� 5b      $��1��6   $	��110��6&   $��30��62   $��5q�6>   $
��Left=�5c      #��[  5e      $= 7Resource name: MaterialDesignRaisedSecondaryLightButton��5f      $��
Stop Video��5h      ���	#FF18E1F4{�5d      �� *DataContext.ControlsEnabled+ �� *FindAncestor+$	 WindowL���5g      �� *StopSourceCommand+5h   .   5j      5n      �� 5p   
   $��3��6   $��Video Command��6   � 5q   
   $��4��6   
 � ColumnDefinitions
 5r      �� 5s      $��* �6#   6/   .+�5v      �� $��0��6   $ 
Horizontal=�5w      $��Center=�5x      .+�5z      �� $	��120��5{      $��30��5|      $��4q�5}      #��[  5      $= 7Resource name: MaterialDesignRaisedSecondaryLightButton��5�      $��	Start rec��5�      ���	#FF18E6FA{�5~      �� *DataContext.ControlsEnabled+ �� *FindAncestor+$	 WindowL���5�      �� *StartRecordingCommand+5�   )   �� 5�      $	��120��5�      $��30��5�      $��4q�5�      #��[  5�      $= 7Resource name: MaterialDesignRaisedSecondaryLightButton��5�      $��Stop rec��5�      ���	#FF1AE1F4{�5�      �� *DataContext.ControlsEnabled+ �� *FindAncestor+$	 WindowL���5�      �� *StopRecordingCommand+5�   (   �� 5�      $	��110��5�      $��30��5�      $��4q�5�      #��[  5�      $= 7Resource name: MaterialDesignRaisedSecondaryLightButton��5�      $��Save Snapshort��5�      ���	#FF18E3F7{�5�      �� *DataContext.ControlsEnabled+ �� *FindAncestor+$	 WindowL���5�      �� *SaveSnapCommand+5�   .   5�      5�      5�      5�      