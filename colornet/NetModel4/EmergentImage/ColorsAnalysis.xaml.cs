using Net.KMean;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
    /// Interaction logic for ColorsAnalysis.xaml
    /// </summary>
    public partial class ColorsAnalysis : UserControl
    {
        public ColorsAnalysis()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty ColorStatisticProperty =
           DependencyProperty.Register("ColorStatistic", typeof(List<ColorStatistic>), typeof(ColorsAnalysis), new
              PropertyMetadata(new List<ColorStatistic>(), new PropertyChangedCallback(OnColorStatisticChanged)));

        public List<ColorStatistic> ColorStatistic
        {
            get { return (List<ColorStatistic>)GetValue(ColorStatisticProperty); }
            set { SetValue(ColorStatisticProperty, value); }
        }

        private static void OnColorStatisticChanged(DependencyObject d,
           DependencyPropertyChangedEventArgs e)
        {
            ColorsAnalysis control = d as ColorsAnalysis;
            control.OnColorStatisticChanged(e);
        }

        private void OnColorStatisticChanged(DependencyPropertyChangedEventArgs e)
        {
            List<ColorStatistic> newColorStatistic = (List<ColorStatistic>)e.NewValue;
            if (newColorStatistic != null)
            {
                colorAnalysisPie.ColorSource = newColorStatistic.Select(item => item.Color);
                colorAnalysisPie.ItemsSource = newColorStatistic.Select(item => item.Percent);
            }
            else
            {
                colorAnalysisPie.ItemsSource = new Double[1] { 1 };
            }
            //tbTest.Text = e.NewValue.ToString();
        }
    }
}
