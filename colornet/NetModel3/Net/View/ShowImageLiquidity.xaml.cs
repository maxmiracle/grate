
using Net.NetBase;
using NetBase;
using System.Drawing.Imaging;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Net.View
{
    /// <summary>
    /// Interaction logic for ShowImage.xaml
    /// </summary>
    public partial class ShowImageLiquidity
        : Window
    {
        private SqImgLayer layer;
        private Rectangle[,] rectPixels;
        private ImgComposition imgComposition;
        private SolidColorBrush borderGroupBrush = new SolidColorBrush(Colors.BlueViolet);

        public ShowImageLiquidity()
        {
            InitializeComponent();
        }

        public ShowImageLiquidity(SqImgLayer layer, ImgComposition imgComposition) : this()
        {

            this.layer = layer;
            this.imgComposition = imgComposition;
            UpdateWorkingArea();

        }

        private void UpdateWorkingArea()
        {
            var LatticeHeight = layer.Height;
            var LatticeWidth = layer.Width;
            double scale = 2;
            LatticeCanvas.Width = LatticeWidth * scale;
            LatticeCanvas.Height = LatticeHeight * scale;
            rectPixels = new Rectangle[LatticeWidth, LatticeHeight];
            LatticeCanvas.Children.Clear();

            // Show input color
            foreach (SqRt sqRt in layer)
            {
                Color color = sqRt.Input;
                Rectangle newRect = new Rectangle() { Fill = new SolidColorBrush(color), StrokeThickness = 0, Height = scale, Width = scale, Tag = sqRt, DataContext = sqRt };
                Canvas.SetLeft(newRect, sqRt.X * scale);
                Canvas.SetTop(newRect, sqRt.Y * scale);
                LatticeCanvas.Children.Add(newRect);
                rectPixels[sqRt.X, sqRt.Y] = newRect;
                var liquidityGroupMaster = sqRt.LiquidityGroupMaster;
                foreach( var nel in sqRt.nels)
                {
                    if ( nel.Value.LiquidityGroupMaster != liquidityGroupMaster )
                    {
                        var xc = nel.Key.X;
                        var yc = nel.Key.Y;
                        LatticeCanvas.Children.Add(
                            new Line()
                            {
                                Stroke = borderGroupBrush,
                                X1 = (sqRt.X + 0.5 + (0.5) * xc  + (0.5) * yc) * scale,
                                Y1 = (sqRt.Y + 0.5 + (0.5) * yc - (0.5) * xc) * scale,
                                X2 = (sqRt.X + 0.5 + (0.5) * xc - (0.5) * yc) *scale,
                                Y2 = (sqRt.Y + 0.5 + (0.5) * yc + (0.5) * xc) *scale,
                                StrokeThickness = 0.5
                            });
                    }
                }
            }


        }
    }
}
