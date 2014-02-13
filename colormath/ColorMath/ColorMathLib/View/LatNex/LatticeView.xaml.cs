using Grate;
using Grate.Reactor4;
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

namespace ColorMathLib.View.LatNex
{
    /// <summary>
    /// Interaction logic for LatticeView.xaml
    /// </summary>
    public partial class LatticeView : UserControl
    {
        public LatticeView()
        {
            InitializeComponent();
            LatticeHeight = 50;
            LatticeWidth = 50;
        }

        public LatticeView ( RLayer layer ) : this ()
        {
            this.InitializeLatticeView(layer);
        }

        public int LatticeWidth { get; set; }
        public int LatticeHeight { get; set; }

        private RLayer layer;
        private Lattice lattice;

        public void InitializeLatticeView(RLayer layer)
        {
            this.layer = layer;
            LatticeWidth = layer.Lattice.Width;
            LatticeHeight = layer.Lattice.Height;
            this.lattice = layer.Lattice;
            UpdateWorkingArea();
        }

        private void ScaleChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {

            //UpdateWorkingArea();
            
        }

        public static double GridViewScaleValue = 3;

        public bool IsGridView { get; set; }

        private Rectangle[,] rectPixels;

        private void UpdateWorkingArea()
        {
            double scale = 2;
            LatticeCanvas.Width = LatticeWidth * scale;
            LatticeCanvas.Height = LatticeHeight * scale;
            rectPixels = new Rectangle[LatticeWidth,LatticeHeight];
            IsGridView = scale >= GridViewScaleValue;
            LatticeCanvas.Children.Clear();

            // Show color of reactor
                foreach (object obj in lattice)
                {
                    SquareBaxin sqbaxin = (obj as SquareBaxin);
                    Reactor reactor = sqbaxin.Tag as Reactor;
                    Color color = reactor.Color;
                    int sq_x = sqbaxin.X;
                    int sq_y = sqbaxin.Y;
                    Rectangle newRect = new Rectangle() { Fill = new SolidColorBrush(color), StrokeThickness = 0, Height = scale, Width = scale, Tag = sqbaxin };
                    newRect.MouseLeftButtonDown += newRect_MouseLeftButtonDown;
                    Canvas.SetLeft(newRect, sq_x * scale);
                    Canvas.SetTop(newRect, sq_y * scale);
                    LatticeCanvas.Children.Add(newRect);
                    rectPixels[sq_x, sq_y] = newRect;
                }

            if (IsGridView)
            {
                for (int horizontalCount = 0; horizontalCount <= LatticeWidth; horizontalCount++)
                {
                    double x = horizontalCount * scale;
                    LatticeCanvas.Children.Add(new Line() { X1 = x, Y1 = 0, X2 = x, Y2 = LatticeCanvas.Height, Stroke = Brushes.Black });
                }

                for (int verticalCount = 0; verticalCount <= LatticeHeight; verticalCount++)
                {
                    double y = verticalCount * scale;
                    LatticeCanvas.Children.Add(new Line() { X1 = 0, Y1 = y, X2 = LatticeCanvas.Width, Y2 = y, Stroke = Brushes.Black });
                }
            }
            if (lattice == null) return;



        }

        void newRect_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Rectangle rect = sender as Rectangle;
            if (rect == null) return;
            rect.Stroke = Brushes.BlueViolet;
            rect.StrokeThickness = 0.3;
            SquareBaxin sqbaxin = (rect.Tag as SquareBaxin);
            Reactor reactor = sqbaxin.Tag as Reactor;
            SelectReactor(reactor, sqbaxin);
        }

        public event OnSelectReactorDelegate OnSelectReactor;

        private void SelectReactor( Reactor selectedReactor, SquareBaxin selectedSquareBaxin)
        {
            if (OnSelectReactor != null)
            {
                OnSelectReactor(this, new SelectReactorEventArgs() { SelectedReactor = selectedReactor, SelectedSquareBaxin = selectedSquareBaxin });
            }
        }

        public delegate void OnSelectReactorDelegate(object sender, SelectReactorEventArgs args);


        internal void ShowConnectedToNextLayerReactor(SquareBaxin NextSquareBaxin)
        {
            Reactor reactor = NextSquareBaxin.Tag as Reactor;
            foreach (var vals in reactor.Selectors)
            {
                Reactor keyReactor = vals.Key;
                Synapse syn = vals.Value;
                var baxin = keyReactor.Parent as SquareBaxin;
                rectPixels[baxin.X, baxin.Y].Stroke = Brushes.Aqua;
                rectPixels[baxin.X, baxin.Y].StrokeThickness = 0.3;
            }
        }
    }

    public class SelectReactorEventArgs : EventArgs
    {
        public Reactor SelectedReactor { get; set; }
        public SquareBaxin SelectedSquareBaxin { get; set; }
    }
}
