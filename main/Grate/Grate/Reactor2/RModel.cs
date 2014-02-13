using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Grate.Reactor2
{
    public class RModel
    {
        private RLayer layer1;
        private RLayer layer2;
        private RLayer layer3;
        private RLayer layer4;

        public RModel(System.Drawing.Image image)
        {
            layer1 = new RLayer(image);
            layer2 = new RLayer(layer1);
            layer3 = new RLayer(layer2);
            layer4 = new RLayer(layer3);
        }

        public void Process()
        {
            layer1.React();
            layer2.Feel();
            layer1.Select();
            layer2.React();
            //layer2.ViewImage();
            layer3.Feel();
            layer2.Select();
            layer3.React();
            //layer3.ViewImage();
            layer4.Feel();
            layer3.Select();
            layer4.React();
            layer4.Slave();
            layer3.Slave();
            layer2.Slave();
            layer1.Slave();
            layer1.ViewImageBackReaction();
        }
    }
}
