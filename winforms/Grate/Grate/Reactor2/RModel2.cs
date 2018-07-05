using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Grate.Reactor2
{
    public class RModel2
    {
        private RLayer layer1;
        private RLayer layer2;
        private RLayer layer3;
        private RLayer layer4;
        private RLayer layer5;
        private RLayer layer6;
        private RLayer layer7;
        private RLayer layer8;
        private RLayer layer9;
        private RLayer layer10;
        private RLayer layer11;
        private RLayer layer12;
        private RLayer layer13;
        private RLayer layer14;

        public RModel2(System.Drawing.Image image)
        {
            layer1 = new RLayer(image);
            layer2 = new RLayer(layer1);
            layer3 = new RLayer(layer2);
            layer4 = new RLayer(layer3);
            layer5 = new RLayer(layer4);
            layer6 = new RLayer(layer5);
            layer7 = new RLayer(layer6);
            layer8 = new RLayer(layer7);
            layer9 = new RLayer(layer8);
            layer10 = new RLayer(layer9);
            layer11 = new RLayer(layer10);
            layer12 = new RLayer(layer11);
            layer13 = new RLayer(layer12);
            layer14 = new RLayer(layer13);
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

            layer5.Feel();
            layer4.Select();
            layer5.React();

            layer6.Feel();
            layer5.Select();
            layer6.React();

            layer7.Feel();
            layer6.Select();
            layer7.React();

            layer8.Feel();
            layer7.Select();
            layer8.React();

            layer9.Feel();
            layer8.Select();
            layer9.React();

            layer10.Feel();
            layer9.Select();
            layer10.React();

            layer11.Feel();
            layer10.Select();
            layer11.React();

            layer12.Feel();
            layer11.Select();
            layer12.React();

            layer13.Feel();
            layer12.Select();
            layer13.React();

            layer14.Feel();
            layer13.Select();
            layer14.React();

            layer14.Slave();            
            layer13.Slave();
            layer12.Slave();                        
            layer11.Slave();            
            layer10.Slave();
            layer9.Slave();           
            layer8.Slave();
            layer7.Slave();
            layer6.Slave();
            layer5.Slave();
            layer4.Slave();
            layer3.Slave();
            layer2.Slave();
            layer1.Slave();
            layer1.ViewImageBackReaction();
        }
    }
}
