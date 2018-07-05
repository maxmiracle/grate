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
using Grate.ColorDispersion;
using ColorMathLib.ColorSet;

namespace ColorMathManager
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void buttonTest1_Click(object sender, RoutedEventArgs e)
        {
            treeTestSet.Items.Clear();
            foreach (ColorWeight cw in TestColorSet.GetTestColorSet(int.Parse(textBoxNum.Text)))
            {
                treeTestSet.Items.Add(cw);
            }
        }

        private void buttonArrange_Click(object sender, RoutedEventArgs e)
        {
            List<ColorWeight> list = new List<ColorWeight>();
            foreach ( ColorWeight cw in treeTestSet.Items )
            {
                list.Add( cw );
            }
            ColorGroup cg = ColorGroup.CreateColorGroup2(list.ToArray());
            treeResult.Items.Add(cg);
        }

        private void scrollBar1_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {

        }
    }
}
