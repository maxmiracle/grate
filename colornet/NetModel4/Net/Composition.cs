using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Net
{
    /// <summary>
    /// Агрегирующий класс для композиции
    /// </summary>
    public class Composition : CompositionPart
    {
        public Composition(IEnumerable<CompositionPart> parts) : base(null, parts)
        {

        }
    }
}
