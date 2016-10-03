using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PathfinderBuilder
{
    public class Duration
    {
        public int Multipier { get; set; }

        public DurationModifier Modifier { get; set; }

        public bool FlatValue { get; set; }

        public int PerNumberOfLevels { get; set; }

        public Duration()
        {
            Multipier = 1;
            PerNumberOfLevels = 1;
            Modifier = DurationModifier.Round;
            FlatValue = false;
        }

        public Duration(int multipier, DurationModifier modifier, bool flatValue, int perNumberOfLevels)
        {
            Multipier = multipier;
            Modifier = modifier;
            FlatValue = flatValue;
            PerNumberOfLevels = perNumberOfLevels;
        }

        public Duration(Duration duration)
        {
            Multipier = duration.Multipier;
            Modifier = duration.Modifier;
            FlatValue = duration.FlatValue;
            PerNumberOfLevels = duration.PerNumberOfLevels;
        }
    }
}
