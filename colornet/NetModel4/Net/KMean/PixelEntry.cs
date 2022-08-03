using Microsoft.ML.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Net.KMean
{
    public class PixelEntry
    {
        [VectorType(3)]
        public float[] Features { get; set; }
    }
}
