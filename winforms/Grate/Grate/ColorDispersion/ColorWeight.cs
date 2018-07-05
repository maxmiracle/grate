using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Grate.ColorDispersion
{
    /// <summary>
    /// class ColorWeight
    /// Color with sense of power of the color besides others
    /// </summary>
    public class ColorWeight
    {
        public static ColorWeight BlackNull = new ColorWeight(Color.Black, 0);
        public static double MaxDelta = Delta(BlackNull, new ColorWeight(Color.White, 1));

        protected ColorWeight()
        {
        }

        public ColorWeight(Color color, double weight)
        {
            this.weight = weight;
            this.color = color;
        }

        public ColorWeight(ColorWeight cw)
        {
            this.weight = cw.Weight;
            this.color = cw.Color;
        }

        private double weight;
        public double Weight
        {
            get { return weight; }
            set { weight = value; }
        }

        private Color color;
        public Color Color
        {
            get { return color; }
            set { color = value; }
        }

        private Object tag;
        public Object Tag
        {
            get { return tag; }
            set { tag = value; }
        }

        public static ColorWeightSum operator +(ColorWeight cw1, ColorWeight cw2)
        {
            return new ColorWeightSum(cw1) + cw2;
        }

        public static double Delta(ColorWeight cw1, ColorWeight cw2)
        {
            int R = (cw1.color.R - cw2.color.R);
            int G = (cw1.color.G - cw2.color.G);
            int B = (cw1.color.B - cw2.color.B);
            return Math.Sqrt((double)(R * R + G * G + B * B));
        }

        public static ColorWeight Average(params ColorWeight[] colors)
        {
            ColorWeightSum sum = ColorWeightSum.BlackNull;
            foreach (ColorWeight cw in colors)
            {
                sum = sum + cw;
            }
            return sum / sum.Weight;
        }

        public static ColorWeight Sum(params ColorWeight[] colors)
        {
            ColorWeightSum sum = ColorWeightSum.BlackNull;
            foreach (ColorWeight cw in colors)
            {
                sum = sum + cw;
            }
            if (sum.Weight != 0)
            {
                ColorWeight res = sum / sum.Weight;
                res.Weight = sum.Weight;
                return res;
            }
            else
            {
                return ColorWeight.BlackNull;
            }
        }

    }


    public class ColorWeightSum
    {
        double r;
        double g;
        double b;
        double weight;

        public static ColorWeightSum BlackNull = new ColorWeightSum(ColorWeight.BlackNull);

        public double Weight
        {
            get
            {
                return weight;
            }
        }

        public ColorWeightSum(ColorWeight cw)
        {
            r = (double)(cw.Color.R) * cw.Weight;
            g = (double)(cw.Color.G) * cw.Weight;
            b = (double)(cw.Color.B) * cw.Weight;
        }

        public ColorWeightSum(ColorWeightSum cws)
        {
            r = cws.r;
            g = cws.g;
            b = cws.b;
            weight = cws.weight;
        }

        public double Norma()
        {
            return Math.Sqrt(r * r + g * g + b * b);
        }

        public ColorWeight Average()
        {
            if (this.Weight != 0)
            {
                return this / this.Weight;
            }
            else
            {
                return ColorWeight.BlackNull;
            }
        }

        public static ColorWeightSum operator +(ColorWeightSum cws, ColorWeight cw)
        {
            ColorWeightSum cws_ret = new ColorWeightSum(cws);
            cws_ret.r += (double)(cw.Color.R) * cw.Weight;
            cws_ret.g += (double)(cw.Color.G) * cw.Weight;
            cws_ret.b += (double)(cw.Color.B) * cw.Weight;
            cws_ret.weight += cw.Weight;
            return cws_ret;
        }

        public static ColorWeightSum operator -(ColorWeightSum cws, ColorWeight cw)
        {
            ColorWeightSum cws_ret = new ColorWeightSum(cws);
            cws_ret.r -= (double)(cw.Color.R) * cw.Weight;
            cws_ret.g -= (double)(cw.Color.G) * cw.Weight;
            cws_ret.b -= (double)(cw.Color.B) * cw.Weight;
            cws_ret.weight -= cw.Weight;
            return cws_ret;
        }


        public static ColorWeight operator /(ColorWeightSum cws, double weight)
        {
            Color color = Color.FromArgb((int)(cws.r / weight), (int)(cws.g / weight), (int)(cws.b / weight));
            return new ColorWeight(color, cws.weight / weight);
        }
    }

}
