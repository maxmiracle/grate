using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Grate.ColorDispersion;
using System.Windows.Media;

namespace ColorMathLib.ColorSet
{
    public class TestColorSet
    {
        public static List<ColorWeight> GetTestColorSet(int num)
        {
            List<ColorWeight> list = new List<ColorWeight>();
            switch ( num ) 
            {
                case 1:
                    list.Add( new ColorWeight( Colors.Aqua, 1 ));
                    list.Add(new ColorWeight(Colors.Red, 1));
                    list.Add(new ColorWeight(Colors.Bisque, 1));
                    break;
                case 2:
                    list.Add(new ColorWeight(Colors.MediumAquamarine, 1));
                    list.Add(new ColorWeight(new Color() { A = 0xFF, B = 0x0F, G = 0x0F, R = 0x07 }, 1));
                    list.Add(new ColorWeight(Colors.Maroon, 1));
                    list.Add(new ColorWeight(Colors.Lime, 1));
                    break;
            }
            return list; 
        }
    }
}
