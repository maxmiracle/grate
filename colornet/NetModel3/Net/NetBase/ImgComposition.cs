using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetBase;

namespace Net.NetBase
{
    public class ImgComposition
    {
        

        public ImgComposition(List<IGrouping<SqRt, SqRt>> groups)
        {

            Groups = new List<SGroup>();
            foreach (var gr in groups)
            {
                SGroup sGroup = new SGroup(gr);
                Groups.Add(sGroup);
            }
        }

        public List<SGroup> Groups { get; protected set; }
    }
}
