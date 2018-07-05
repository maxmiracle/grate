using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Grate.ColorDispersion
{
    public class ColorElement : ColorWeight
    {
        public ColorGroup Parent { set; get; }

        public ColorElement()
        {
        }

        public ColorElement(ColorWeight m)
        {
            Color = m.Color;
            Weight = m.Weight;
            Parent = null;
        }
    }

}
