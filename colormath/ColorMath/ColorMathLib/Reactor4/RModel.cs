using ColorMathLib.Analysis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media.Imaging;

namespace Grate.Reactor4
{
    public class RModel
    {
        private RLayer layer1;
        private RLayer layer2;
        private RLayer layer3;
        private System.Windows.Media.Imaging.BitmapImage bitmapImage;
        private AnalysisManager analysisManager;

        public event RLayer.ShowBitmapDelegate OnShowBitmap;

        public RModel(System.Drawing.Image image)
        {
            layer1 = new RLayer(image);
            layer1.OnShowBitmap += new RLayer.ShowBitmapDelegate(ShowBitmap);
            layer2 = new RLayer(layer1);
            layer2.OnShowBitmap += new RLayer.ShowBitmapDelegate(ShowBitmap);
            layer3 = new RLayer(layer2);
            layer3.OnShowBitmap += new RLayer.ShowBitmapDelegate(ShowBitmap);
        }

        public RModel(System.Windows.Media.Imaging.BitmapImage bitmapImage)
        {
            // TODO: Complete member initialization
            this.bitmapImage = bitmapImage;
            layer1 = new RLayer(bitmapImage);
            layer1.OnShowBitmap += new RLayer.ShowBitmapDelegate(ShowBitmap);
            layer2 = new RLayer(layer1);
            layer2.OnShowBitmap += new RLayer.ShowBitmapDelegate(ShowBitmap);
            layer3 = new RLayer(layer2);
            layer3.OnShowBitmap += new RLayer.ShowBitmapDelegate(ShowBitmap);
            analysisManager = new AnalysisManager();
        }

        public void Process()
        {
            layer1.React();
            layer2.Feel();
            layer1.Select();
            layer2.React();
            analysisManager.RunLatNexViewer("Stage1 - layer1 - layer2", layer1, layer2);
            //layer2.ViewImage();
            layer3.Feel();
            layer2.Select();
            layer3.React();
            //layer3.ViewImage();
            layer3.Slave();
            layer2.Slave();
            layer1.Slave();
            layer1.ViewImageBackReaction();
        }

        public void ShowBitmap( BitmapSource bitmap, String title)
        {
            if (OnShowBitmap != null)
            {
                OnShowBitmap(bitmap, title);
            }
        }
    }
}
