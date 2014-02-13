using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Grate
{
    public class SquareBaxin : ISquareBaxin
    {
        private int x;
        private int y;
        private Object tag;
        private ILattice lattice;

        public SquareBaxin(ILattice lattice, int x, int y, Object cell)
        {
            this.lattice = lattice;
            this.x = x;
            this.y = y;
            tag = cell;
        }
        
        public int X
        {
            get
            {
                return x;
            }
        }

        
        public int Y
        {
            get
            {
                return y;
            }
        }


        public Object Tag
        {
            set
            {
                tag = value;
            }
            get
            {
                return tag;
            }
        }


        public ILattice Lattice
        {
            get
            {
                return lattice;
            }
        }

        public ISquareBaxin Left
        {
            get
            {
                return lattice[x - 1, y];
            }
        }

        public ISquareBaxin Right
        {
            get
            {
                return lattice[x + 1, y];
            }
        }

        public ISquareBaxin Top
        {
            get
            {
                return lattice[x, y - 1];
            }
        }

        public ISquareBaxin Bottom
        {
            get
            {
                return lattice[x, y + 1];
            }
        }


    }
}
