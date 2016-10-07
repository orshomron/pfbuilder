using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PathfinderBuilder.Races
{
    public class Catfolk : Race
    {
        private static readonly List<Language> StaticStartingLanguages = new List<Language> { Language.Common, Language.Catfolk };
        private static readonly List<Language> StaticAvailableLanguages = new List<Language> { Language.Elven, Language.Gnoll, Language.Gnome, Language.Goblin, Language.Halfling, Language.Orc, Language.Sylvan };

        [Flags]
        public enum RacialTraitsCategories : ulong
        {
            None = 0ul,
            LowLightVision = BaseRacialTraits.LowLightVision,
            [Display(ShortName = "Cat's Luck")]
            CatsLuck = 1ul << 0,
            [Display(ShortName = "Natural Hunter")]
            NaturalHunter = 1ul << 1,
            [Display(ShortName = "Sprinter")]
            Sprinter = 1ul << 2,
        }

        public Catfolk()
        {
            var type = typeof(RacialTraitsCategories);
            const RacialTraitsCategories sprinter = RacialTraitsCategories.Sprinter;
            const RacialTraitsCategories naturalHunter = RacialTraitsCategories.NaturalHunter;
            const RacialTraitsCategories lowLightVision = RacialTraitsCategories.LowLightVision;

            SelectedTraits.Add(new SimpleSkillAdditionRacialTrait("Natural Hunter", "+2 Perception, Stealth and Survival", new[] { new KeyValuePair<Skills, int>(Skills.Perception, 2), new KeyValuePair<Skills, int>(Skills.Survival, 2), new KeyValuePair<Skills, int>(Skills.Stealth, 2) }, type, naturalHunter));
            SelectedTraits.Add(new LowLightVisionRacialTrait(type, lowLightVision));
            SelectedTraits.Add(new SimpleRacialTrait("Sprinter", "gain a 10-foot racial bonus to their speed when using the charge, run, or withdraw actions", type, sprinter));
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

        public override string Type
        {
            get { return "Humanoid"; }
        }

        public override List<string> Subtypes
        {
            get { return new List<string> { "Catfolk" }; }
        }

        public override bool RaceHasOptionalAbilityModifier
        {
            get { return true; }
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
            get { return 0; }
        }

        public override int IntelligenceModifier
        {
            get { return 0; }
        }

        public override int WisdomModifier
        {
            get { return -2; }
        }

        public override int CharismaModifier
        {
            get { return 2; }
        }

        public override string RaceName
        {
            get { return "Catfolk"; }
        }
    }
}