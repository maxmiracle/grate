using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Net.NetBase
{
    public class NeuroMath
    {
        const double SAME_COLOR_FRONT = 2;
        const double SCF_A = 1 / (-1 * SAME_COLOR_FRONT);

        public static double LimitedImpedance(double colorModuleDiff)
        {
            double Nx;
            if (colorModuleDiff < SAME_COLOR_FRONT)
            {
                Nx = colorModuleDiff * SCF_A + 1;
            }
            else
            {
                Nx = 0.001;
            }
            return Nx;
        }

        const double GAIN_FRONT = 0.90;
        const double NULL_GAIN = 0.000001;
        internal static double GainBackFront(double S1)
        {
            if ( S1 < GAIN_FRONT )
            {
                return S1/GAIN_FRONT;
            }
            else
            {
                return NULL_GAIN;
            }
        }

        internal static double GainTo1(double x)
        {
            if (x < 0) return 0;
            return 10*x / (10*x + 1);
        }
    }
}
