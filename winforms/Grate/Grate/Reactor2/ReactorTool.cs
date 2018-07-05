using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Grate.Reactor2
{
    public class ReactorTool2
    {
        private RModel2 model;

        internal void RunOn(System.Drawing.Image image)
        {
            model = new RModel2(image);
            model.Process();
        }
    }
}
