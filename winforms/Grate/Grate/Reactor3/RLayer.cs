using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using Grate.ColorDispersion;
using System.Diagnostics;

namespace Grate.Reactor3
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
                    lattice[i, j].Tag = new Reactor((image as Bitmap).GetPixel(i, j));
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
                    Reactor reactor = new Reactor(ColorWeight.BlackNull);
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

        private Synapse AddReactorInput( Lattice selectorLattice,  int i, int j, Reactor reactor, double weight)
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
            ImagePreview imagePreview = new ImagePreview();
            Bitmap bm = new Bitmap(lattice.Width, lattice.Height);
            for (int i = 0; i < lattice.Width; i++)
            {
                for (int j = 0; j < lattice.Height; j++)
                {
                    bm.SetPixel(i, j, ((lattice[i, j].Tag) as Reactor).Color);
                }
            }
            imagePreview.Show(bm);
        }

        public void ViewImageBackReaction()
        {
            ImagePreview imagePreview = new ImagePreview();
            Bitmap bm = new Bitmap(lattice.Width, lattice.Height);
            for (int i = 0; i < lattice.Width; i++)
            {
                for (int j = 0; j < lattice.Height; j++)
                {
                    bm.SetPixel(i, j, ((lattice[i, j].Tag) as Reactor).BackReactionColor);
                }
            }
            imagePreview.Show(bm);
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
