using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace Grate
{
    public class Lattice : ILattice
    {
        ISquareBaxin[][] lat;

        private int height;
        private int width;

        public Lattice(int width, int height)
        {
            lat = new SquareBaxin[width][];
            for (int i = 0; i < width; i++)
            {
                lat[i] = new SquareBaxin[height];
                for (int j = 0; j < height; j++)
                {
                    lat[i][j] = new SquareBaxin(this, i, j, null);
                }
            }
            this.width = width;
            this.height = height;
        }
        
        #region ILattice Members

        public int Height
        {
            get { return height; }
        }

        public int Width
        {
            get { return width; }
        }

        public ISquareBaxin this[int x, int y]
        {
            get
            {
                if ((x < 0) || (x >= width) || (y < 0) || (y >= height))
                {
                    throw new ApplicationException("SquareLattice: Index out of bounds");
                }
                return lat[x][y];
            }
            set
            {
                lat[x][y] = value;
            }
        }

        #endregion

        #region IEnumerable Members

        public System.Collections.IEnumerator GetEnumerator()
        {
            return new LatticeEnumerator(this);
        }

        #endregion
    }

    public class LatticeEnumerator : IEnumerator
    {
        private ILattice lattice;
        private int x_cur;
        private int y_cur;
        public LatticeEnumerator(ILattice lattice)
        {
            this.lattice = lattice;
            Reset();
        }

        #region IDisposable Members

        public void Dispose()
        {

        }

        #endregion

        #region IEnumerator Members

        object IEnumerator.Current
        {
            get
            {
                return (object)(lattice[x_cur, y_cur]);
            }
        }

        public bool MoveNext()
        {
            x_cur++;
            if (x_cur >= lattice.Width)
            {
                y_cur++;
                x_cur = 0;
            }
            if (y_cur >= lattice.Height)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public void Reset()
        {
            x_cur = -1;
            y_cur = 0;
        }

        #endregion
    }
}
