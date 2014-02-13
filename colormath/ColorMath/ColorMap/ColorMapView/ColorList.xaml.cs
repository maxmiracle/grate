using System;
using System.Collections.Generic;
using System.Linq;
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
using System.Collections.ObjectModel;

namespace ColorMapView
{
    /// <summary>
    /// Interaction logic for ColorList.xaml
    /// </summary>
    public partial class ColorList : UserControl
    {
        public ColorList()
        {
            InitializeComponent();
            colorList.DataContext = this;
            colorList.SetBinding(ListBox.ItemsSourceProperty, "ColorCollection");
        }

        public ObservableCollection<ColorBox> ColorCollection
        {
            get { return (ObservableCollection<ColorBox>)GetValue(ColorCollectionProperty); }
            set
            {
                SetValue(ColorCollectionProperty, value);
            }
        }

        // Using a DependencyProperty as the backing store for ColorList.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ColorCollectionProperty =
            DependencyProperty.Register("ColorCollection", typeof(ObservableCollection<ColorBox>), typeof(ColorList), new UIPropertyMetadata(null));

        private void addItem_Click(object sender, RoutedEventArgs e)
        {
            ColorCollection.Add(new ColorBox() { Color = Colors.Transparent });
        }

        private void removeItem_Click(object sender, RoutedEventArgs e)
        {
            int colorIndex = colorList.SelectedIndex;
            if ( ( colorIndex > 0 ) && ( ColorCollection.Count > colorIndex ))
                ColorCollection.RemoveAt(colorIndex);
        }

        


        
    }
}
