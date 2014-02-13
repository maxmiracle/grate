using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Grate.Reactor4
{
    public class RModel
    {
        private RLayer layer1;
        private RLayer layer2;
        private RLayer layer3;

        public RModel(System.Drawing.Image image)
        {
            layer1 = new RLayer(image);
            layer2 = new RLayer(layer1);
            layer3 = new RLayer(layer2);
        }

        public void Process()
        {
            layer1.React();
            layer2.Feel();
            layer1.Select();
            layer2.React();
            layer2.ViewImage();
            layer3.Feel();
            layer2.Select();
            layer3.React();
            layer3.ViewImage();
            layer3.Slave();
            layer2.Slave();
            layer1.Slave();
            layer1.ViewImageBackReaction();
        }
    }
}
