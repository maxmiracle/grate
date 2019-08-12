using Net;
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
    /// Interaction logic for ImageView.xaml
    /// </summary>
    public partial class ImageView : UserControl
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

        public ImageView(SqImgLayer layer) : this()
        {
            ImgLayer = layer;
            UpdateWorkingArea();
        }

        private void ScaleChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            //UpdateWorkingArea();
        }

        public SqImgLayer ImgLayer
        {
            get { return (SqImgLayer)GetValue(ImgLayerProperty); }
            set
            {
                SetValue(ImgLayerProperty, value);
            }
        }

        public static readonly DependencyProperty ImgLayerProperty =
            DependencyProperty.Register("ImgLayer", typeof(SqImgLayer), typeof(ImageView), new PropertyMetadata(null, (o, args) => UpdateImgLayerLayout(o, args) ));

        private static void UpdateImgLayerLayout(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs args)
        {
            ((ImageView)dependencyObject).UpdateWorkingArea((SqImgLayer)(args.NewValue));
        }

        private void UpdateWorkingArea(SqImgLayer imgLayer = null)
        {
            if (imgLayer == null) imgLayer = ImgLayer;

            var LatticeHeight = imgLayer.Height;
            var LatticeWidth = imgLayer.Width;
            double scale = 2;
            LatticeCanvas.Width = LatticeWidth * scale;
            LatticeCanvas.Height = LatticeHeight * scale;
            rectPixels = new Rectangle[LatticeWidth, LatticeHeight];
            IsGridView = scale >= GridViewScaleValue;
            LatticeCanvas.Children.Clear();

            // Show color of reactor
            foreach (SqRt sqRt in ImgLayer)
            {


                Color color;
                color = sqRt.Input;

                Rectangle newRect = new Rectangle() { Fill = new SolidColorBrush(color), StrokeThickness = 0, Height = scale, Width = scale, Tag = sqRt, DataContext = sqRt };
                newRect.MouseLeftButtonDown += newRect_MouseLeftButtonDown;
                Canvas.SetLeft(newRect, sqRt.X * scale);
                Canvas.SetTop(newRect, sqRt.Y * scale);
                LatticeCanvas.Children.Add(newRect);
                rectPixels[sqRt.X, sqRt.Y] = newRect;





                //// Nearest vector
                //if (ShowClustersButton.IsChecked == true)
                //{

                //    // Show nearest by Color
                //if (sqRt.LiquidityDirection != null)
                //{
                //    XY xyDir = sqRt.LiquidityDirection;

                //    var scaleOneSecond = scale / 2;
                //    var x1 = sqRt.X * scale + scaleOneSecond;
                //    var y1 = sqRt.Y * scale + scaleOneSecond;
                //    var x2 = (sqRt.X + (xyDir.X) * 0.5) * scale + scaleOneSecond;
                //    var y2 = (sqRt.Y + (xyDir.Y) * 0.5) * scale + scaleOneSecond;
                //    var newLine = new Line() { X1 = x1, Y1 = y1, X2 = x2, Y2 = y2, Stroke = ColorRelationBrush, StrokeThickness = ColorRelationStrokeThikness };
                //    LatticeCanvas.Children.Add(newLine);
                //}

                //}
            }
        }


        //if ( isClusterGain.IsChecked == true)
        //{
        //    // Neighbour gain value show if the Neighbour Mode is selected
        //    foreach (SqRt sqRt in layer)
        //    {
        //        Color color = CW.GetColor(sqRt.S2);
        //        Rectangle newRect = new Rectangle() { Fill = new SolidColorBrush(color), StrokeThickness = 0, Height = scale, Width = scale, Tag = sqRt, DataContext = sqRt };
        //        newRect.MouseLeftButtonDown += newRect_MouseLeftButtonDown;
        //        Canvas.SetLeft(newRect, sqRt.X * scale);
        //        Canvas.SetTop(newRect, sqRt.Y * scale);
        //        LatticeCanvas.Children.Add(newRect);
        //        rectPixels[sqRt.X, sqRt.Y] = newRect;
        //    }
        //}
        //else if (isNeighbourGain.IsChecked == true)
        //{
        //    // Neighbour gain value show if the Neighbour Mode is selected
        //    foreach (SqRt sqRt in layer)
        //    {
        //        Color strokeColor = Colors.WhiteSmoke;
        //        Color color = CW.GetColor(sqRt.NCG2);
        //        if (sqRt.IsNCGMax)
        //        {
        //            color = Colors.BlueViolet;
        //        }
        //        Rectangle newRect = new Rectangle() { Fill = new SolidColorBrush(color), StrokeThickness = 0.05,
        //            Stroke = new SolidColorBrush(strokeColor),
        //            Height = scale, Width = scale, Tag = sqRt, DataContext = sqRt };
        //        newRect.MouseLeftButtonDown += newRect_MouseLeftButtonDown;
        //        Canvas.SetLeft(newRect, sqRt.X * scale);
        //        Canvas.SetTop(newRect, sqRt.Y * scale);
        //        LatticeCanvas.Children.Add(newRect);
        //        rectPixels[sqRt.X, sqRt.Y] = newRect;
        //    }
        //}
        //    else 
        //    {
        //        // Show color of reactor
        //        foreach (SqRt sqRt in layer)
        //        {
        //            Color color = sqRt.Input;
        //            Rectangle newRect = new Rectangle() { Fill = new SolidColorBrush(color), StrokeThickness = 0, Height = scale, Width = scale, Tag = sqRt, DataContext = sqRt };
        //            newRect.MouseLeftButtonDown += newRect_MouseLeftButtonDown;
        //            Canvas.SetLeft(newRect, sqRt.X * scale);
        //            Canvas.SetTop(newRect, sqRt.Y * scale);
        //            LatticeCanvas.Children.Add(newRect);
        //            rectPixels[sqRt.X, sqRt.Y] = newRect;
        //        }
        //    }


        //    // Nearest vector
        //    if (isNearest.IsChecked == true) 
        //foreach (SqRt sqRt in layer)
        // {
        //     // Show nearest by Color
        //     if (sqRt.NearestByColor != null)
        //     {
        //         var scaleOneSecond = scale / 2;
        //         var x1 = sqRt.X * scale + scaleOneSecond;
        //         var y1 = sqRt.Y * scale + scaleOneSecond;
        //         var x2 = (sqRt.X + (sqRt.NearestByColor.X - sqRt.X) * 0.5) * scale + scaleOneSecond;
        //         var y2 = (sqRt.Y + (sqRt.NearestByColor.Y - sqRt.Y) * 0.5) * scale + scaleOneSecond;
        //         var newLine = new Line() { X1 = x1, Y1 = y1, X2 = x2, Y2 = y2, Stroke = ColorRelationBrush, StrokeThickness = ColorRelationStrokeThikness };
        //         LatticeCanvas.Children.Add(newLine);
        //     }
        // }

        //    // Groups border
        //    if (isGroup.IsChecked == true)
        //    foreach (SqRt sqRt in layer)
        //    {
        //        SqRt up;
        //        if ((sqRt.nels.TryGetValue(XY.XYUp, out up)) && (up.ColorGroup.GroupId != sqRt.ColorGroup.GroupId))
        //        {
        //            var x1 = sqRt.X * scale;
        //            var y1 = sqRt.Y * scale;
        //            var x2 = x1 + scale;
        //            var y2 = y1;
        //            var newLine = new Line() { X1 = x1, Y1 = y1, X2 = x2, Y2 = y2, Stroke = GroupBorderBrush, StrokeThickness = GroupBorderStrokeThikness };
        //            LatticeCanvas.Children.Add(newLine);
        //        };
        //        SqRt left;
        //        if ((sqRt.nels.TryGetValue(XY.XYLeft, out left)) && (left.ColorGroup.GroupId != sqRt.ColorGroup.GroupId))
        //        {
        //            var x1 = sqRt.X * scale;
        //            var y1 = sqRt.Y * scale;
        //            var x2 = x1;
        //            var y2 = y1 + scale;
        //            var newLine = new Line() { X1 = x1, Y1 = y1, X2 = x2, Y2 = y2, Stroke = GroupBorderBrush, StrokeThickness = GroupBorderStrokeThikness };
        //            LatticeCanvas.Children.Add(newLine);
        //        };
        //    }

        //    if (IsGridView)
        //    {
        //        for (int horizontalCount = 0; horizontalCount <= LatticeWidth; horizontalCount++)
        //        {
        //            double x = horizontalCount * scale;
        //            LatticeCanvas.Children.Add(new Line() { X1 = x, Y1 = 0, X2 = x, Y2 = LatticeCanvas.Height, Stroke = Brushes.Black });
        //        }

        //        for (int verticalCount = 0; verticalCount <= LatticeHeight; verticalCount++)
        //        {
        //            double y = verticalCount * scale;
        //            LatticeCanvas.Children.Add(new Line() { X1 = 0, Y1 = y, X2 = LatticeCanvas.Width, Y2 = y, Stroke = Brushes.Black });
        //        }
        //    }
        //}
        //}

        private void newRect_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Rectangle rect = sender as Rectangle;
            if (rect == null) return;

            //rect.Stroke = Brushes.BlueViolet;
            //rect.StrokeThickness = 0.3;
            SqRt sqRt = rect.Tag as SqRt;
            // Fire select this reactor
        }

        private void ShowClusters(object sender, RoutedEventArgs e)
        {
            //ShowClustersButton.IsChecked = !ShowClustersButton.IsChecked;
            UpdateWorkingArea();
        }
    }
}
