using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace PathfinderBuilder.Races
{
    public class Human : Race, IRaceWithOptionalAbilityModifier
    {
        [Flags]
        public enum RacialTraitsCategories : ulong
        {
            None = 0ul,
            Skilled = 1ul << 0,
            [Display(ShortName = "Bonus Feat")]
            BonusFeat = 1ul << 1
        }

        private static readonly List<Language> StaticStartingLanguages = new List<Language> { Language.Common };
        private static readonly List<Language> StaticAvailableLanguages = Enum.GetValues(typeof(Language)).Cast<Language>().Where(l => !StaticStartingLanguages.Contains(l)).ToList();

        public Human()
        {
            const RacialTraitsCategories skilledCategory = RacialTraitsCategories.Skilled;
            const RacialTraitsCategories bonusFeatCategory = RacialTraitsCategories.BonusFeat;
            var enumType = typeof(RacialTraitsCategories);

            var skilledTrait = new AddSkillPointsPerLevelRacialTrait("Skilled", "Additional skill point per level", 1, enumType, skilledCategory);
            var bonusFeatTrait = new FeatsAtFirstLevelRacialTrait("Bonus Feat", "Humans select one extra feat at 1st level", 1, enumType, bonusFeatCategory);

            SelectedTraits.Add(skilledTrait);
            SelectedTraits.Add(bonusFeatTrait);

            var adoptiveParentage = new SimpleRacialTrait("Adoptive Parentage", "", enumType, bonusFeatCategory);
            var aquaticAncestry = new SimpleRacialTrait("Aquatic Ancestry", "", enumType, skilledCategory);
            var awareness = new SimpleRacialTrait("Awareness", "", enumType, bonusFeatCategory);
            var comprehensiveEducation = new SimpleRacialTrait("Comprehensive Education", "", enumType, skilledCategory);
            var dualTalent = new AddOptionalAbilityModifiersRacialTrait("Dual Talent", "Select an additional ability score to recieve a +2 bonus", enumType, 1, bonusFeatCategory | skilledCategory);
            var dimdweller = new SimpleRacialTrait("Dimdweller", string.Empty, enumType) { RP = 2 };
            var draconicHeritage = new SimpleRacialTrait("Draconic Heritage", string.Empty, enumType, skilledCategory) { RP = 4 };
            var dragonScholar = new SimpleRacialTrait("Dragon Scholar", string.Empty, enumType, bonusFeatCategory) { RP = 4 };
            var eyeForTalent = new SimpleRacialTrait("Eye for Talent", string.Empty, enumType, bonusFeatCategory);
            var feyMagic = new SimpleRacialTrait("Fey Magic", string.Empty, enumType, skilledCategory) { RP = 2 };
            var focusedStudy = new SimpleRacialTrait("Focused Study", string.Empty, enumType, bonusFeatCategory);
            var giantAncestry = new SimpleRacialTrait("Giant Ancestry", string.Empty, enumType, skilledCategory);
            var hof = new SimpleRacialTrait("Heart of the Fields", string.Empty, enumType, skilledCategory);
            var hom = new SimpleRacialTrait("Heart of the Mountains", string.Empty, enumType, skilledCategory);
            var hoSea = new SimpleRacialTrait("Heart of the Sea", string.Empty, enumType, skilledCategory);
            var hoSlums = new SimpleRacialTrait("Heart of the Slums", string.Empty, enumType, skilledCategory);
            var hoSnows = new SimpleRacialTrait("Heart of the Snows", string.Empty, enumType, skilledCategory);
            var hoStreets = new SimpleRacialTrait("Heart of the Streets", string.Empty, enumType, skilledCategory);
            var hoSun = new SimpleRacialTrait("Heart of the Sun", string.Empty, enumType, skilledCategory);
            var how = new SimpleRacialTrait("Heart of the Wilderness", string.Empty, enumType, skilledCategory);
            var heroic = new SimpleRacialTrait("Heroic", string.Empty, enumType, bonusFeatCategory);
            var industrious = new SimpleRacialTrait("Industrious", string.Empty, enumType, skilledCategory);
            var innovative = new SimpleRacialTrait("Innovative", string.Empty, enumType, skilledCategory);
            var institutionalMemory = new SimpleRacialTrait("Institutional Memory", string.Empty, enumType, skilledCategory);
            var militaryTradition = new SimpleRacialTrait("Military Tradition", string.Empty, enumType, bonusFeatCategory);
            var mixedHeritage = new SimpleRacialTrait("Mixed Heritage", string.Empty, enumType, bonusFeatCategory);
            var piety = new SimpleRacialTrait("Piety", string.Empty, enumType, bonusFeatCategory);
            var poisonMinion = new SimpleRacialTrait("Poison Minion", string.Empty, enumType, skilledCategory) { RP = 4 };
            var practicedHunter = new SimpleRacialTrait("Practiced Hunter", string.Empty, enumType, skilledCategory);
            var psychicDefense = new SimpleRacialTrait("Psychic Defense", string.Empty, enumType, bonusFeatCategory);
            var rationalize = new SimpleRacialTrait("Rationalize", string.Empty, enumType, bonusFeatCategory);
            var reptilianAncestry = new SimpleRacialTrait("Reptilian Ancestry", string.Empty, enumType, bonusFeatCategory);
            var selfMadeFate = new SimpleRacialTrait("Self-Made Fate", string.Empty, enumType, bonusFeatCategory);
            var shadowhunter = new SimpleRacialTrait("Shadowhunter", string.Empty, enumType, bonusFeatCategory) { RP = 2 };
            var silverTongued = new SimpleRacialTrait("Silver Tongued", string.Empty, enumType, skilledCategory);
            var socialTies = new SimpleRacialTrait("Social Ties", string.Empty, enumType, skilledCategory);
            var unstoppableMagic = new SimpleRacialTrait("Unstoppable Magic", string.Empty, enumType, bonusFeatCategory);
            var wayfarer = new SimpleRacialTrait("Wayfarer", string.Empty, enumType, skilledCategory);

            AvailableTraits.Add(adoptiveParentage);
            AvailableTraits.Add(aquaticAncestry);
            AvailableTraits.Add(awareness);
            AvailableTraits.Add(comprehensiveEducation);
            AvailableTraits.Add(dualTalent);
            AvailableTraits.Add(dimdweller);
            AvailableTraits.Add(draconicHeritage);
            AvailableTraits.Add(dragonScholar);
            AvailableTraits.Add(eyeForTalent);
            AvailableTraits.Add(feyMagic);
            AvailableTraits.Add(focusedStudy);
            AvailableTraits.Add(giantAncestry);
            AvailableTraits.Add(hof);
            AvailableTraits.Add(hom);
            AvailableTraits.Add(hoSea);
            AvailableTraits.Add(hoSlums);
            AvailableTraits.Add(hoSnows);
            AvailableTraits.Add(hoStreets);
            AvailableTraits.Add(hoSun);
            AvailableTraits.Add(how);
            AvailableTraits.Add(heroic);
            AvailableTraits.Add(industrious);
            AvailableTraits.Add(innovative);
            AvailableTraits.Add(institutionalMemory);
            AvailableTraits.Add(militaryTradition);
            AvailableTraits.Add(mixedHeritage);
            AvailableTraits.Add(piety);
            AvailableTraits.Add(poisonMinion);
            AvailableTraits.Add(practicedHunter);
            AvailableTraits.Add(psychicDefense);
            AvailableTraits.Add(rationalize);
            AvailableTraits.Add(reptilianAncestry);
            AvailableTraits.Add(selfMadeFate);
            AvailableTraits.Add(shadowhunter);
            AvailableTraits.Add(silverTongued);
            AvailableTraits.Add(socialTies);
            AvailableTraits.Add(unstoppableMagic);
            AvailableTraits.Add(wayfarer);
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
            get { return new List<string> { "Human" }; }
        }

        public override bool RaceHasOptionalAbilityModifier => true;

        public override int StrengthModifier => AttributesWithModifier.Contains(Attributes.Strength) ? 2 : 0;

        public override int DexterityModifier => AttributesWithModifier.Contains(Attributes.Dexterity) ? 2 : 0;

        public override int ConstitutionModifier => AttributesWithModifier.Contains(Attributes.Constitution) ? 2 : 0;

        public override int IntelligenceModifier => AttributesWithModifier.Contains(Attributes.Intelligence) ? 2 : 0;

        public override int WisdomModifier => AttributesWithModifier.Contains(Attributes.Wisdom) ? 2 : 0;

        public override int CharismaModifier => AttributesWithModifier.Contains(Attributes.Charisma) ? 2 : 0;

        public override string RaceName
        {
            get { return "Human"; }
        }

        public HashSet<Attributes> AttributesWithModifier { get; } = new HashSet<Attributes>();

        public int NumberOfAttributes => 1 + SelectedTraits.Sum(t => (t as IAddOptionalAbilityModifier)?.Number ?? 0);
    }
}