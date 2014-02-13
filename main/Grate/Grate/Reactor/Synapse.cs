using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Grate.ColorDispersion;

namespace Grate.Reactor
{
    public class Synapse
    {
        // dendrit -> neuron -> axon -> synapse -> dendrit
        public Reactor axon;
        public Reactor dendrit;
        public ColorWeight reaction;
        public ColorImpact selection;
        public ColorWeight feeling;
        public ColorWeight backReaction;
    }
}
