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
using Grate.ColorDispersion;

namespace ColorMathLib.View
{
    /// <summary>
    /// Interaction logic for ColorWeightNpcList.xaml
    /// </summary>
    public partial class ColorWeightNpcList : UserControl
    {
        public ColorWeightNpcList()
        {
            InitializeComponent();
            colorList.DataContext = this;
            ColorCollection = new ObservableCollection<ColorWeightNpc>();
            colorList.SetBinding(ListBox.ItemsSourceProperty, "ColorCollection");
        }

        public ObservableCollection<ColorWeightNpc> ColorCollection
        {
            get { return (ObservableCollection<ColorWeightNpc>)GetValue(ColorCollectionProperty); }
            set
            {
                SetValue(ColorCollectionProperty, value);
            }
        }

        // Using a DependencyProperty as the backing store for ColorList.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ColorCollectionProperty =
            DependencyProperty.Register("ColorCollection", typeof(ObservableCollection<ColorWeightNpc>), typeof(ColorWeightNpcList), new UIPropertyMetadata(null));

        private void addItem_Click(object sender, RoutedEventArgs e)
        {
            ColorCollection.Add(new ColorWeightNpc() { Color = Colors.Azure, Weight = 1 });
        }

        private void removeItem_Click(object sender, RoutedEventArgs e)
        {
            int colorIndex = colorList.SelectedIndex;
            if ( ( colorIndex >= 0 ) && ( ColorCollection.Count > colorIndex ))
                ColorCollection.RemoveAt(colorIndex);
        }
    }
}
