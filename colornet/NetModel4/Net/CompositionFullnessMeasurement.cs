using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Net
{
    public class CompositionFullnessMeasurement
    {
        public static MeasureAction<Model, Composition> compositionFullnessMeasurement = (model, composition) =>
            {
                return 100;
            };

    }
}
