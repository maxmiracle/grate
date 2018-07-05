using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetBase;
using NetBase.View;

namespace Net.StartUp
{
    public class Program
    {
        [System.STAThreadAttribute()]
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public static void Main()
        {
            SqImgLayer layer = SqImgLayer.GetLayer();
            SqImgLayerWindow window = new SqImgLayerWindow(layer);
            window.ShowDialog();
        }


    }
}
