using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.ComponentModel;

namespace CustomColorPicker
{
    /// <summary>
    /// Interaction logic for CustomColorPicker.xaml
    /// </summary>
    public partial class MyColorPicker : UserControl, INotifyPropertyChanged
    {
        public event Action<Color> SelectedColorChanged;

        String _hexValue = string.Empty;

        public String HexValue
        {
            get { return _hexValue; }
            set { _hexValue = value; }
        }

        //private Color selectedColor = Colors.Transparent;
        //public Color SelectedColor
        //{
        //    get { return selectedColor; }
        //    set
        //    {
        //        if (selectedColor != value)
        //        {
        //            selectedColor = value;
        //        }
        //    }
        //}





        public Color SelectedColor
        {
            get { return (Color)GetValue(SelectedColorProperty); }
            set { 
                SetValue(SelectedColorProperty, value);
                NotifyPropertyChanged("SelectedColor");                
            }
        }

        // Using a DependencyProperty as the backing store for SelectedColor.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectedColorProperty =
            DependencyProperty.Register("SelectedColor", typeof(Color), typeof(CustomColorPicker), new UIPropertyMetadata(Colors.Transparent, OnSelectedColorChanged));

        
        private static void OnSelectedColorChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) 
        {
            Color newColor = (Color)(e.NewValue);
            CustomColorPicker ccp = (CustomColorPicker)d;
            if ((newColor != null) && (ccp!=null))
            {
                ccp.cp.CustomColor = newColor;
                ccp.recContent.Fill = new SolidColorBrush(ccp.cp.CustomColor);
                ccp.HexValue = string.Format("#{0}", ccp.cp.CustomColor.ToString().Substring(1));
            }
        }




        bool _isContexMenuOpened = false;
        public CustomColorPicker()
        {
            InitializeComponent();
            b.ContextMenu.Closed += new RoutedEventHandler(ContextMenu_Closed);
            b.ContextMenu.Opened += new RoutedEventHandler(ContextMenu_Opened);
            b.PreviewMouseLeftButtonUp += new MouseButtonEventHandler(b_PreviewMouseLeftButtonUp);
        }

        void ContextMenu_Opened(object sender, RoutedEventArgs e)
        {
            _isContexMenuOpened = true;
        }

        void ContextMenu_Closed(object sender, RoutedEventArgs e)
        {
            if (!b.ContextMenu.IsOpen)
            {
                if (SelectedColorChanged != null)
                {
                    SelectedColorChanged(cp.CustomColor);
                }
                recContent.Fill = new SolidColorBrush(cp.CustomColor);
                HexValue = string.Format("#{0}", cp.CustomColor.ToString().Substring(1));
                SelectedColor = cp.CustomColor;
            }
            _isContexMenuOpened = false;
        }

        void b_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (!_isContexMenuOpened)
            {
                if (b.ContextMenu != null && b.ContextMenu.IsOpen == false)
                {
                    b.ContextMenu.PlacementTarget = b;
                    b.ContextMenu.Placement = System.Windows.Controls.Primitives.PlacementMode.Bottom;
                    ContextMenuService.SetPlacement(b, System.Windows.Controls.Primitives.PlacementMode.Bottom);
                    b.ContextMenu.IsOpen = true;
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(string prop)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged( this, new PropertyChangedEventArgs( prop ));
            }
        }
    }
}
