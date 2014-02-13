using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Grate.Neuro
{
    public class NSelector
    {
        private Color color;

        public Color Color
        {
            set
            {
                color = value;
            }
            get
            {
                return color;
            }
        }

        public NSelector(Color color)
        {
            this.color = color;
            reductors = new List<IReductorInput>();
        }

        private List<IReductorInput> reductors;
        public List<IReductorInput> Reductors
        {
            get
            {
                return reductors;
            }
        }
    }
}
