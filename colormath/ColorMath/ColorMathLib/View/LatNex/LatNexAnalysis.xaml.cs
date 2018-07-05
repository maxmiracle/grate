using Grate.Reactor4;
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

namespace ColorMathLib.View.LatNex
{
    /// <summary>
    /// Interaction logic for LatNexAnalysis.xaml
    /// </summary>
    public partial class LatNexAnalysis : Window
    {
        public LatNexAnalysis()
        {
            InitializeComponent();
        }

        public LatNexAnalysis(RLayer layer1, RLayer layer2, string title) : this()
        {
            this.Title = title;
            LatticeLayer1.InitializeLatticeView(layer1);
            
            LatticeLayer2.InitializeLatticeView(layer2);
            LatticeLayer2.OnSelectReactor += LatticeLayer2_OnSelectReactor;
        }

        void LatticeLayer2_OnSelectReactor(object sender, SelectReactorEventArgs args)
        {
            if (args == null) return;
            reactorStructView.ReactorObject = args.SelectedReactor;
            LatticeLayer1.ShowConnectedToNextLayerReactor(args.SelectedSquareBaxin);
        }
    }
}
