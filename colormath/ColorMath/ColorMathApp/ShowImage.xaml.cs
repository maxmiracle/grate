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
using System.Windows.Shapes;

namespace ColorMathApp
{
    /// <summary>
    /// Interaction logic for ShowImage.xaml
    /// </summary>
    public partial class ShowImage : Window
    {
        public ShowImage()
        {
            InitializeComponent();
        }

        public ShowImage(string title, BitmapSource src) : this()
        {
            labelTitle.Content = title;
            image.Source = src;
        }
    }
}
