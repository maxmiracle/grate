using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace EmergentImage
{
    public class EmergentCommands
    {
        static EmergentCommands()
        {
            AnalyseColors = new RoutedUICommand("AnalyseColors", "AnalyseColors", typeof(EmergentCommands));
        }

        public static RoutedUICommand AnalyseColors { get; }
    }
}
