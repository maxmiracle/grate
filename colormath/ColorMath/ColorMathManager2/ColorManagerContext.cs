using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using Grate.ColorDispersion;
using System.ComponentModel;
using System.Windows.Media;

namespace ColorMathManager2
{
    public class ColorManagerContext : INotifyPropertyChanged
    {
        public ColorManagerContext()
        {
            colorCollection = new ObservableCollection<ColorWeightNpc>();
            colorCollection.Add(new ColorWeightNpc() { Color = Colors.Azure, Weight = 1 });
            colorCollection.Add(new ColorWeightNpc() { Color = Colors.Azure, Weight = 1 });
            colorCollection.Add(new ColorWeightNpc() { Color = Colors.Azure, Weight = 1 });
            colorCollection.Add(new ColorWeightNpc() { Color = Colors.Green, Weight = 1 });
        }

        private ObservableCollection<ColorWeightNpc> colorCollection;

        public ObservableCollection<ColorWeightNpc> ColorCollection
        {
            get { return colorCollection; }
            set { colorCollection = value; NotifyPropertyChanged("ColorCollection"); }
        }


        private ColorGroup[] colorGroupResult;
        public ColorGroup[] ColorGroupResult
        {
            get { return colorGroupResult; }
            set
            {
                colorGroupResult = value;
                NotifyPropertyChanged("ColorGroupResult");
            }
        }

        public void GroupColorCollection()
        {
            ColorWeight[] colorArray = ColorCollection.ToArray();
            ColorWeight[] colorArray2 = new ColorWeight[colorArray.Length];

            for (int i = 0; i < colorArray.Length; i++)
            {
                colorArray2[i] = new ColorWeight(colorArray[i].Color, colorArray[i].Weight);
            }

            ColorGroup[] cg = new ColorGroup[] { ColorGroup.CreateColorGroup2(colorArray2) };
            ColorGroupResult = cg;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(string property)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }
    }
}
