using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Grate.Reactor4
{
    public class ReactorTool
    {
        private RModel model;

        public event RLayer.ShowBitmapDelegate OnShowBitmap;

        public void RunOn(Image image)
        {
            model = new RModel(image);
            model.Process();
        }

        public void RunOn(System.Windows.Media.Imaging.BitmapImage bitmapImage)
        {
            model = new RModel(bitmapImage);
            model.OnShowBitmap += new RLayer.ShowBitmapDelegate(model_OnShowBitmap);
            model.Process();
        }

        void model_OnShowBitmap(System.Windows.Media.Imaging.BitmapSource bitmap, string title)
        {
            if (OnShowBitmap != null)
            {
                OnShowBitmap(bitmap, title);
            }
        }

        
    }
}
