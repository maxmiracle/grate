using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using Grate.ColorDispersion;

namespace Grate.Neuro
{
    public interface IReductorInput
    {
        void Affect(Color impact);
        ColorWeight Fill();
        ColorWeight Impact();
    }
}
