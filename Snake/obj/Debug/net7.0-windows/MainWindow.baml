   M S B A M L   `   `   `  ����@  <Snake, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:SnakeSnake  5   	       Snake.MainWindow   5   	   W SPresentationCore, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   %localclr-namespace:Snake     5   	   -   5          � Title$  Snake��5   	   $	��900��5	   	   $
��1200��5
   	   $	��500��5   	   $	��300��5   	    � WindowStartupLocation$ CenterScreen=�5   	    � Icon$ Assets/icon.ico��5   	      BackgroundColour#��[  5   	     
TextColour#��[ 5   	     MainFont#��[ 5   	    � 	Resources 5       �� 
TargetType �� Property �� Value��%�   '�       7   7  7  �� 5   
   " �6   .�5      ��  � ��8   6+   �� 5       � ��8  6+   �� 5       � ��8  6+   5      5      .��5      (� .��5      �  � RowDefinitions 5      �� 5       $
��Auto �6   �� 5!      $��* �6   .+�5$      �� -   5         ��Name$ 	ScoreText��5%      $��Center=�5&      $��Center=�5'      $��SCORE 0��5(      $��22(�5)      $��10q�5*      �� 5,      -   5        	 ��Name$	 
GridBorder��5,      $��1��5-        GridLineColour#��[ 5.      $	��1.5q�5/      $��Center=�50      $��Center=�51      
 �� EdgeMode$
 Aliased=�52      $��
20,0,20,20q�53      ._�54      A� -   5         A�Name$ GameGrid��54      $	��400��55      $	��400��56        GridBackgroundColour#��[ 57       J� SnapsToDevicePixels$
 True��58       �� BitmapScalingMode$ HighQuality=�59      5:      5;      �� 5=      -   5        $	 Overlay��5>      $��1��5?        OverlayColour#��[ 5@      $
 Aliased=�5D      ��5A      ��  �� Path$ ActualWidth� �� ElementName$ 
GridBorder����5B      �� $ ActualHeight�$ 
GridBorder����5C      �� $ Margin�$ 
GridBorder��._�5E      �� -   5        $ OverlayText��5F      $��Press any key to start��5G      $��26(�5H      $��Center=�5I      $��Center=�5J      $
��Wrap=�5K      5L      5M      5O      5P      5Q      