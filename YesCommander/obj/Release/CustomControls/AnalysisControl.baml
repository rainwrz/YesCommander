   M S B A M L   `   `   `  ���� G  CYesCommander, Version=1.3.1.0, Culture=neutral, PublicKeyToken=null_4clr-namespace:YesCommander.CustomControls.Components&YesCommander.CustomControls.Components  1    +YesCommander.CustomControls.AnalysisControl   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     P LElysium, Version=2.0.976.4, Culture=neutral, PublicKeyToken=afa220db249e5b15/metro#http://schemas.codeplex.com/elysium  7params*http://schemas.codeplex.com/elysium/params  E
components4clr-namespace:YesCommander.CustomControls.Components  -     �� BitmapScalingMode$  NearestNeighbor=�.���  � RowDefinitions �� $
��auto ��� $
��auto ��� $
��auto ��� $
��auto ��� $
��auto ��� .+��� $��0�� �� Orientation$ 
Horizontal=�$��4q�$��Center=�.+��� -   $��missionWindowImage��$��/Resources/icon32.ico��$
��None=��� -   $��abilityAnalysis��$��技能分析��$��18(����#ffe8ce$��Bottom=�$��2q��� -   $��traitAnalysis��$��特长分析��$��15(����#ffe8ce$��Bottom=�$��2q��� -   $��raceAnalysis��$��种族分析��$��15(����#ffe8ce$��Bottom=�$��2q��� -   $��classString��$��1��$��0,8,0,4q�$
��Wrap=�$]��W选择任意一个或者两个技能组合，查看可拥有此组合的职业天赋。��$��30��$��Center=� �� TextAlignment$ Center=����#ffe8ce�� -   $��abilityCheckBoxPanel��$��2��$	��0,2q�$ 
Horizontal=�$��2��$��Center=�$��Center=�.+�<   6YesCommander.CustomControls.Components.AbilityCheckBox                    �� -   $��traitCheckBoxPanel��$��2��$	��0,2q�$ 
Horizontal=�$��Center=�$��Center=�$y�Hidden=��� -	   $��raceRadioPanel��$��2��$	��0,2q�$ 
Horizontal=�$��Center=�$��Center=�$y�Hidden=�.+�	� -
   $��侏儒��$	��4,0q�
����·�	� -   $��人类��$	��4,0q�
����·�	� -   $��矮人��$	��4,0q�
����·�	� -   $��暗夜精灵��$	��4,0q�
����·�	� -   $��	德莱尼��$	��4,0q�
����·�	� -   $��狼人��$	��4,0q�
����·�	� -   $��兽人��$	��4,0q�
����·�	� -   $��被遗忘者��$	��4,0q�
����·�	� -   $��	牛头人��$	��4,0q�
����·�	� -   $��巨魔��$	��4,0q�
����·�	� -   $��	血精灵��$	��4,0q�
����·�	� -   $��地精��$	��4,0q�
����·�	� -   $��	熊猫人��$	��4,0q�
����·�	� -   $��其他��$	��4,0q�
����·��� $��3��$��Center=�$ 
Horizontal=�.+��� -   $��titleMy��$��麾下��$��22(�
����+��$��Bottom=�$��2q�$��Center=��� -   $��titleAli��$��联盟��$��18(�
������$��Bottom=�$��2q�$��Center=��� -   $��titleHrd��$��1��$��部落��$��18(�
���  ��$��Bottom=�$��2q�$��Center=��� $��4��$)��#/Resources/gear-bonus-divider_2.jpg��$
��None=�$	��150��$��0,0,0,4q�� -   $��myScroll��$��5�� �� $
��auto ��� .+�� $��Center=�$	��900��$��-4,0,0,0q� � 	Resources  �� 
TargetType �� Property �� Value��%n   '��       �� " ��.���  �@��TextAlignment$ Center=���  � ��$ Center=���  � ��$ 4q� � ColumnDefinitions �� $��30 ��� $	��200 ��� $	��100 ��� $	��180 ��� $��40 ��� $��40 ��� $��* ��� $��2* ��� $
��1.5* ��� $��40 � �� �� .+��� $��偏好��$��2��$
��Left=��� $��1��$��姓名���� $��2��$��种族���� $��3��$��职业���� $��4��$��等级���� $��5��$��装等���� $��6��$��技能���� $��7��$��可能习得���� $��8��$��特长���� $��9��$��冻结���� $��1��$��Hidden=�
���   �$
��Auto=�$	��0,4q�.���� -   $��myPanel��� -   $��	aliScroll��$��5��$y�Hidden=� �� $
��auto ��� .+�� $	��850��$��Center=� �� �� �� �� $	��110 ��� $��50 ��� $��50 ��� $��50 ��� $��50 � ��%\   '��       �� " ��.���  � ��$ 4q���  � ��$ 12(���  � ��$ Center=�.+��� $��	英文名���� $��	简体名��$��1���� $��	繁体名��$��2���� $��种族��$��3���� $��	始等级��$��4���� $��	始技能��$��5���� $��	始特长��$��6���� $��	已拥有��$��7���� $��1��$��Hidden=�
���   �$
��Auto=�$��0,4,0,8q�.���� -   $��aliPanel��� -   $��	hrdScroll��$��5��$y�Hidden=� �� $
��auto ��� .+�� $	��850��$��Center=� �� �� �� �� $	��110 ��� $��50 ��� $��50 ��� $��50 ��� $��50 � ��%\   '��       �� " ��.���  � ��$ 4q���  � ��$ 12(���  � ��$ Center=�.+��� $��	英文名���� $��	简体名��$��1���� $��	繁体名��$��2���� $��种族��$��3���� $��	始等级��$��4���� $��	始技能��$��5���� $��	始特长��$��6���� $��	已拥有��$��7���� $��1��$��Hidden=�
���   �$
��Auto=�$��0,4,0,8q�.���� -    $��hrdPanel��