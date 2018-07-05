using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace Grate.ColorDispersion
{
    public class ColorGroup : ColorElement, IEnumerable<ColorElement>
    {
        public static double GoldenRatio = (1 + Math.Sqrt(5)) / 2;

        private List<ColorElement> simpleNodes = new List<ColorElement>();

        public IList<ColorElement> Nodes
        {
            get
            {
                return simpleNodes;
            }
        }

        protected ColorGroup(ColorWeight cw1, ColorWeight cw2)
        {
            simpleNodes.Add(new ColorElement(cw1) { Parent = this });
            simpleNodes.Add(new ColorElement(cw2) { Parent = this });
            RecalculateMetrics();
            level = EstimateLevel(ColorWeight.Delta(cw1, cw2));
        }

        public static ColorGroup CreateColorGroup(params ColorWeight[] group)
        {
            if (group.Length < 2)
            {
                throw new ColorDispersionException("Group count couldn't be less then 2");
            }
            ColorGroup cg = new ColorGroup(group[0], group[1]);
            for (int i = 2; i < group.Length; i++)
            {
                cg = cg.Append(group[i]);
            }
            return cg;
        }

        public static ColorGroup CreateColorGroup2(params ColorWeight[] group)
        {
            if (group.Length < 2)
            {
                throw new ColorDispersionException("Group count couldn't be less then 2");
            }
            ColorGroup cg = new ColorGroup(group[0], group[1]);
            for (int i = 2; i < group.Length; i++)
            {
                cg = cg.Append2(group[i]);
            }
            return cg;
        }


        private int level;
        public int Level { get { return level; } }


        private void RecalculateMetrics()
        {
            ColorWeight cwa = ColorWeight.Sum(simpleNodes.ToArray());
            this.Color = cwa.Color;
            this.Weight = cwa.Weight;
        }

        private void RecalculateMetrics(ColorGroup cg)
        {
            do
            {
                cg.RecalculateMetrics();
                cg = cg.Parent;
            }
            while (cg != null);
        }

        public ColorGroup Append(ColorWeight cw)
        {
            if (simpleNodes.Count > 0)
            {
                double minDelta;
                // Find group with delta -> min
                ColorElement minArg = simpleNodes.ArgMin<ColorElement, double>((arg) => ColorWeight.Delta(cw, arg), out minDelta);
                int newLevel = EstimateLevel(minDelta);
                if (newLevel == Level)
                {
                    simpleNodes.Add(new ColorElement(cw));
                }
                else if (newLevel < Level)
                {
                    if (minArg is ColorGroup)
                    {
                        (minArg as ColorGroup).Append(cw);
                    }
                    else
                    {
                        simpleNodes.Remove(minArg);
                        simpleNodes.Add(new ColorGroup(minArg, cw));
                    }
                }
                else // newLavel > Level
                {
                    RecalculateMetrics();
                    return new ColorGroup(cw, this);
                }
            }
            else
            {
                simpleNodes.Add(new ColorElement(cw));
            }
            RecalculateMetrics();
            return this;
        }

        public ColorGroup Append2(ColorWeight cw)
        {
            if (simpleNodes.Count > 0)
            {
                double minDelta;
                // Find group with delta -> min
                ColorElement minArg = this.ArgMin<ColorElement, double>((arg) => ColorWeight.Delta(cw, arg), out minDelta);
                int newLevel = EstimateLevel(minDelta);
                if (newLevel == minArg.Parent.Level)
                {
                    minArg.Parent.simpleNodes.Add(new ColorElement(cw) { Parent = minArg.Parent });
                    RecalculateMetrics(minArg.Parent);
                }
                else if (newLevel < minArg.Parent.Level)
                {
                    ColorGroup parent = minArg.Parent;
                    parent.simpleNodes.Remove(minArg);
                    parent.simpleNodes.Add(new ColorGroup(minArg, cw) { Parent = parent });
                    RecalculateMetrics(parent);
                }
                else // newLavel > Level
                {
                    ColorGroup parent = minArg.Parent;
                    while (true)
                    {
                        if (parent != null)
                        {
                            if (parent.Level >= newLevel)
                            {
                                parent.simpleNodes.Add(new ColorElement(cw) { Parent = parent });
                                RecalculateMetrics(parent);
                                return this;
                            }
                            else
                            {
                                parent = parent.Parent;
                            }
                        }
                        else
                        {
                            ColorGroup cg = new ColorGroup(cw, this);
                            RecalculateMetrics(cg);
                            return cg;
                        }
                    }
                }
            }
            else
            {
                simpleNodes.Add(new ColorElement(cw) { Parent = this });
                RecalculateMetrics(this);
            }
            return this;
        }

        public int EstimateLevel(double delta)
        {
            if (delta == 0)
            {
                return 0;
            }
            else
            {
                return (int)(Math.Round(Math.Log(delta, 2 /*GoldenRatio*/)));
            }
            // 100000 - 5 levels;
            //double basis = delta * 100000 / MaxDelta;
            //return (int)(Math.Round(Math.Log10(basis)));
        }

        public ColorWeight Feel(double percent)
        {
            double weightNeeded = this.Weight*percent;
            ColorWeightSum sum = ColorWeightSum.BlackNull;
            simpleNodes.Sort(new ColorElementWeightComparer());
            simpleNodes.Reverse();
            int i = 0;
            do
            {
                double remain = weightNeeded - sum.Weight;
                ColorElement el = simpleNodes[i++];
                if ((el.Weight > remain) && (el is ColorGroup))
                {
                    ColorGroup ecg = (ColorGroup)el;
                    ColorWeight cwp = ecg.Feel(remain / el.Weight);
                    sum = sum + cwp;
                    break;
                }
                else
                {
                    sum = sum + el;
                }
            }
            while (sum.Weight < weightNeeded);
            ColorWeight res = sum.Average();
            res.Weight = sum.Weight;
            return res;
        }

        IEnumerator<ColorElement> IEnumerable<ColorElement>.GetEnumerator()
        {
            return new ColorGroupEnumerator(this);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return new ColorGroupEnumerator(this);
        }
    }

    public class ColorElementWeightComparer : IComparer<ColorElement>
    {
        public int Compare(ColorElement x, ColorElement y)
        {
            if (x.Weight > y.Weight) return 1;
            else if (x.Weight < y.Weight) return -1;
            else return 0;
        }
    }

    public class ColorGroupEnumerator : IEnumerator<ColorElement>
    {
        private ColorGroup colorGroup;

        private ColorElement currentElement = null;

        private ColorGroupEnumerator currentGroupEnumerator = null;

        private int currentIndex = -1;

        public ColorGroupEnumerator(ColorGroup colorGroup)
        {
            this.colorGroup = colorGroup;
        }

        public object Current
        {
            get { return currentElement; }
        }

        public bool MoveNext()
        {
            if (currentGroupEnumerator != null)
            {
                if (currentGroupEnumerator.MoveNext())
                {
                    currentElement = (ColorElement)(currentGroupEnumerator.Current);
                    return true;
                }
                else
                {
                    currentGroupEnumerator = null;
                    currentIndex++;
                }
            }
            else
            {
                currentIndex++;
            }
            if (currentIndex >= colorGroup.Nodes.Count)
            {
                return false;
            }
            ColorElement currentItem = colorGroup.Nodes[currentIndex];
            if (currentItem is ColorGroup)
            {
                currentGroupEnumerator = (ColorGroupEnumerator)((currentItem as IEnumerable<ColorElement>).GetEnumerator());
                if (currentGroupEnumerator.MoveNext())
                {
                    currentElement = (ColorElement)(currentGroupEnumerator.Current);
                    return true;
                }
                else
                {
                    currentGroupEnumerator = null;
                    return MoveNext();
                }
            }
            else
            {
                currentGroupEnumerator = null;
                currentElement = currentItem;
                return true;
            }
        }

        public void Reset()
        {
            currentElement = null;
            currentGroupEnumerator = null;
            currentIndex = -1;
        }

        public void Dispose()
        {
            Reset();
        }


        ColorElement IEnumerator<ColorElement>.Current
        {
            get { return currentElement; }
        }
    }
}
