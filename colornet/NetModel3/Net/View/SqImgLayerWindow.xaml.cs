using System;
using System.Collections.Generic;
using System.Drawing;
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

namespace NetBase.View
{
    /// <summary>
    /// Interaction logic for SqImgLayerWindow.xaml
    /// </summary>
    public partial class SqImgLayerWindow : Window
    {
        private SqImgLayer layer;

        public SqImgLayerWindow()
        {
            InitializeComponent();
        }

        public SqImgLayerWindow(SqImgLayer layer) : this()
        {
            // TODO: Complete member initialization
            this.layer = layer;
            layerView.Layer = layer;
        }
    }
}
