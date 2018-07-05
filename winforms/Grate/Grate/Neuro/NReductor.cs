using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Grate.ColorDispersion;

namespace Grate.Neuro
{
    public class NReductor :  IReductorTransmit
    {
        ColorWeight consolidatedColorWeight;

        public ColorWeight ConsolidatedColorWeight
        {
            get
            {
                return consolidatedColorWeight;
            }
        }


        private List<ReductorInput> reductorsList;

        public List<ReductorInput> ReductorsList
        {
            get
            {
                return reductorsList;
            }
        }

        #region IReductorTransmit Members

        public ColorWeight Effect()
        {
            return ColorWeight.BlackNull;
        }
        #endregion

        public ColorWeight Fill()
        {
            ColorWeight cw = ColorWeight.BlackNull;
            ColorWeightSum cws = new ColorWeightSum( cw );
            foreach (ReductorInput ri in reductorsList)
            {
                cws = cws + ri.Impact();
            }
            consolidatedColorWeight = cws / cws.Weight;
            return consolidatedColorWeight;
        }

        public NReductor()
        {
            reductorsList = new List<ReductorInput>();
        }
    }
}
