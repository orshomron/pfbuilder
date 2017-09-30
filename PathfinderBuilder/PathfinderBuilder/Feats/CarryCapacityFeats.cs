using System.Collections.Generic;

namespace PathfinderBuilder.Feats
{
    public class CutYourLosses : IFeat, IAddToEffectiveCarryCapacity
    {
        public CutYourLosses()
        {
            var strength = new AbilityPrerequisite(new Dictionary<Attributes, int> { { Attributes.Strength, 13 } });
            var acrobatics = new SkillRankPrerequisite(Skills.Acrobatics, 1);

            Prerequisite = new ComplexPrerequisite(strength, acrobatics);
        }

        public IPrerequisite Prerequisite { get; }

        public string Name => "Cut Your Losses";

        public string Description => @"Whenever you withdraw as a full-round action and have at least one free hand, you can pick up one unattended object or unconscious ally of your size or smaller at any point during your movement without provoking attacks of opportunity. Any additional movement performed on your turn still provokes attacks of opportunity as normal.
In addition, you treat your Strength score as 2 higher for the purpose of determining your carrying capacity.";
        public BookSource Source => BookSource.PathfinderSocietyPrimer;
        public int StrIncrease => 2;
        public double FlatBonus => 0;
        public double FlatMultiplier => 1;
    }
}
