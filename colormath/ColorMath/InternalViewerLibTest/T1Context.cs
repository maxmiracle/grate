using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternalViewerLibTest
{
    public class T1Context
    {
        public static T1Context GetContext()
        {
            T1Context t1 = new T1Context()
                {
                    Description = "T1 Context test object",
                    ComplicatedDic = new Dictionary<int, ComplicatedStruct>()
                        {
                            { 1, new ComplicatedStruct() { Value = 1, Description = "First"} },
                            { 2, new ComplicatedStruct() { Value = 22, Description = "Second"} },
                            { 3, new ComplicatedStruct() { Value = 33, Description = "Third"} }                             
                        },
                    ComplicatedList = new List<ComplicatedStruct>()
                    {new ComplicatedStruct(){Description = "SomeItem", Value = 500},
                     new ComplicatedStruct(){Description = "An item", Value = 600}},
                    StringItems = new List<string>() { "text1", "text2", "text3", "text4" },
                    StringItems2 = new List<string>() { "More text1", "More text2", "More text3", "More text4" }
                };
            return t1;
        }

        public string Description { get; set; }
        public List<string> StringItems { get; set; }
        public IList<string> StringItems2 { get; set; }
        public List<ComplicatedStruct> ComplicatedList { get; set; }
        public Dictionary<int, ComplicatedStruct> ComplicatedDic { get; set; }
    }

    public class ComplicatedStruct
    {
        public int Value { get; set; }
        public string Description { get; set; }
    }
}
