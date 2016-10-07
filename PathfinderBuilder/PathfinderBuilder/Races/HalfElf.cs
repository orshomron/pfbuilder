using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace PathfinderBuilder.Races
{
    public class HalfElf : Race, IRaceWithOptionalAbilityModifier
    {
        private static readonly List<Language> StaticStartingLanguages = new List<Language> { Language.Common, Language.Elven };
        private static readonly List<Language> StaticAvailableLanguages = Enum.GetValues(typeof(Language)).Cast<Language>().Where(l => !StaticStartingLanguages.Contains(l)).ToList();

        [Flags]
        public enum RacialTraitsCategories : ulong
        {
            None = 0ul,
            [Display(ShortName = "Keen Senses")]
            KeenSenses = 1ul << 0,
            [Display(ShortName = "Elven Immunities")]
            ElvenImmunities = 1ul << 1,
            [Display(ShortName = "Multitalented")]
            Multitalented = 1ul << 2,
            [Display(ShortName = "Adaptability")]
            Adaptability = 1ul << 3,
            [Display(ShortName = "Low-Light Vision")]
            LowLightVision = 1ul << 4,
            [Display(ShortName = "Elf Blood")]
            ElfBlood = 1ul << 5,
        }

        public HalfElf()
        {
            SelectedTraits.Add(new SimpleRacialTrait("Elf Blood", "Half-elves count as both elves and humans for any effect related to race", typeof(RacialTraitsCategories), RacialTraitsCategories.ElfBlood));
            SelectedTraits.Add(new LowLightVisionRacialTrait(typeof(RacialTraitsCategories), RacialTraitsCategories.LowLightVision));
        }

        public override bool RaceHasOptionalAbilityModifier
        {
            get { return true; }
        }

        public override int StrengthModifier
        {
            get { return SelectedAttribute == Attributes.Strength ? 2 : 0; }
        }

        public override int DexterityModifier
        {
            get { return SelectedAttribute == Attributes.Dexterity ? 2 : 0; }
        }

        public override int ConstitutionModifier
        {
            get { return SelectedAttribute == Attributes.Constitution ? 2 : 0; }
        }

        public override int IntelligenceModifier
        {
            get { return SelectedAttribute == Attributes.Intelligence ? 2 : 0; }
        }

        public override int WisdomModifier
        {
            get { return SelectedAttribute == Attributes.Wisdom ? 2 : 0; }
        }

        public override int CharismaModifier
        {
            get { return SelectedAttribute == Attributes.Charisma ? 2 : 0; }
        }

        public override string RaceName
        {
            get { return "Half-Elf"; }
        }

        public Attributes SelectedAttribute { get; set; }

        public override string Type
        {
            get { return "Humanoid"; }
        }

        public override List<string> Subtypes
        {
            get { return new List<string> { "Elf", "Human" }; }
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