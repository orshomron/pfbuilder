using System;
using System.Collections.Generic;

namespace PathfinderBuilder
{
    public abstract class Race
    {
        public abstract string Type { get; }
        public abstract List<string> Subtypes { get; }
        public abstract bool RaceHasOptionalAbilityModifier { get; }
        public abstract int StrengthModifier { get; }
        public abstract int DexterityModifier { get; }
        public abstract int ConstitutionModifier { get; }
        public abstract int IntelligenceModifier { get; }
        public abstract int WisdomModifier { get; }
        public abstract int CharismaModifier { get; }
        public abstract string RaceName { get; }
        public List<IRacialTrait> SelectedTraits { get; set; }
        public List<IRacialTrait> AvailableTraits { get; set; }
        public abstract Size Size { get; }
        public abstract int Speed { get; }
        public abstract List<Language> StartingLanguages { get; }
        public abstract List<Language> AvailableLanguages { get; }

        protected Race()
        {
            SelectedTraits = new List<IRacialTrait>();
            AvailableTraits = new List<IRacialTrait>();
        }

        public int GetModifier(Attributes attribute)
        {
            switch (attribute)
            {
                case Attributes.Charisma:
                    return CharismaModifier;
                case Attributes.Constitution:
                    return ConstitutionModifier;
                case Attributes.Dexterity:
                    return DexterityModifier;
                case Attributes.Strength:
                    return StrengthModifier;
                case Attributes.Wisdom:
                    return WisdomModifier;
                case Attributes.Intelligence:
                    return IntelligenceModifier;
                default:
                    throw new InvalidOperationException("Unknown attribute recieved");
            }
        }
    }

    public interface IRaceWithOptionalAbilityModifier
    {
        Attributes SelectedAttribute { get; set; }
    }
}
