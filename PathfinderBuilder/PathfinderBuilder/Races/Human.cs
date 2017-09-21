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
            BonusFeat = 1ul << 1,
            Languages = 1ul << 2,
        }

        private static readonly List<Language> StaticStartingLanguages = new List<Language> { Language.Common };
        private static readonly List<Language> StaticTribalisticStartingLanguages = new List<Language> { Language.Tribal };
        private static readonly List<Language> StaticTribalisticAvailableLanguages = new List<Language> { Language.Common, Language.Giant, Language.Goblin, Language.Halfling };
        private static readonly List<Language> StaticAvailableLanguages = Enum.GetValues(typeof(Language)).Cast<Language>().Where(l => !StaticStartingLanguages.Contains(l)).ToList();

        private static readonly IRacialTrait TribalisticRacialTrait = new SimpleRacialTrait("Tribalistic", string.Empty, typeof(RacialTraitsCategories), BookSource.InnerSeaRaces, RacialTraitsCategories.Languages);

        public Human()
        {
            const RacialTraitsCategories skilledCategory = RacialTraitsCategories.Skilled;
            const RacialTraitsCategories bonusFeatCategory = RacialTraitsCategories.BonusFeat;
            const RacialTraitsCategories languagesCategory = RacialTraitsCategories.Languages;
            var enumType = typeof(RacialTraitsCategories);

            var skilledTrait = new AddSkillPointsPerLevelRacialTrait("Skilled", "Additional skill point per level", 1, enumType, BookSource.Core, skilledCategory);
            var bonusFeatTrait = new FeatsAtFirstLevelRacialTrait("Bonus Feat", "Humans select one extra feat at 1st level", 1, enumType, BookSource.Core, bonusFeatCategory);
            var languagesTrait = new SimpleRacialTrait("Human Languages", "Human can learn any non-secret language", enumType, BookSource.Core, languagesCategory);

            SelectedTraits.Add(skilledTrait);
            SelectedTraits.Add(bonusFeatTrait);
            SelectedTraits.Add(languagesTrait);

            var adoptiveParentage = new SimpleRacialTrait("Adoptive Parentage", "", enumType, BookSource.AdvancedRaceGuide, bonusFeatCategory);
            var aquaticAncestry = new SimpleRacialTrait("Aquatic Ancestry", "", enumType, BookSource.HorrorAdventures, skilledCategory);
            var awareness = new SimpleRacialTrait("Awareness", "", enumType, BookSource.InnerSeaRaces, bonusFeatCategory);
            var comprehensiveEducation = new SimpleRacialTrait("Comprehensive Education", "", enumType, BookSource.InnerSeaRaces, skilledCategory);
            var dualTalent = new AddOptionalAbilityModifiersRacialTrait("Dual Talent", "Select an additional ability score to recieve a +2 bonus", enumType, BookSource.AdvancedRaceGuide, 1, bonusFeatCategory | skilledCategory);
            var dimdweller = new SimpleRacialTrait("Dimdweller", string.Empty, enumType, BookSource.BloodOfShadows) { RP = 2 };
            var draconicHeritage = new SimpleRacialTrait("Draconic Heritage", string.Empty, enumType, BookSource.LegacyOfDragons, skilledCategory) { RP = 4 };
            var dragonScholar = new SimpleRacialTrait("Dragon Scholar", string.Empty, enumType, BookSource.LegacyOfDragons, bonusFeatCategory) { RP = 4 };
            var eyeForTalent = new SimpleRacialTrait("Eye for Talent", string.Empty, enumType, BookSource.AdvancedRaceGuide, bonusFeatCategory);
            var feyMagic = new SimpleRacialTrait("Fey Magic", string.Empty, enumType, BookSource.HeroesOfTheWild, skilledCategory) { RP = 2 };
            var focusedStudy = new SimpleRacialTrait("Focused Study", string.Empty, enumType, BookSource.AdvancedRaceGuide, bonusFeatCategory);
            var giantAncestry = new SimpleRacialTrait("Giant Ancestry", string.Empty, enumType, BookSource.HorrorAdventures, skilledCategory);
            var hof = new SimpleRacialTrait("Heart of the Fields", string.Empty, enumType, BookSource.AdvancedRaceGuide, skilledCategory);
            var hom = new SimpleRacialTrait("Heart of the Mountains", string.Empty, enumType, BookSource.AdvancedRaceGuide, skilledCategory);
            var hoSea = new SimpleRacialTrait("Heart of the Sea", string.Empty, enumType, BookSource.AdvancedRaceGuide, skilledCategory);
            var hoSlums = new SimpleRacialTrait("Heart of the Slums", string.Empty, enumType, BookSource.AdvancedRaceGuide, skilledCategory);
            var hoSnows = new SimpleRacialTrait("Heart of the Snows", string.Empty, enumType, BookSource.AdvancedRaceGuide, skilledCategory);
            var hoStreets = new SimpleRacialTrait("Heart of the Streets", string.Empty, enumType, BookSource.AdvancedRaceGuide, skilledCategory);
            var hoSun = new SimpleRacialTrait("Heart of the Sun", string.Empty, enumType, BookSource.AdvancedRaceGuide, skilledCategory);
            var how = new SimpleRacialTrait("Heart of the Wilderness", string.Empty, enumType, BookSource.AdvancedRaceGuide, skilledCategory);
            var heroic = new SimpleRacialTrait("Heroic", string.Empty, enumType, BookSource.AdvancedRaceGuide, bonusFeatCategory);
            var industrious = new SimpleRacialTrait("Industrious", string.Empty, enumType, BookSource.InnerSeaRaces, skilledCategory);
            var innovative = new SimpleRacialTrait("Innovative", string.Empty, enumType, BookSource.InnerSeaRaces, skilledCategory);
            var institutionalMemory = new SimpleRacialTrait("Institutional Memory", string.Empty, enumType, BookSource.InnerSeaRaces, skilledCategory);
            var militaryTradition = new SimpleRacialTrait("Military Tradition", string.Empty, enumType, BookSource.InnerSeaRaces, bonusFeatCategory);
            var mixedHeritage = new SimpleRacialTrait("Mixed Heritage", string.Empty, enumType, BookSource.AdvancedRaceGuide, bonusFeatCategory);
            var piety = new SimpleRacialTrait("Piety", string.Empty, enumType, BookSource.HorrorAdventures, bonusFeatCategory);
            var poisonMinion = new SimpleRacialTrait("Poison Minion", string.Empty, enumType, BookSource.BloodOfShadows, skilledCategory) { RP = 4 };
            var practicedHunter = new SimpleRacialTrait("Practiced Hunter", string.Empty, enumType, BookSource.InnerSeaRaces, skilledCategory);
            var psychicDefense = new SimpleRacialTrait("Psychic Defense", string.Empty, enumType, BookSource.HorrorAdventures, bonusFeatCategory);
            var rationalize = new SimpleRacialTrait("Rationalize", string.Empty, enumType, BookSource.HorrorAdventures, bonusFeatCategory);
            var reptilianAncestry = new SimpleRacialTrait("Reptilian Ancestry", string.Empty, enumType, BookSource.HorrorAdventures, bonusFeatCategory);
            var selfMadeFate = new SimpleRacialTrait("Self-Made Fate", string.Empty, enumType, BookSource.InnerSeaRaces, bonusFeatCategory);
            var shadowhunter = new SimpleRacialTrait("Shadowhunter", string.Empty, enumType, BookSource.BloodOfShadows, bonusFeatCategory) { RP = 2 };
            var silverTongued = new SimpleRacialTrait("Silver Tongued", string.Empty, enumType, BookSource.AdvancedRaceGuide, skilledCategory);
            var socialTies = new SimpleRacialTrait("Social Ties", string.Empty, enumType, BookSource.InnerSeaRaces, skilledCategory);
            var unstoppableMagic = new SimpleRacialTrait("Unstoppable Magic", string.Empty, enumType, BookSource.InnerSeaRaces, bonusFeatCategory);
            var wayfarer = new SimpleRacialTrait("Wayfarer", string.Empty, enumType, BookSource.InnerSeaRaces, skilledCategory);

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
            AvailableTraits.Add(TribalisticRacialTrait);
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

        public override List<Language> StartingLanguages => SelectedTraits.Contains(TribalisticRacialTrait) ? StaticTribalisticStartingLanguages : StaticStartingLanguages;

        public override List<Language> AvailableLanguages => SelectedTraits.Contains(TribalisticRacialTrait) ? StaticTribalisticAvailableLanguages : StaticAvailableLanguages;

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