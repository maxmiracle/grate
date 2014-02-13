using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Grate.ColorDispersion
{
    public class ColorDispersionException : Exception
    {
        public ColorDispersionException() { }
        public ColorDispersionException(string message) : base(message) { }
    }
}
