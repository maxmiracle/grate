using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetBase;

namespace Net.NetBase
{
    public class ElementRingOverLayerIterator : IEnumerator<SqRt>, IEnumerable<SqRt>
    {
        int x;
        int y;
        double radius;
        SqImgLayer layer;

        struct pnt
        {
            pnt(byte dx, byte dy)
            {
                this.dx = dx;
                this.dy = dy;
            }

            pnt(byte[] dxdy)
            {
                this.dx = dxdy[0];
                this.dy = dxdy[1];
            }
            public byte dx;
            public byte dy;
        }

        static int[][][] rings = new int[][][]
        {
            new int[][] { new int[] {0, 0} },

            new int[][] { new int[] {1,-1}, new int[] {1, 0}, new int[] {1,1 }, new int[] {0, 1},new int[]  {-1,1}, new int[] {-1,0}, new int[] {-1, -1}, new int[] {0, -1} },

            new int[][] { new int[] {2,-1}, new int[] { 2, 0}, new int[] { 2,  1},  new int[] { 1, 2}, new int[] {0, 2}, new int[] {-1,  2},
                          new int[] {-2,1}, new int[] {-2, 0}, new int[] {-2, -1},  new int[] {-1,-2}, new int[] {0,-2}, new int[] { 1, -2}  },

            new int[][] { new int[] {3, -1},  new int[] {3, 0}, new int[] {3,1   }, new int[] {2,  2}, 
                          new int[] {1,  3},  new int[] {0, 3}, new int[] {-1,3  }, new int[] {-2, 2}, 
                          new int[] {-3, 1},  new int[] {-3,0}, new int[] {-3,-1 }, new int[] {-2,-2},
                          new int[] {-1,-3},  new int[] {0,-3}, new int[] { 1, -3}, new int[] {2, -2} } 
        };

        int currentRing = 0;
        int currentElementInRing = -1;

        public ElementRingOverLayerIterator(SqImgLayer layer, int x, int y, int radius)
        {
            this.layer = layer;
            this.x = x;
            this.y = y;
            this.radius = radius;
            if ( radius > 3 )
            {
                throw new Exception("radius parameter > 2 is not supported");
            }
        }

        public SqRt Current
        {
            get {
                int[] pntCurrent = rings[currentRing][currentElementInRing];
                return layer[x + pntCurrent[0], y + pntCurrent[1]];
            }
        }

        public void Dispose()
        {
            
        }

        object System.Collections.IEnumerator.Current
        {
            get {
                return this.Current;
                 }
        }

        private bool moveNextInternal()
        {
            if (currentElementInRing + 1 < rings[currentRing].Length)
            {
                currentElementInRing += 1;
                return true;
            }
            else if (currentRing < radius)
            {
                currentRing += 1;
                currentElementInRing = 0;
                return true; 
            }
            else
            {
                return false;
            }
        }


        public bool MoveNext()
        {
            bool isNext;
            int[] pntCurrent;
            // Additionaly check field borders
            do
            {
                isNext = moveNextInternal();
                pntCurrent = rings[currentRing][currentElementInRing];
            } while (isNext && (!layer.ValidateXY(x + pntCurrent[0], y + pntCurrent[1])));
            return isNext;
        }

        public void Reset()
        {
            currentRing = 0;
            currentElementInRing = -1;
        }

        public IEnumerator<SqRt> GetEnumerator()
        {
            return this;
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return this;
        }
    }
}
