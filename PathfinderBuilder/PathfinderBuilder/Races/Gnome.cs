using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PathfinderBuilder.Races
{
    public class Gnome : Race
    {
        private static readonly List<Language> StaticStartingLanguages = new List<Language> { Language.Common, Language.Gnome, Language.Sylvan };
        private static readonly List<Language> StaticAvailableLanguages = new List<Language> { Language.Draconic, Language.Dwarven, Language.Elven, Language.Giant, Language.Goblin, Language.Orc };

        [Flags]
        public enum RacialTraitsCategories : ulong
        {
            None = 0ul,
            KeenSenses = BaseRacialTraits.KeenSenses,
            LowLightVision = BaseRacialTraits.LowLightVision,
            WeaponFamiliarity = BaseRacialTraits.WeaponFamiliarity,
            [Display(ShortName = "Obsessive")]
            Obsessive = 1ul << 0,
            [Display(ShortName = "Gnome Magic")]
            GnomeMagic = 1ul << 1,
            [Display(ShortName = "Hatred")]
            Hatred = 1ul << 2,
            [Display(ShortName = "Defensive Training")]
            DefensiveTraining = 1ul << 3,
            [Display(ShortName = "Illusion Resistance")]
            IllusionResistance = 1ul << 4,
        }

        public Gnome()
        {
            SelectedTraits.Add(new SimpleSkillAdditionRacialTrait("Keen Senses", "+2 Perception", new[] { new KeyValuePair<Skills, int>(Skills.Perception, 2) }, typeof(RacialTraitsCategories), BookSource.Core, RacialTraitsCategories.KeenSenses));
        }

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
            get { return 0; }
        }

        public override int CharismaModifier
        {
            get { return 2; }
        }

        public override string RaceName
        {
            get { return "Gnome"; }
        }

        public override string Type
        {
            get { return "Humanoid"; }
        }

        public override List<string> Subtypes
        {
            get { return new List<string> { "Gnome" }; }
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