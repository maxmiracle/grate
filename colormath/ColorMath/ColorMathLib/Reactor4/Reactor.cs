using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Grate.ColorDispersion;
using System.Windows.Media;

namespace Grate.Reactor4
{
    /// <summary>
    /// class Reactor
    /// 
    /// Neuron with activity of 4-th generation
    /// ColorGroup should be used here
    /// 
    /// Scheme
    /// 
    /// React  => Feel
    ///        <=
    /// Select => React =>  Feel
    ///                 <= 
    ///          Select => React
    /// Selector has many Feelers
    /// But at the end of the cycle Selector should chose one Reactor(Feeler)
    /// At the first stage feeler make sense about selectors by ColorGroup class mechanics
    /// Then Selector get feelers sense and aim for the best feeler (reactor)
    /// Colors should weights to ther natives attractions in the next level.
    /// </summary>
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

        public System.Drawing.Color BackReactionColorF
        {
            get
            {
                return backReaction.FColor;
            }
        }

        public Object Parent { get; set; }

        public Reactor(ColorWeight cw, Object parent)
            : this(cw)
        {
            this.Parent = parent;
        }

        public Reactor(ColorWeight cw)
        {
            currentFeeling = cw;
            selectors = new Dictionary<Reactor, Synapse>();
            feelers = new Dictionary<Reactor, Synapse>();
        }

        public Reactor(Color c) : this(new ColorWeight(c, 1)) { }



        /// <summary>
        /// Feel the selectors
        /// </summary>
        /// <returns>What neuron is feeling, what color is the best for these inputs</returns>
        public ColorWeight Feel()
        {
            ColorWeight colorFeeling = ColorWeight.BlackNull;
            try
            {
                List<ColorWeight> colorsToFeel = new List<ColorWeight>();
                foreach (Synapse s in selectors.Values)
                {
                    //s.reaction.Tag = s;
                    //s.reaction.Weight = s.weight;
                    colorsToFeel.Add(s.reaction);
                }
                // Color group mechanics
                ColorGroup cg = ColorGroup.CreateColorGroup2(colorsToFeel.ToArray());
                //40 percents of inputs make decision that have more occurences
                colorFeeling = cg.Feel(0.4);
                foreach (Synapse s in selectors.Values)
                {
                    s.feeling = colorFeeling;
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.ToString());
            }
            finally
            {
                
            }
            return colorFeeling;
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
