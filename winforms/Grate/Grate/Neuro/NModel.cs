using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Grate.Neuro
{
    public class NModel
    {
        private NSelectorLayer layer1;
        private NReductorLayer layer2;

        public NModel(System.Drawing.Image image)
        {
            layer1 = new NSelectorLayer(image);
            layer2 = new NReductorLayer(layer1);
        }

        public void Process1()
        {
            layer1.Affect();
            layer2.Fill();
            layer2.ViewImage();
        }
    }
}
