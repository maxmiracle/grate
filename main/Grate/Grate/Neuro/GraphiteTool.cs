using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Grate.Neuro;

namespace Grate
{
    class GraphiteTool
    {
        private NModel model;

        internal void RunOn(System.Drawing.Image image)
        {
            model = new NModel(image);
            model.Process1();
        }
    }
}
