   M S B A M L   `   `   `  ���� E  ASpinerBase, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:SpinerBaseBESpinerBaseBE      SpinerBaseBE.App   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     ,localclr-namespace:SpinerBaseBE       �� 	Resources     	BaseColor �� Color  BaseColorSelected  	BackColor  	FontColor  DetailColor  
LightColor  Font  stlLabelBase �� 
TargetType �� Property �� Value  stlTextBoxBase 	 stlDatePickerBase(  "System.Windows.Controls.DatePicker 
 stlListBoxBase  stlTextBlockBase  stlCheckboxBase  stlRadioButtonBase  stlComboBoxBase  ButtonStyle �� 
TargetType �� Triggers P� Property P� Value  ButtonRoundStyle  ButtonRoundRectangleStyle # ButtonRoundRectangleStyleSelect  ButtonRoundElipseStyle  ComboBoxToggleButton	 � ColumnDefinitions
 ��Name �� CornerRadius F�Name  ComboBoxTextBox �� 	ClickMode d� 	IsChecked �� Path �� Mode �� RelativeSource J� IsHitTestVisible �� SelectionBoxItem �� SelectionBoxItemTemplate ��Name �� 
IsReadOnly �� 
IsReadOnly �� IsDropDownOpen � AllowsTransparency J� SnapsToDevicePixels � ActualWidth �� MaxDropDownHeight K� IsItemsHost-  'System.Windows.Input.KeyboardNavigation   DirectionalNavigation �� Triggers  �� 
TargetName! P� 
SourceName" �� Padding  StandardBorderBrush  StandardBackgroundBrush  HoverBorderBrush  SelectedBackgroundBrush  SelectedForegroundBrush  DisabledForegroundBrush  NormalBrush  NormalBorderBrush  HorizontalNormalBrush  HorizontalNormalBorderBrush   ListBoxBackgroundBrush# �� 
StartPoint$ �� EndPoint% � Color& � Offset ! StandardBrush " 
GlyphBrush # PressedBrush $ ScrollBarLineButton % ScrollBarPageButton & ScrollBarThumb ' VerticalScrollBar' � RowDefinitions( ]� DecreaseRepeatButton) ]� Thumb* ]� IncreaseRepeatButton ( HorizontalScrollBar ) FavsScrollViewer+ �� VerticalOffset, � Value- �� ScrollableHeight. � Maximum/ �� ViewportHeight0 �� ViewportSize*1 �� #ComputedVerticalScrollBarVisibility2 �� Orientation3 �� HorizontalOffset4 �� ScrollableWidth5 �� ViewportWidth,6 �� %ComputedHorizontalScrollBarVisibility��%�  &	        &	      &	 $     &	 7     &	 J     &	 _     &	 t     &	 �     7  7  &	 �     7  7  &		 A    7  7  &	
 �    7  7  &	 �    7  7  &	 ^    7  7  &	 �    7  7  &	 P    7  7  &	 �    7   7  7  7  &	 �    &	 �    7  &	     7  7  &	 �    7   7  &	 �    7   7   7  &	 �    '�� 1    7  7  7  7  7   '�� ]    7  7  7  7   &	 �    &	 �    &	 �    &	 �    &	 �    &	 �    &	 �    &	 
    &	     &	 +    &	  ;    &	! �    &	" 
    &	#     &	$ �    7  7  7 " 7 # 7  &	% A    &	&     7   7   &	' �    7 $ 7 % 7 & 7  7  7 % 7 $ &	( �    7 $ 7 % 7 & 7   7   7 % 7 $ '�� _    7 ( 7 ' &	) �    �� $ Orange���� $ Yellow���� $ #ffa000���� $ #ff6600���� $ 	#66ff6600���� $ 	#66ffff00��*�	Arial)��� " ��.���  � ��8   ��  � ��8  ��  � ��$
 Bold"���  � ��$ 12(��� " ��.���  � ��8   ��  � ��8  ��  � ��$
 Bold"���  � ��$ 12(��� "  .���  � ��8   ��  � ��8  ��  � ��$
 Bold"���  � ��$ 12(��� " ��.���  � ��8   ��  � ��8  ��  � ��$
 Bold"���  � ��$ 12(��� " ��.���  � ��8   ��  � ��8  ��  � ��$
 Bold"���  � ��$ 12(��� " ��.���  � ��8   ��  � ��8  ��  � ��$
 Bold"���  � ��$ 12(���  � ��$ Center=��� " 	�.���  � ��8   ��  � ��8  ��  � ��$
 Bold"���  � ��$ 12(���  � ��$ Center=��� " ��.���  � ��8   ��  � ��8  ��  � ��$
 Bold"���  � ��$ 12(���  � ��
 ������� " ��.���  � ��8   ��  � ��8  ��  � ��8  ��  � ��$
 Bold"���  � ��$ Center=���  � �� �� " ��.R��� #��z��._��� $��Center=�$��Center=� P�  �@J�IsMouseOver . . ���  � ��8  �� " ��.���  � ��
 ���� ��  � �� �� " ��.R��� #��z��._��� $��Center=�$��Center=���  � ��$ 50����  � ��$ 50����  �@�Cursor$
 Hand�� P�  �@J�IsMouseOver . . ���  � ��
 ���� �� " ��.���  � ��8   ��  �@��CornerRadius$ 10,10,10,10���� " ��.���  � ��8   ��  �@��CornerRadius$ 10,10,10,10�� P�  �@J�IsMouseOver . . ���  � ��8  �� " A�.���  � ��8    P�  �@J�IsMouseOver . . ���  � ��8  �� " d�.R�� 	 �� �� $��20 �.+��� $
 Border��$��2��$ 0��
�������8��  $��1q��� $��0��$ 0��$��1q�
�������8�� $��0,0,1,0q�F� $ Arrow��$��1��8�� $��Center=�$��Center=�=����        �    �� � 	= ��[ � z �� � z     � 	=  	= 	�� " ��.R��� $
 PART_ContentHost��|�.  #��z���� " ��.���  �@J�SnapsToDevicePixels . ��  �@�OverridesDefaultStyle . ��  � ��$
 Auto=���  � ��$
 Auto=���  � �� . ��  � ��$	 100����  � ��$ 20����  � ��8   ��  � �� �� " ��.R�� .+�d� $��ToggleButton��8�� $��2��|�.  $ Press=� �� $ IsDropDownOpen�$ TwoWay=� �� *TemplatedParent+�� $��ContentSite�� .  #��z #��z #��z��$��3,3,23,3q�$��Center=�$
��Left=��� $ PART_EditableTextBox��8�� $
��Left=�$��Center=�$��3,3,23,3q�|�. 
�������8�� $y�Hidden=�# z ��Z� � $��Popup��$��Bottom=�#��z  . |�.  $��Slide=�.��� $��DropDown�� . #��z #��z .+��� $
 DropDownBorder��
�������$��1q�8�� �� $��4,6,4,6q� . .����  . $ 	Contained=� P�  �@��HasItems .  . ��� $  DropDownBorder�� � ��$ 95��P�  � {� .  . ���  � �� �#888888P�  �@��
IsGrouping . . ���  � �� .  P� $! Popup�� �@�AllowsTransparency . . ��� $  DropDownBorder�� �@��CornerRadius$ 0���� $  DropDownBorder�� � ��$ 0,2,0,0q�P�  �@��
IsEditable . . ���  � �� .  �� $  PART_EditableTextBox�� � y�$ Visible=��� $  ContentSite�� � y�$ Hidden=� �� " ��.���  �@J�SnapsToDevicePixels . ��  � ��8   ��  � ��8  ��  �@�OverridesDefaultStyle . ��  � �� �� " ��.R��� $��Border��$" 2q� . ._���  P�  �@��IsHighlighted . . ��� $  Border��8   � ��P�  � {� .  . ��� 8   � ���� $
 #888���� $ Black���� $
 #DDD���� $
 Gray���� $ White���� $
 #888���� $
 #888���� $
 #888���� $ Black���� $
 #888���� $	# 0,0&�$$ 1,0.001&���� � $% White��$	& 0.0Q�� $% White��$	& 0.6Q�� $% #DDDDDD��$	& 1.2Q��� $	# 0,0&�$	$ 0,1&���� � $
% #FFF��$	& 0.0Q�� $
% #CCC��$	& 1.0Q��� $
 #444���� $	# 0,0&�$	$ 0,1&���� � $
% #BBB��$	& 0.0Q�� $
% #EEE��$	& 0.1Q�� $
% #EEE��$	& 0.9Q�� $
% #FFF��$	& 1.0Q��� " ��.���  � y�$ Hidden=���  �@J�SnapsToDevicePixels . ��  �@�OverridesDefaultStyle . ��  � |� .  ��  � �� �� " ��.R��� $��Border��$��1q�$ 2��8��  8�� $��1q�._�F� $��Center=�$��Center=�8�� ���� $ Content� �� *TemplatedParent+ P�  � �� . . ��� $  Border��8   � ��P�  � {� .  . ��� 8   � ���� " ��.���  � y�$ Hidden=���  �@J�SnapsToDevicePixels . ��  �@�OverridesDefaultStyle . ��  � �� .  ��  � |� .  ��  � �� �� " ��.R��� 
���   ��� " n�.���  �@J�SnapsToDevicePixels . ��  �@�OverridesDefaultStyle . ��  � �� .  ��  � |� .  ��  � �� �� " n�.R��� $ 2��8��  8�� $��0q�$��8��$��8,0,-2,0q��� " ��.R�� ' �� $��0���� $��0.00001* ��� $��0��.+��� $��3��$ 2��
������ �� $��0��8��  $��18��$��ScrollBar.LineUpCommand��$��M 0 4 L 8 4 L 4 0 Z��]� $��
PART_Track��$��1����. ( �� 8�� $��ScrollBar.PageUpCommand��) n� 8�� $��1,0,1,0q�8�� 8�� * �� 8�� $��ScrollBar.PageDownCommand���� $��3��8�� $��18��$��ScrollBar.LineDownCommand��$��M 0 0 L 4 4 L 8 0 Z���� " ��.R�� 	 �� $��18���� $��0.00001* ��� $��18��.+��� $��3��$ 2�����#F0F0F0�� $��0��8��  $��18��$��ScrollBar.LineLeftCommand��$��M 4 0 L 4 8 L 0 4 Z��]� $��
PART_Track��$��1����.  ( �� 8�� $��ScrollBar.PageLeftCommand��) n� 8�� $��0,1,0,1q�8�� 8�� * �� 8�� $ ��ScrollBar.PageRightCommand���� $��3��8�� $��18��$ ��ScrollBar.LineRightCommand��$��M 0 0 L 4 4 L 0 8 Z���� " ��.���  �@J�SnapsToDevicePixels . ��  �@�OverridesDefaultStyle .  P�  �@��Orientation$ 
Horizontal=�. ���  � ��$
 Auto����  � ��$ 18����  � ��8   P�  �@��Orientation$ Vertical=�. ���  � ��$ 18����  � ��$
 Auto����  � ��8  �� " ��.���  �@�OverridesDefaultStyle . ��  � �� �� " ��.R�� 	 �� $
��Auto ��� ' �� �� $
��Auto �.+��� $��1���� $��PART_VerticalScrollBar��#, z+ #. z- #0 z/ #y�z1 �� $��PART_HorizontalScrollBar��$2 
Horizontal=�$��1��$��1��#, z3 #. z4 #0 z5 #y�z6 