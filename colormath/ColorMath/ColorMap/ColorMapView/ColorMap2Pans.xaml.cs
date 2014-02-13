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
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class ColorMap2Pans : UserControl
    {
        public ColorMap2Pans()
        {
            InitializeComponent();

        }

        void ColorPoints_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            UpdateBoxChangeNofiers();
            DrawPoints();
        }

        public ObservableCollection<ColorBox> ColorPoints
        {
            get { return (ObservableCollection<ColorBox>)GetValue(ColorPointsProperty); }
            set
            {
                SetValue(ColorPointsProperty, value);
            }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ColorPointsProperty =
            DependencyProperty.Register("ColorPoints", typeof(ObservableCollection<ColorBox>), typeof(ColorMap2Pans), new UIPropertyMetadata(null, OnColorPointsChanged));

        public static void OnColorPointsChanged( DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ColorMap2Pans userControl = d as ColorMap2Pans;
            ObservableCollection<ColorBox> newCollection = e.NewValue as ObservableCollection<ColorBox>;
            if (( userControl != null) && (newCollection != null ))
            {
                userControl.DrawPoints();
                userControl.UpdateBoxChangeNofiers();
                newCollection.CollectionChanged += new System.Collections.Specialized.NotifyCollectionChangedEventHandler(userControl.ColorPoints_CollectionChanged);
            }
        }

        private void UpdateBoxChangeNofiers()
        {
            foreach (var color in ColorPoints)
            {
                color.PropertyChanged += color_PropertyChanged;
            }
             
        }

        void color_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            DrawPoints();
        }

        private void DrawPoints()
        {
            SolidColorBrush strokeBrushEasy = new SolidColorBrush(new Color(){ A = 60, R = 0, G = 0, B = 0} );
            SolidColorBrush strokeBrushSelected = new SolidColorBrush(new Color() { A = 250, R = 0, G = 0, B = 0 });
            
            colorMap.Children.Clear();
            foreach (var color in ColorPoints)
            {
                SolidColorBrush strokeBrush;
                if ( color.IsSelected )
                {
                    strokeBrush = strokeBrushSelected;
                }
                else
                {
                    strokeBrush = strokeBrushEasy;
                }
                double radius = 10;
                double rad_corr = radius/2;
                double axe = 255;
                var el_p1 = new Ellipse() { Width = radius, Height = radius, Fill = new SolidColorBrush(color.Color), Stroke = strokeBrush};
                var el_p2 = new Ellipse() { Width = radius, Height = radius, Fill = new SolidColorBrush(color.Color), Stroke = strokeBrush };
                double c1 = color.Color.R;
                double c2 = color.Color.G;
                double c3 = color.Color.B;
                Canvas.SetLeft(el_p1, c1  );
                Canvas.SetTop(el_p1, axe - c2);
                Canvas.SetLeft(el_p2, c1);
                Canvas.SetTop(el_p2, axe + c3);
                colorMap.Children.Add(el_p1);
                colorMap.Children.Add(el_p2);
                var line = new Line() { X1 = c1 + rad_corr, Y1 = axe - c2 + radius, X2 = c1 + rad_corr, Y2 = axe + c3, Stroke = strokeBrush };
                colorMap.Children.Add(line);
            }
        }


    }
}
