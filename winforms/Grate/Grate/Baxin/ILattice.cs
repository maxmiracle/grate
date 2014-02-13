using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace Grate
{
    public interface ILattice : IEnumerable
    {
        int Height { get; }
        int Width { get; }
        ISquareBaxin this[int x, int y] { get; set; }
    }
}
