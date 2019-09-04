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

namespace EmergentImage
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        EmergentImageContext context;

        public MainWindow()
        {
            InitializeComponent();

            EmergentImageContext context = new EmergentImageContext();
            this.DataContext = context;
            this.context = context;
        }

        public void Open(object sender, ExecutedRoutedEventArgs e)
        {
            context.Open(sender, e);
        }

        public void CanOpen(object sender, CanExecuteRoutedEventArgs e)
        {
            context.CanOpen(sender, e);
        }

        public void AnalyseColors(object sender, ExecutedRoutedEventArgs e)
        {
            context.AnalyseColors(sender, e);
        }

        public void CanAnalyseColors(object sender, CanExecuteRoutedEventArgs e)
        {
            context.CanAnalyseColors(sender, e);
        }
    }
}
