using System.Collections.Generic;

namespace PathfinderBuilder.Races
{
    public class Halfling : Race
    {
        private static readonly List<Language> StaticStartingLanguages = new List<Language> { Language.Common, Language.Halfling };
        private static readonly List<Language> StaticAvailableLanguages = new List<Language> { Language.Dwarven, Language.Elven, Language.Gnome, Language.Goblin };

        public override bool RaceHasOptionalAbilityModifier
        {
            get { return false; }
        }

        public override int StrengthModifier
        {
            get { return -2; }
        }

        public override int DexterityModifier
        {
            get { return 2; }
        }

        public override int ConstitutionModifier
        {
            get { return 0; }
        }

        public override int IntelligenceModifier
        {
            get { return 0; }
        }

        public override int WisdomModifier
        {
            get { return 0; }
        }

        public override int CharismaModifier
        {
            get { return 2; }
        }

        public override string RaceName
        {
            get { return "Halfling"; }
        }

        public override string Type
        {
            get { return "Humanoid"; }
        }

        public override List<string> Subtypes
        {
            get { return new List<string> { "Halfling" }; }
        }

        public override Size Size
        {
            get { return Size.Small; }
        }

        public override int Speed
        {
            get { return 20; }
        }

        public override List<Language> StartingLanguages
        {
            get { return StaticStartingLanguages; }
        }

        public override List<Language> AvailableLanguages
        {
            get { return StaticAvailableLanguages; }
        }
    }
}