   M S B A M L   `   `   `  ����   YesCommander5    /YesCommander.CustomControls.AllFollowersByClass   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     P LElysium, Version=2.0.976.4, Culture=neutral, PublicKeyToken=afa220db249e5b15/metro#http://schemas.codeplex.com/elysium  7params*http://schemas.codeplex.com/elysium/params    �� BitmapScalingMode$  NearestNeighbor=�.���  � RowDefinitions �� $
��auto ��� $
��auto ��� $
��auto ��� $
��auto ��� $
��auto ��� .+��� $��0��$��2�� �� Orientation$ 
Horizontal=�$��4q�$��Center=�.+��� -   $��missionWindowImage��$��/Resources/icon32.ico��$
��None=��� $��所有随从一览��$��18(����#ffe8ce$��Bottom=�$��2q��� $ 
Horizontal=�$��1��$��Center=�$��Center=� � 	Resources  �� 
TargetType �� Property �� Value��%�   '��       '�� C     �� " ��.���  � ��$ 10,4,0,4q���  � ��$ Center=��� " ��.���  � ��$ 4,4,0,4q���  � ��$ 18����  � ��$ 18��.+��� -   $��classComboBox��$	��8,4q�$	��120��$
��Left=��� $C��=此天赋职业可以习得以下技能（任意俩组合）:���� -   $��abilityPanel��$ 
Horizontal=�$	��0,0q�$��Center=�.+��� -   $��ability1���� -   $��ability2���� -   $��ability3���� -   $��ability4���� -   $��ability5���� $��2��$��Center=�$ 
Horizontal=�.+��� -	   $��titleAli��$��3��$��联盟��$��22(�
������$��Bottom=�$��2q�$��Center=��� -
   $��titleHrd��$��3��$��1��$��部落��$��18(�
���  ��$��Bottom=�$��2q�$��Center=��� $��3��$)��#/Resources/gear-bonus-divider_2.jpg��$
��None=�$	��100��� $��4��$	��850�� � ColumnDefinitions �� �� �� �� $	��110 ��� $��50 ��� $��50 ��� $��50 ��� $��50 � ��%\   '��       �� " ��.���  � ��$ 4q���  � ��$ 12(���  � ��$ Center=�.+��� $��	英文名���� $��	简体名��$��1���� $��	繁体名��$��2���� $��种族��$��3���� $��	始等级��$��4���� $��	始技能��$��5���� $��	始特长��$��6���� $��	已拥有��$��7���� -   $��	aliScroll��$��5��$��Hidden=�
���   �$
��Auto=�$	��0,8q�.���� -   $��aliPanel��$��8q��� -   $��	hrdScroll��$��5��$��Hidden=�
���   �$
��Auto=�$	��0,8q�$y�Hidden=�.���� -   $��hrdPanel��$��1��$��8q�