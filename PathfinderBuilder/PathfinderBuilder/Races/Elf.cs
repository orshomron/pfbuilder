using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PathfinderBuilder.Races
{
    public class Elf : Race
    {
        private static readonly List<Language> StaticStartingLanguages = new List<Language> { Language.Common, Language.Elven };
        private static readonly List<Language> StaticAvailableLanguages = new List<Language> { Language.Celestial, Language.Draconic, Language.Gnoll, Language.Gnome, Language.Goblin, Language.Orc, Language.Sylvan };

        [Flags]
        public enum RacialTraitsCategories : ulong
        {
            None = 0ul,
            [Display(ShortName = "Keen Senses")]
            KeenSenses = 1ul << 0,
            [Display(ShortName = "Elven Immunities")]
            ElvenImmunities = 1ul << 1,
            [Display(ShortName = "Elven Magic")]
            ElvenMagic = 1ul << 2,
            [Display(ShortName = "Weapon Familiarity")]
            WeaponFamiliarity = 1ul << 3,
            [Display(ShortName = "Low-Light Vision")]
            LowLightVision = 1ul << 4,
        }

        public Elf()
        {
            SelectedTraits.Add(new SimpleSkillAdditionRacialTrait("Keen Senses", "+2 Perception", new[] { new KeyValuePair<Skills, int>(Skills.Perception, 2) }, typeof(RacialTraitsCategories), BookSource.Core, RacialTraitsCategories.KeenSenses));
            SelectedTraits.Add(new LowLightVisionRacialTrait(typeof(RacialTraitsCategories), RacialTraitsCategories.LowLightVision, BookSource.Core));
        }

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
            get { return 2; }
        }

        public override int ConstitutionModifier
        {
            get { return -2; }
        }

        public override int IntelligenceModifier
        {
            get { return 2; }
        }

        public override int WisdomModifier
        {
            get { return 0; }
        }

        public override int CharismaModifier
        {
            get { return 0; }
        }

        public override string RaceName
        {
            get { return "Elf"; }
        }

        public override string Type
        {
            get { return "Humanoid"; }
        }

        public override List<string> Subtypes
        {
            get { return new List<string> { "Elf" }; }
        }

        public override Size Size
        {
            get { return Size.Medium; }
        }

        public override int Speed
        {
            get { return 30; }
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