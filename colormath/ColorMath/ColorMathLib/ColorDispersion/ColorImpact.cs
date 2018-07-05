using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Grate.ColorDispersion
{
    public class ColorImpact
    {
        private bool inGame;
        private ColorWeight colorWeight;

        // PassPixel doesn't affect result;
        public static ColorImpact Pass = new ColorImpact(false, ColorWeight.BlackNull);

        public bool InGame
        {
            get
            {
                return inGame;
            }
        }

        public ColorWeight ColorWeight
        {
            get
            {
                return colorWeight;
            }
        }

        public ColorImpact(bool inGame, ColorWeight colorWeight)
        {
            this.inGame = inGame;
            this.colorWeight = colorWeight;
        }


    }
}
