using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Net.ColorMath
{
    [DebuggerDisplay("{a},{r},{g},{b},{isUndefined}")]
    public struct ColorVector
    {
        public ColorVector(System.Windows.Media.Color color)
        {
            a = color.A;
            r = color.R;
            g = color.G;
            b = color.B;
            isUndefined = false;
        }

        public ColorVector(System.Drawing.Color color)
        {
            a = color.A;
            r = color.R;
            g = color.G;
            b = color.B;
            isUndefined = false;
        }

        public ColorVector(bool isUndefined)
        {
            a = 0;
            r = 0;
            g = 0;
            b = 0;
            this.isUndefined = isUndefined;
        }


        public int a;
        public int r;
        public int g;
        public int b;
        public Boolean isUndefined;



        public static ColorVector operator +(ColorVector cv1, ColorVector cv2)
        {
            ColorVector cv3;
            cv3.a = cv1.a + cv2.a;
            cv3.r = cv1.r + cv2.r;
            cv3.g = cv1.g + cv2.g;
            cv3.b = cv1.b + cv2.b;
            cv3.isUndefined = cv1.isUndefined || cv2.isUndefined;
            return cv3;
        }

        public static ColorVector operator -(ColorVector cv1, ColorVector cv2)
        {
            ColorVector cv3;
            cv3.a = cv1.a - cv2.a;
            cv3.r = cv1.r - cv2.r;
            cv3.g = cv1.g - cv2.g;
            cv3.b = cv1.b - cv2.b;
            cv3.isUndefined = cv1.isUndefined || cv2.isUndefined;
            return cv3;
        }

        public static ColorVector operator /(ColorVector cv1, double d)
        {
            ColorVector cv3;
            cv3.a = (int)Math.Round(cv1.a / d);
            cv3.r = (int)Math.Round(cv1.r / d);
            cv3.g = (int)Math.Round(cv1.g / d);
            cv3.b = (int)Math.Round(cv1.b / d);
            cv3.isUndefined = cv1.isUndefined;
            return cv3;
        }

        public static ColorVector operator *(ColorVector cv1, double d)
        {
            ColorVector cv3;
            cv3.a = (int)Math.Round(cv1.a * d);
            cv3.r = (int)Math.Round(cv1.r * d);
            cv3.g = (int)Math.Round(cv1.g * d);
            cv3.b = (int)Math.Round(cv1.b * d);
            cv3.isUndefined = cv1.isUndefined;
            return cv3;
        }

        public double GetModule()
        {
            if (isUndefined) return double.MaxValue;
            return Math.Sqrt(a * a + r * r + g * g + b * b);
        }

        public override string ToString()
        {
            return String.Format("{0},{1},{2},{3},{4}", a, r, g, b, isUndefined);
        }

        public static implicit operator ColorVector(System.Windows.Media.Color color)
        {
            return new ColorVector(color);
        }

        public static implicit operator ColorVector(System.Drawing.Color color)
        {
            return new ColorVector(color);
        }

        public static ColorVector Undefined = new ColorVector(true);
    }
}
