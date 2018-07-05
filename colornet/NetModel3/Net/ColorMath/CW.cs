using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Net.ColorMath
{
    public static class CW
    {
        public static float GetModule(this Color color)
        {
            return (float)Math.Sqrt((double)(color.ScA * color.ScA + color.ScB * color.ScB + color.ScG * color.ScG + color.ScR * color.ScR));
        }

        public static double Delta(this Color cw1, Color cw2)
        {
            int R = (cw1.R - cw2.R);
            int G = (cw1.G - cw2.G);
            int B = (cw1.B - cw2.B);
            return Math.Sqrt((double)(R * R + G * G + B * B));
        }

        // Get color from double value
        public static Color GetColor(double m)
        {
            byte v = (byte)(m * 500);
            return new Color() { R = v, G = v, B = v, A = 255 }; 
        }

        public static bool IsMore(this Color color, Color otherColor)
        {
            return color.ToColor().ToArgb() > otherColor.ToColor().ToArgb();
        }

        public static System.Windows.Media.Color ToColor(this System.Drawing.Color color) => System.Windows.Media.Color.FromArgb(color.A, color.R, color.G, color.B);
        public static System.Drawing.Color ToColor(this System.Windows.Media.Color color) => System.Drawing.Color.FromArgb(color.A, color.R, color.G, color.B);

    }
}
