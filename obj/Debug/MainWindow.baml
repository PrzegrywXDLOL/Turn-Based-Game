   M S B A M L   `   `   `  ����J  FTurn-Based-Game, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null1clr-namespace:Turn_Based_GameTurn_Based_Game  5   	   O Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem 65        Turn_Based_Game.MainWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   /localclr-namespace:Turn_Based_Game     5   	   6sys&clr-namespace:System;assembly=mscorlib     65     � Title$  Turn Based Game��5   	   $	��650��6!   $
��1000��6.    � 
ResizeMode$ NoResize=�5	   	   .��5      � 
����·� � RowDefinitions 5   
   �� 5      $��0.25* �6   �� 5      �� 5      $��0.25* �6    � ColumnDefinitions 5   
   �� 5      �� 5      .+�5   
   �� -   5         ��Name$ Player1Name��5      $��0��5      $��0��6!      TopInfo#��[  5      $��Player 1 Placeholder��5   )   �� 5   
   -   5        $ Player2Name��5      $��0��5      $��1��6!   #��[  5      $��Player 2 Placeholder��5   )   � 5   
   -   5         �Name$ 	Player1HP��5      $��0��5      $��0��6#   5      � 5   
   -   5        $ 	Player2HP��5       $��0��5      $��1��6#   5       �� 5#   
   -   5         ��Name$ pl1Buff��5$       	 Buffs#��[ 5#      $��0��6/   $��1��5$      6   �� 5%   
   -   5        $ pl2Buff��5&   -   #��[ 5%      $��1��5&      $��1��6    6-   �� 5(   
   $��0��6   $��2��6'   .��5*      �� -   5        $ 	roundInfo��5*        	RoundInfo#��[ 5+      $����TEST TEST TEST TEST TEST TEST TEST TEST TEST TEST TEST TEST TEST TEST TEST TEST TEST TEST TEST TEST TEST TEST TEST TEST TEST TEST TEST TEST TEST TEST TEST TEST TEST TEST TEST TEST TEST TEST TEST TEST TEST TEST TEST TEST TEST TEST TEST TEST ��5,      5.      � 50   
   $��2��6   $��1��6   
���"�"� 52      �� 53      �� 54       56      �� 57      �� 58      .+�5;      �� -   5         ��Name$ Attack��5;      $��0��6%   $��0��5<      $��Attack��6"   �� 5=      -	   5        $ Block��5=      $��0��6$   $��1��5>      $��Block��6"   $��2��5?      �� 5@      -
   5        $
 Heal��5@      $��1��6#   $��0��5A      $
��Heal��6"   5C      5D      5E      