using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Grate.ColorDispersion;
using System.Drawing;

namespace Grate.Reactor3
{
    public class Reactor
    {
        private Dictionary<Reactor, Synapse> selectors;

        public Dictionary<Reactor, Synapse> Selectors
        {
            get
            {
                return selectors;
            }
        }

        private Dictionary<Reactor, Synapse> feelers;
        
        public Dictionary<Reactor, Synapse> Feelers
        {
            get
            {
                return feelers;
            }
        }

        private ColorWeight currentFeeling;
        private ColorWeight backReaction;

        public Color Color
        {
            get
            {
                return currentFeeling.Color;
            }
        }

        public Color BackReactionColor
        {
            get
            {
                return backReaction.Color;
            }
        }

        public Reactor(ColorWeight cw)
        {
            currentFeeling = cw;
            selectors = new Dictionary<Reactor, Synapse>();
            feelers = new Dictionary<Reactor, Synapse>();
        }

        public Reactor(Color c) : this(new ColorWeight(c, 1)) { }

        public ColorWeight Feel()
        {
            ColorWeightSum sum = new ColorWeightSum(ColorWeight.BlackNull);
            foreach (Synapse s in selectors.Values)
            {
                ColorWeight tmp_cw = new ColorWeight(s.reaction.Color, s.weight) ;
                sum = sum + tmp_cw;
            }
            ColorWeight cw = sum.Average();
            foreach (Synapse s in selectors.Values)
            {
                s.feeling = cw;
            }
            return cw;
        }

        public ColorWeight React()
        {
            if (selectors.Count > 0)
            {
                ColorWeightSum sum = new ColorWeightSum(ColorWeight.BlackNull);
                foreach (Synapse s in selectors.Values)
                {
                    if (s.selection.InGame)
                    {
                        sum = sum + s.selection.ColorWeight;
                    }
                }
                ColorWeight cw = sum.Average();
                currentFeeling = cw;
            }
            foreach (Synapse s in feelers.Values)
            {
                s.reaction = currentFeeling;
            }            
            return currentFeeling;
        }

        public void Select()
        {
            double minDifference = double.MaxValue;
            Synapse selectedSynapse = null;
            foreach (Synapse s in feelers.Values)
            {
                double difference = ColorWeight.Delta(currentFeeling, s.feeling);
                if (difference < minDifference)
                {
                    selectedSynapse = s;
                    minDifference = difference;
                }
            }
            foreach (Synapse s in feelers.Values)
            {

                if (s == selectedSynapse)
                {
                    s.selection = new ColorImpact(true, currentFeeling);
                }
                else
                {
                    s.selection = ColorImpact.Pass;
                }
            }
        }

        public void Slave()
        {
            if (feelers.Count > 0)
            {
                foreach (Synapse s in feelers.Values)
                {
                    if (s.selection.InGame)
                    {
                        backReaction = s.backReaction;
                        break;
                    }
                }
            }
            else
            {
                backReaction = currentFeeling;
            }
            if (selectors.Count > 0)
            {
                foreach (Synapse s in selectors.Values)
                {
                    if (s.selection.InGame)
                    {
                        s.backReaction = backReaction;
                    }
                }
            }
        }
    }
}
