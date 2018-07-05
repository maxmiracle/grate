using System;
using System.Windows;

namespace InternalViewerLib
{
    /// <summary>
    /// Interaction logic for InternalViewerWindow.xaml
    /// </summary>
    public partial class InternalViewerWindow : Window
    {
        public InternalViewerWindow()
        {
            InitializeComponent();
        }

        public void AddObject(String name, Object objectContext)
        {
            this.ObjectViewerControl.AddObject(name, objectContext);
        }

    }
}
