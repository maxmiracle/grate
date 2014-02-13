using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace Grate.ColorDispersion
{
    public class ColorWeightNpc : ColorWeight , INotifyPropertyChanged
    {
        private bool isSelected;

        public bool IsSelected
        {
            get { return isSelected; }
            set
            {
                isSelected = value;
                NotifyPropertyChanged("IsSelected");
            }
        }

        public override double Weight
        {
            get { return weight; }
            set { 
                weight = value;
                NotifyPropertyChanged("Weight");
            }
        }

        public override System.Windows.Media.Color Color
        {
            get { return color; }
            set { color = value; NotifyPropertyChanged("Color"); }
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
