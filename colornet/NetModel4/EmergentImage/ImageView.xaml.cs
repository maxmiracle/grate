using Net;
using NLog;
using System;
using System.Collections.Generic;
using System.Drawing;
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
using Color = System.Windows.Media.Color;
using Rectangle = System.Windows.Shapes.Rectangle;

namespace EmergentImage
{
    /// <summary>
    /// Interaction logic for ImageView.xaml
    /// </summary>
    public partial class ImageView
    {
        public ImageView()
        {
            InitializeComponent();
        }

        private static double GridViewScaleValue = 3;

        private bool IsGridView { get; set; }

        private Rectangle[,] rectPixels;

        private SolidColorBrush ColorRelationBrush = new SolidColorBrush(Colors.BlueViolet);
        private double ColorRelationStrokeThikness = 0.3;

        private SolidColorBrush GroupBorderBrush = new SolidColorBrush(Colors.DarkBlue);
        private double GroupBorderStrokeThikness = 0.3;

        public ImageView(BitmapImage bitmap) : this()
        {
            ImgBitmap = bitmap;
            UpdateWorkingArea();
        }

        private void ScaleChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            UpdateWorkingArea();
        }

        public BitmapImage ImgBitmap
        {
            get { return (BitmapImage)GetValue(ImgBitmapProperty); }
            set
            {
                SetValue(ImgBitmapProperty, value);
            }
        }

        public static readonly DependencyProperty ImgBitmapProperty =
            DependencyProperty.Register("ImgBitmap", typeof(BitmapImage), typeof(ImageView), new PropertyMetadata(null, (o, args) => UpdateImgLayerLayout(o, args) ));

        private static void UpdateImgLayerLayout(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs args)
        {
            if (args.OldValue != args.NewValue)
                ((ImageView)dependencyObject).UpdateWorkingArea((BitmapImage)(args.NewValue));
        }

        private void UpdateWorkingArea(BitmapImage bitmapImage = null)
        {
            if (bitmapImage != null)
            {
                ImageControl.Source = bitmapImage;
            }
        }

    }
}
