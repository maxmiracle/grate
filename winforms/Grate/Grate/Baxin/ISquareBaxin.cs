using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Grate
{
    /// <summary>
    /// Связывает соседние ячеки
    /// </summary>
    public interface ISquareBaxin
    {
        int X { get; }
        int Y { get; }
        ISquareBaxin Top { get; }
        ISquareBaxin Left { get; }
        ISquareBaxin Bottom { get; }
        ISquareBaxin Right { get; }
        Object Tag { set;  get; }
    }
}
