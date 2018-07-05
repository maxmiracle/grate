using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Collections;

namespace Grate.Neuro
{
    public class NSelectorLayer
    {
        private Lattice lattice;

        public Lattice Lattice
        {
            get
            {
                return lattice;
            }
        }

        public NSelectorLayer(System.Drawing.Image image)
        {
            if (image == null)
            {
                lattice = null;
            }
            lattice = new Lattice(image.Width, image.Height);
            for (int i = 0; i < image.Width; i++)
            {
                for (int j = 0; j < image.Height; j++)
                {
                    lattice[i, j].Tag = new NSelector((image as Bitmap).GetPixel(i, j));
                }
            }
        }

        internal void Affect()
        {
            foreach ( object obj in lattice)
            {
                NSelector selector = (obj as SquareBaxin).Tag as NSelector;
                foreach (IReductorInput reductorInput in selector.Reductors)
                {
                    reductorInput.Affect(selector.Color);
                }
            }
        }
    }
}
