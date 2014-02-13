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
using ColorMathLib.View;

namespace ColorMathManager2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ColorManagerContext context = new ColorManagerContext();

        public MainWindow()
        {
            InitializeComponent();

            colorList.DataContext = context;
            colorList.SetBinding(ColorWeightNpcList.ColorCollectionProperty, "ColorCollection");

            treeResult.DataContext = context;
            treeResult.SetBinding(TreeView.ItemsSourceProperty, "ColorGroupResult");
        }

        private void buttonArrange_Click(object sender, RoutedEventArgs e)
        {
            context.GroupColorCollection();
        }

        Random rand = new Random();

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            var collection = context.ColorCollection;
            for (int i = 0; i < 100; i++)
            {
                
                int setFirst = rand.Next(collection.Count - 2) + 1;
                var item = collection[setFirst];
                collection.Remove(item);
                collection.Insert(0, item);
            }
            context.ColorCollection = collection;
        }
    }
}
