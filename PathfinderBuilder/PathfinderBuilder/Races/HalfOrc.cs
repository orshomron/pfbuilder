using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PathfinderBuilder.Races
{
    public class HalfOrc : Race, IRaceWithOptionalAbilityModifier
    {
        private static readonly List<Language> StaticStartingLanguages = new List<Language> { Language.Common, Language.Orc };
        private static readonly List<Language> StaticAvailableLanguages = new List<Language> { Language.Abyssal, Language.Draconic, Language.Giant, Language.Gnoll, Language.Goblin };

        [Flags]
        public enum RacialTraitsCategories : ulong
        {
            None = 0ul,
            Darkvision = BaseRacialTraits.Darkvision,
            WeaponFamiliarity = BaseRacialTraits.WeaponFamiliarity,
            [Display(ShortName = "Intimidating")]
            Intimidating = 1ul << 0,
            [Display(ShortName = "Orc Blood")]
            OrcBlood = 1ul << 1,
            [Display(ShortName = "Orc Ferocity")]
            OrcFerocity = 1ul << 2,
        }

        public HalfOrc()
        {
            var type = typeof(RacialTraitsCategories);
            const RacialTraitsCategories orcBlood = RacialTraitsCategories.OrcBlood;
            const RacialTraitsCategories intimidating = RacialTraitsCategories.Intimidating;

            SelectedTraits.Add(new SimpleSkillAdditionRacialTrait("Intimidating", "+2 Intimidate", new[] { new KeyValuePair<Skills, int>(Skills.Intimidate, 2) }, type, intimidating));
            SelectedTraits.Add(new SimpleRacialTrait("Orc Blood", "Half-orcs count as both humans and orcs for any effect related to race", type, orcBlood));
        }

        public override string Type => "Humanoid";

        public override List<string> Subtypes => new List<string> { "Human", "Orc" };

        public override bool RaceHasOptionalAbilityModifier => true;

        public override int StrengthModifier => AttributesWithModifier.Contains(Attributes.Strength) ? 2 : 0;

        public override int DexterityModifier => AttributesWithModifier.Contains(Attributes.Dexterity) ? 2 : 0;

        public override int ConstitutionModifier => AttributesWithModifier.Contains(Attributes.Constitution) ? 2 : 0;

        public override int IntelligenceModifier => AttributesWithModifier.Contains(Attributes.Intelligence) ? 2 : 0;

        public override int WisdomModifier => AttributesWithModifier.Contains(Attributes.Wisdom) ? 2 : 0;

        public override int CharismaModifier => AttributesWithModifier.Contains(Attributes.Charisma) ? 2 : 0;

        public override string RaceName => "Half-Orc";

        public override Size Size => Size.Medium;

        public override int Speed => 30;

        public override List<Language> StartingLanguages => StaticStartingLanguages;

        public override List<Language> AvailableLanguages => StaticAvailableLanguages;

        public HashSet<Attributes> AttributesWithModifier { get; set; } = new HashSet<Attributes>();
        public int NumberOfAttributes { get; set; } = 1;
    }
}