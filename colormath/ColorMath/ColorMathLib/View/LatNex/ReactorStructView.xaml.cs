using Grate;
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
    /// Interaction logic for ReactorStructView.xaml
    /// </summary>
    public partial class ReactorStructView : UserControl
    {

        public Reactor ReactorObject
        {
            get { return (Reactor)GetValue(ReactorObjectProperty); }
            set { SetValue(ReactorObjectProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ReactorObject.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ReactorObjectProperty =
            DependencyProperty.Register("ReactorObject", typeof(Reactor), typeof(ReactorStructView), new PropertyMetadata(null, OnChangedReactorObject));

        private static void OnChangedReactorObject(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ReactorStructView rsv = d as ReactorStructView;
            if (rsv == null) return;
            rsv.UpdateReactorObjectView(e.NewValue as Reactor);
        }

        private void UpdateReactorObjectView(Reactor reactor)
        {
            reactorTree.Items.Clear();

            SquareBaxin sqBaxin = reactor.Parent as SquareBaxin;
            StackPanel stackPanel = new StackPanel();
            stackPanel.Children.Add(new Rectangle() { Width = 20, Height = 20, Fill = new SolidColorBrush(reactor.Color)});
            stackPanel.Children.Add(new TextBlock() { Text = String.Format( " X: {0}, Y: {1} ", sqBaxin.X, sqBaxin.Y)});
            TreeViewItem reactorRootItem = new TreeViewItem() { Header = stackPanel };

            foreach (var vals in reactor.Selectors)
            {
                Reactor keyReactor = vals.Key;
                Synapse syn = vals.Value;
                var baxin = keyReactor.Parent as SquareBaxin;

                StackPanel stackPanelChild = new StackPanel();
                stackPanelChild.Children.Add(new Rectangle() { Width = 20, Height = 20, Fill = new SolidColorBrush(keyReactor.Color) });
                stackPanelChild.Children.Add(new TextBlock() { Text = String.Format(" X: {0}, Y: {1} ", baxin.X, baxin.Y) });
                stackPanelChild.Children.Add(new Rectangle() { Width = 20, Height = 20, Fill = new SolidColorBrush(syn.reaction.Color) });
                stackPanelChild.Children.Add(new TextBlock() { Text = String.Format(" InGame: {0} ", syn.selection.InGame) });
                stackPanelChild.Children.Add(new Rectangle() { Width = 20, Height = 20, Fill = new SolidColorBrush(syn.feeling.Color) });
                reactorRootItem.Items.Add( new TreeViewItem() { Header = stackPanelChild });

            }


            reactorTree.Items.Add(reactorRootItem);
        }

        public ReactorStructView()
        {
            InitializeComponent();
        }
    }
}
