using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Grate.Reactor
{
    public class ReactorTool
    {
        private RModel model;

        internal void RunOn(System.Drawing.Image image)
        {
            model = new RModel(image);
            model.Process();
        }
    }
}
