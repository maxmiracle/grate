using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Net
{
    public class ThresholdAssessment //: Assessment
    {
        public double Threshold { get; set; }

        public bool Assess(double criterionValue)
        {
            return criterionValue >= Threshold;
        }
    }
}
