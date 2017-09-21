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

        public override bool RaceHasOptionalAbilityModifier => true;

        public override int StrengthModifier => AttributesWithModifier.Contains(Attributes.Strength) ? 2 : 0;

        public override int DexterityModifier => AttributesWithModifier.Contains(Attributes.Dexterity) ? 2 : 0;

        public override int ConstitutionModifier => AttributesWithModifier.Contains(Attributes.Constitution) ? 2 : 0;

        public override int IntelligenceModifier => AttributesWithModifier.Contains(Attributes.Intelligence) ? 2 : 0;

        public override int WisdomModifier => AttributesWithModifier.Contains(Attributes.Wisdom) ? 2 : 0;

        public override int CharismaModifier => AttributesWithModifier.Contains(Attributes.Charisma) ? 2 : 0;

        public override string RaceName => "Half-Elf";

        public override string Type => "Humanoid";

        public override List<string> Subtypes => new List<string> { "Elf", "Human" };

        public override Size Size => Size.Medium;

        public override int Speed => 30;

        public override List<Language> StartingLanguages => StaticStartingLanguages;

        public override List<Language> AvailableLanguages => StaticAvailableLanguages;
        public HashSet<Attributes> AttributesWithModifier { get; set; } = new HashSet<Attributes>();
        public int NumberOfAttributes { get; set; } = 1;
    }
}