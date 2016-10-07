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

        public override string Type
        {
            get { return "Humanoid"; }
        }

        public override List<string> Subtypes
        {
            get { return new List<string> { "Human", "Orc" }; }
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

        public Attributes SelectedAttribute { get; set; }

        public override string RaceName
        {
            get { return "Half-Orc"; }
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