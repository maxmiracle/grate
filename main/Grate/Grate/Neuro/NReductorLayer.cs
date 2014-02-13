using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Grate.Neuro
{
    public class NReductorLayer
    {
        private Lattice lattice;
        public NReductorLayer(NSelectorLayer selectorLayer)
        {
            Lattice selectorLattice = selectorLayer.Lattice;
            lattice = new Lattice((selectorLattice.Width - 1) / 2, (selectorLattice.Height - 1) / 2);
            double w1 = 1;
            double w2 = 0.7;
            double w3 = 0.5;
            for (int i = 0; i < lattice.Width; i++)
            {
                for (int j = 0; j < lattice.Height; j++)
                {
                    // центры в координатах предыдущего уровня
                    int ii = i * 2 + 1;
                    int jj = j * 2 + 1;
                    NReductor reductor = new NReductor();
                    lattice[i, j].Tag = reductor;
                    AddReductorInput(selectorLattice, ii, jj, reductor, w1);
                    AddReductorInput(selectorLattice, ii - 1, jj, reductor, w2);
                    AddReductorInput(selectorLattice, ii, jj - 1, reductor, w2);
                    AddReductorInput(selectorLattice, ii + 1, jj, reductor, w2);
                    AddReductorInput(selectorLattice, ii, jj + 1, reductor, w2);
                    AddReductorInput(selectorLattice, ii - 1, jj - 1, reductor, w3);
                    AddReductorInput(selectorLattice, ii + 1, jj + 1, reductor, w3);
                    AddReductorInput(selectorLattice, ii - 1, jj + 1, reductor, w3);
                    AddReductorInput(selectorLattice, ii + 1, jj - 1, reductor, w3);
                }
            }
        }

        private static ReductorInput AddReductorInput(Lattice selectorLattice, int i, int j, NReductor reductor, double weight)
        {
            ReductorInput reductorInput = new ReductorInput(weight);
            reductor.ReductorsList.Add(reductorInput);
            ((NSelector)(selectorLattice[i, j].Tag)).Reductors.Add(reductorInput);
            return reductorInput;
        }

        public void Fill()
        {
            foreach (object obj in lattice)
            {
                NReductor reductor = (obj as SquareBaxin).Tag as NReductor;
                reductor.Fill();
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
                    bm.SetPixel(i, j, ((lattice[i, j].Tag) as NReductor).ConsolidatedColorWeight.Color);
                }
            }
            imagePreview.Show(bm);
        }
    }
}
