using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using Grate.ColorDispersion;
using System.Diagnostics;
using System.Windows.Media.Imaging;
using System.Runtime.InteropServices;
using System.Windows.Media;

namespace Grate.Reactor4
{
    public class RLayer
    {
        private Lattice lattice;

        public Lattice Lattice
        {
            get
            {
                return lattice;
            }
        }

        public delegate void ShowBitmapDelegate( BitmapSource bitmap, String title);

        public event ShowBitmapDelegate OnShowBitmap;
        private BitmapImage bitmapImage;

        private void fireShowBitmap( BitmapSource bitmap, String title )
        {
            if ( OnShowBitmap != null )

            {
                OnShowBitmap ( bitmap, title );
            }
        }

        public RLayer(Image image)
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
                    lattice[i, j].Tag = new Reactor(new ColorWeight((image as Bitmap).GetPixel(i, j), 1));
                }
            }
        }

        public RLayer(RLayer layer)
        {
            Lattice selectorLattice = layer.Lattice;
            lattice = new Lattice((selectorLattice.Width - 1) / 2, (selectorLattice.Height - 1) / 2);
            Debug.WriteLine(String.Format("Width:{0}, Height:{1}", selectorLattice.Width, selectorLattice.Height));
            double w1 = 1.0;
            double w2 = 0.0;
            double w3 = 0.0;
            double w4 = 0.0;
            double w5 = 0.0;
            for (int i = 0; i < lattice.Width; i++)
            {
                for (int j = 0; j < lattice.Height; j++)
                {
                    // центры в координатах предыдущего уровня
                    int ii = i * 2 + 1;
                    int jj = j * 2 + 1;
                    Reactor reactor = new Reactor(ColorWeight.BlackNull, lattice[i,j]);
                    lattice[i, j].Tag = reactor;
                    AddReactorInput(selectorLattice, ii, jj, reactor, w1);
                    AddReactorInput(selectorLattice, ii - 1, jj, reactor, w2);
                    AddReactorInput(selectorLattice, ii, jj - 1, reactor, w2);
                    AddReactorInput(selectorLattice, ii + 1, jj, reactor, w2);
                    AddReactorInput(selectorLattice, ii, jj + 1, reactor, w2);
                    AddReactorInput(selectorLattice, ii - 1, jj - 1, reactor, w3);
                    AddReactorInput(selectorLattice, ii + 1, jj + 1, reactor, w3);
                    AddReactorInput(selectorLattice, ii - 1, jj + 1, reactor, w3);
                    AddReactorInput(selectorLattice, ii + 1, jj - 1, reactor, w3);

                    AddReactorInput(selectorLattice, ii - 2, jj, reactor, w4);
                    AddReactorInput(selectorLattice, ii, jj - 2, reactor, w4);
                    AddReactorInput(selectorLattice, ii + 2, jj, reactor, w4);
                    AddReactorInput(selectorLattice, ii, jj + 2, reactor, w4);

                    AddReactorInput(selectorLattice, ii - 2, jj - 1, reactor, w5);
                    AddReactorInput(selectorLattice, ii + 2, jj + 1, reactor, w5);
                    AddReactorInput(selectorLattice, ii - 2, jj + 1, reactor, w5);
                    AddReactorInput(selectorLattice, ii + 2, jj - 1, reactor, w5);

                    AddReactorInput(selectorLattice, ii - 1, jj - 2, reactor, w5);
                    AddReactorInput(selectorLattice, ii + 1, jj + 2, reactor, w5);
                    AddReactorInput(selectorLattice, ii - 1, jj + 2, reactor, w5);
                    AddReactorInput(selectorLattice, ii + 1, jj - 2, reactor, w5);
                }
            }
        }

        public RLayer(BitmapImage bitmapImage)
        {
            this.bitmapImage = bitmapImage;
            if (bitmapImage == null)
            {
                lattice = null;
            }

            int[] pixels = GetPixels(bitmapImage);
            lattice = new Lattice(bitmapImage.PixelWidth, bitmapImage.PixelHeight);
            for (int i = 0; i < bitmapImage.PixelWidth; i++)
            {
                for (int j = 0; j < bitmapImage.PixelHeight; j++)
                {
                    //PixelColor cur = (PixelColor)(pixels[i*bitmapImage.PixelWidth + j]);
                    System.Drawing.Color colF = System.Drawing.Color.FromArgb(pixels[j * bitmapImage.PixelWidth + i]); 
                    //System.Windows.Media.Color color = new System.Windows.Media.Color() { A = cur.Alpha, B = cur.Blue, G = cur.Green, R = cur.Red };
                    lattice[i, j].Tag = new Reactor(new ColorWeight(colF, 1), lattice[i, j]);
                }
            }
        }

        public int[] GetPixels(BitmapImage source)
        {
            if ((source.Format != PixelFormats.Bgra32) &&
                ((source.Format != PixelFormats.Bgr32)))
                throw new Exception("не тот формат!");
                //source = new FormatConvertedBitmap(source, (FormatConvertedBitmap)(PixelFormats.Bgra32), null, 0);

            int width = source.PixelWidth;
            int height = source.PixelHeight;
            int nStride = (source.PixelWidth * source.Format.BitsPerPixel + 7) / 8;
            //int nStride = 1024;
            int[] m = new int[width * height];
            source.CopyPixels(m, nStride, 0);
            return m;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct PixelColor
        {
            public byte Blue;
            public byte Green;
            public byte Red;
            public byte Alpha;
        }

        private Synapse AddReactorInput(Lattice selectorLattice, int i, int j, Reactor reactor, double weight)
        {
            if ((i >= 0) && (j >= 0) && (i < selectorLattice.Width) && (j < selectorLattice.Height))
            {
                Synapse synapse = new Synapse();
                synapse.weight = weight;
                synapse.axon = reactor;
                synapse.dendrit = (Reactor)(selectorLattice[i, j].Tag);
                reactor.Selectors.Add(synapse.dendrit, synapse);
                synapse.dendrit.Feelers.Add(reactor, synapse);
                return synapse;
            }
            else
            {
                return null;
            }
        }

        public void ViewImage()
        {
            Bitmap bm = new Bitmap(lattice.Width, lattice.Height);
            int[] pixels = new int[ lattice.Height * lattice.Width ];
            for (int i = 0; i < lattice.Width; i++)
            {
                for (int j = 0; j < lattice.Height; j++)
                {
                    bm.SetPixel(i, j, ColorWeight.ConvertToDrawingColor(((lattice[i, j].Tag) as Reactor).Color));
                    pixels[j*lattice.Width + i] = ColorWeight.ConvertToDrawingColor(((lattice[i, j].Tag) as Reactor).Color).ToArgb();
                }
            }


            BitmapSource bms = BitmapSource.Create(lattice.Width, lattice.Height, 96, 96, PixelFormats.Bgr32, null, pixels, lattice.Width * 4);
            fireShowBitmap(bms, "Some image");
        }

        public void ViewImageBackReaction()
        {
        
            Bitmap bm = new Bitmap(lattice.Width, lattice.Height);
            int[] pixels = new int[lattice.Height * lattice.Width];
            for (int i = 0; i < lattice.Width; i++)
            {
                for (int j = 0; j < lattice.Height; j++)
                {
                    bm.SetPixel(i, j, ((lattice[i, j].Tag) as Reactor).BackReactionColorF);
                    pixels[j * lattice.Width + i] = ((lattice[i, j].Tag) as Reactor).BackReactionColorF.ToArgb();
                }
            }
            BitmapSource bms = BitmapSource.Create(lattice.Width, lattice.Height, 96, 96, PixelFormats.Bgr32, null, pixels, lattice.Width * 4);
            this.fireShowBitmap(bms, "Back reaction");
        }

        internal void React()
        {
            foreach (object obj in lattice)
            {
                Reactor reactor = (obj as SquareBaxin).Tag as Reactor;
                reactor.React();
            }
        }

        internal void Feel()
        {
            foreach (object obj in lattice)
            {
                Reactor reactor = (obj as SquareBaxin).Tag as Reactor;
                reactor.Feel();
            }
        }

        internal void Select()
        {
            foreach (object obj in lattice)
            {
                Reactor reactor = (obj as SquareBaxin).Tag as Reactor;
                reactor.Select();
            }
        }

        internal void Slave()
        {
            foreach (object obj in lattice)
            {
                Reactor reactor = (obj as SquareBaxin).Tag as Reactor;
                reactor.Slave();
            }
        }
    }
}
