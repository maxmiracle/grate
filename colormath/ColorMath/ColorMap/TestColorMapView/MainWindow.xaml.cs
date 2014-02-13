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
using ColorMapView;
using System.ComponentModel;

namespace TestColorMapView
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        ColorCollectionContext context = new ColorCollectionContext(); 

        public MainWindow()
        {
            InitializeComponent();
            context.AddColor(Colors.Red);
            context.AddColor(Colors.Azure);
            context.AddColor(Colors.Beige);
            myColorList.DataContext = context;
            myColorList.SetBinding(ColorList.ColorCollectionProperty, "ColorCollection");
            colorMap2Pans.DataContext = context;
            colorMap2Pans.SetBinding(ColorMap2Pans.ColorPointsProperty, "ColorCollection");
        }
    }
}
