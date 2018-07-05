using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.Windows.Media;

namespace ColorMapView
{
    public class ColorCollectionContext
    {
        public ColorCollectionContext()
        {
            ColorCollection = new ObservableCollection<ColorBox>();
        }

        public ObservableCollection<ColorBox> ColorCollection { get; set; }

        public void AddColor(Color color)
        {
            ColorCollection.Add(new ColorBox() { Color = color });
        }
    }
}
