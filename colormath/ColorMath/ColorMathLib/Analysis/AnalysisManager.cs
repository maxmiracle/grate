using ColorMathLib.View.LatNex;
using Grate.Reactor4;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ColorMathLib.Analysis
{
    public class AnalysisManager
    {
        public void RunLatNexViewer(string title, RLayer layer, RLayer layerNext)
        {
            LatNexAnalysis win = new LatNexAnalysis(layer, layerNext, title);
            win.ShowDialog();
        }
    }
}
