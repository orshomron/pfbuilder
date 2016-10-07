using System.Collections.Generic;

namespace PathfinderBuilder.Races
{
    public class Dwarf : Race
    {
        private static readonly List<Language> StaticStartingLanguages = new List<Language> { Language.Common, Language.Dwarven };
        private static readonly List<Language> StaticAvailableLanguages = new List<Language> { Language.Giant, Language.Gnome, Language.Goblin, Language.Orc, Language.Terran, Language.Undercommon };

        public override bool RaceHasOptionalAbilityModifier
        {
            get { return false; }
        }

        public override int StrengthModifier
        {
            get { return 0; }
        }

        public override int DexterityModifier
        {
            get { return 0; }
        }

        public override int ConstitutionModifier
        {
            get { return 2; }
        }

        public override int IntelligenceModifier
        {
            get { return 0; }
        }

        public override int WisdomModifier
        {
            get { return 2; }
        }

        public override int CharismaModifier
        {
            get { return -2; }
        }

        public override string RaceName
        {
            get { return "Dwarf"; }
        }

        public override string Type
        {
            get { return "Humanoid"; }
        }

        public override List<string> Subtypes
        {
            get { return new List<string> { "Dwarf" }; }
        }

        public override Size Size
        {
            get { return Size.Medium; }
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