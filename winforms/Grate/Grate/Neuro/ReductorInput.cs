using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using Grate.ColorDispersion;

namespace Grate.Neuro
{
    public class ReductorInput : IReductorInput
    {
        Color impact;
        double weight;

        public double Weight
        {
            set
            {
                weight = value;
            }
            get
            {
                return weight;
            }
        }

        public ReductorInput(double weight)
        {
            this.weight = weight;
        }

        #region IReductorInput Members

        public void Affect(Color impact)
        {
            this.impact = impact;
        }

        public ColorWeight Fill()
        {
            return ColorWeight.BlackNull;
        }

        #endregion

        #region IReductorInput Members


        public ColorWeight Impact()
        {
            return new ColorWeight(impact, weight);
        }

        #endregion
    }
}
