using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetBase
{
    public struct XY
    {
        public XY( int x, int y)
        {
            X = x;
            Y = y;
        }

        public int X;
        public int Y;
        public static XY XYLeft = new XY() { X = -1, Y = 0 };
        public static XY XYRight = new XY() { X = 1, Y = 0 };
        public static XY XYUp = new XY() { X = 0, Y = -1 };
        public static XY XYDown = new XY() { X = 0, Y = 1 };
        public static XY XYSelf = new XY() { X = 0, Y = 0 };

        public static XY operator !(XY xy)
        {
            return new XY(xy.X * (-1), xy.Y * (-1));
        }

        public static bool operator !=(XY xy1, XY xy2)
        {
            if (( xy1.X == xy2.X) && ( xy1.Y == xy2.Y))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public static bool operator ==(XY xy1, XY xy2)
        {
            if ((xy1.X == xy2.X) && (xy1.Y == xy2.Y))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static XY operator *(XY xy1, int c)
        {
            return new XY(xy1.X * c, xy1.Y * c);
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
