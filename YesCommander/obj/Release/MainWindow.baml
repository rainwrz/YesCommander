   M S B A M L   `   `   `  ���� G  CYesCommander, Version=1.3.1.0, Culture=neutral, PublicKeyToken=nullI)clr-namespace:YesCommander.CustomControlsYesCommander.CustomControls      YesCommander.MainWindow   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     P LElysium, Version=2.0.976.4, Culture=neutral, PublicKeyToken=afa220db249e5b15/metro#http://schemas.codeplex.com/elysium  7params*http://schemas.codeplex.com/elysium/params  6custom)clr-namespace:YesCommander.CustomControls  -     �� BitmapScalingMode$  NearestNeighbor=� � Title$ YesCommander��$	��580��$
��1020�� � 
ResizeMode$ CanMinimize=� � Icon$ /Resources/icon32.ico�� � WindowStartupLocation$ CenterScreen=�  Elysium.Parameters.Design   Theme$
 DarkX�   Elysium.Parameters.Manager   Theme$
 DarkX�.���  � RowDefinitions �� $
��auto ��� .+��� -   $��about��$��0��$
��10,0q�$��/Resources/help-icon.png��$
��None=�$��Right=���  �� Orientation$ 
Horizontal=�$	��4,0q�$
��Left=�.+�	� -   $��radioFollowers��$��随从一览��$��4q�$y�Hidden=�	� -   $��radioMissions��$��任务模拟��$��4q�$y�Hidden=�	� -   $��radioAllFollowers��$��所有随从��$��4q�$y�Hidden=�	� -   $��radioAnalysis��$��数据分析��$��4q�$y�Hidden=�� -   $��
questPanel��$��1��$��2��$y�Visible=�$��8,-8,0,8q� �� $
��auto ��� $
��auto ��� $
��auto ��� $
��auto ��� .+��� $��0��$ 
Horizontal=�$��4q�$��Center=�.+��� -   $��titleHighMaul��$��悬槌堡任务��$��18(����#ffe8ce$��Bottom=�$��2q��� -	   $��	titleRing��$��橙戒任务��$��15(����#ffe8ce$��Bottom=�$��2q��� -
   $��	titleElse��$��其他三人任务��$��15(����#ffe8ce$��Bottom=�$��2q��� -   $��missionWindowImage��$��/Resources/icon32.ico��$
��None=��� -   $��maxNumberOfPartyComboBox��$��0��$��2��$��Bottom=�$	��200��$��30��$��0,4,60,0q�$��Right=��� $��1��$)��#/Resources/gear-bonus-divider_2.jpg��$
��None=��� $ 
Horizontal=�$��2��$��Center=�	 � 	Resources	 
 �� 
TargetType �� Property �� Value��%�   '��       '�� C     �� "
 ��.���  � ��$ 10,4,0,4q���  � ��$ Center=��� "
 ��.���  � ��$ 4,4,0,4q���  � ��$ 18����  � ��$ 18��.+��� -   $��missionsComboBox��$	��8,4q�$	��200��$
��Left=��� $��装等要求:���� -   $��textItemLevel��
���2�2��� $��任务时间:���� -   $��textMissionTime���� $��技能要求:���� -   $��abilityPanel��$ 
Horizontal=�$	��0,0q�$��Center=�.+��� -   $��ability1���� -   $��ability2���� -   $��ability3���� -   $��ability4���� -   $��ability5���� -   $��ability6���� $��场景:���� -   $��trait���� $��奖励:���� -   $��
textReward��
���2�2��� $��基础胜率:���� -   $��textBasicChance���� $��备注:���� -   $��
textRemark��� -   $��	titleGrid��$��3��$��Center=�$
��1000��$
��Left=�$��4q�$y�Hidden=�	 ��%n   '��       �� "
 ��.���  �@��TextAlignment$ Center=���  � ��$ Center=���  � ��$ 4q� � ColumnDefinitions �� $��50 ��� $
��auto ��� �� $
��auto ��� $	��300 ��� $	��100 �.+��� $��	成功率���� -   $��checkboxMaxiLevel��$��1��$8��2使用最高装等(不影响无装等要求任务)��$��Center=��� $��2��$��追随者详情���� -   $��checkboxUsingFavorite��$��3��$��仅使用偏好随从��$��Center=��� $��4��$��
团队buff���� $��5��$��耗时���� $��5��$��Hidden=�
���   �$
��Auto=�$	��0.8q�.���� -   $��
partyPanel��$��4��$	��0,8q�$
��Left=�	 '   !YesCommander.CustomControls.Party��%2   '        �� "
  .���  � ��$ 4,4,0,4q�� -   $��followerPanel��$��1��$y�Hidden=�$��8,-8,0,8q� �� $
��auto ��� �� $
��auto �.+�� -    $��
titleGrid2��$��0��$��4q�$y�Hidden=� �� $
��auto ��� $
��auto ��� $
��auto �.+��� $��0��$ 
Horizontal=�$��0,0,-8,0q�$��Center=�.+��� -!   $��titleAll��$��全部��$��22(����#ffe8ce$��Bottom=�$��2q��� -"   $��titleFavorite��$��偏好��$��18(����#ffe8ce$��Bottom=�$��2q��� $��1��$)��#/Resources/gear-bonus-divider_1.jpg��$
��None=�$	��100��� $��2��$��Center=�$	��900��$��	-4,10,0,0q�	 ��%n   '��       �� "
 ��.���  �@��TextAlignment$ Center=���  � ��$ Center=���  � ��$ 4q� �� $��30 ��� $	��200 ��� $	��100 ��� $	��180 ��� $��40 ��� $��40 ��� $��* ��� $��2* ��� $
��1.5* ��� $��40 �.+��� $��偏好��$��2��$
��Left=��� $��1��$��姓名���� $��2��$��种族���� $��3��$��职业���� $��4��$��等级���� $��5��$��装等���� $��6��$��技能���� $��7��$��可能习得���� $��8��$��特长���� $��9��$��冻结���� -#   $��	allScroll��$��1��$��Hidden=�
���   �$��Visible=�$��4q�$	��Top=�.���� -$   $��followerRows��$ Vertical=�$��Center=�$	��900���� -%   $��favoriteScroll��$��1��$y�Hidden=�$��Hidden=�
���   �$��Visible=�$��4q�$	��Top=�.���� -&   $��favoriteRows��$ Vertical=�$��Center=�$	��900���� $��2��$ 
Horizontal=�$��8q�$��Center=�.+��� -'   $��90��$��8q�$
��Read���� -(   $��90��$��8q�$��Input���� -)   $��followerImage��$��8q�$��/Resources/icon32.ico��$y�Hidden=�$
��None=�5   /YesCommander.CustomControls.AllFollowersByClass  -*     Name$ followerDetailPanel��$��1��$��8,-8,0,8q�$y�Hidden=�1   +YesCommander.CustomControls.AnalysisControl  -+     Name$ analysisPanel��$��1��$��8,-8,0,8q�$y�Hidden=�