using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ColorMathLib.Reactor4
{
    public class ModelContext
    {
        public System.Windows.Media.Imaging.BitmapImage Image { get; set; }

        public Grate.Reactor4.ReactorTool Tool { get; set; }

        public void Run()
        {
            Tool.RunOn(Image);
        }
    }
}
