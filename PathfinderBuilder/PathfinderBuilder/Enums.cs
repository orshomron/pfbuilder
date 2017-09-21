using System;
using System.ComponentModel.DataAnnotations;

namespace PathfinderBuilder
{
    public static class EnumExtensions
    {
        public static Attributes GetAttribute(this Skills skill)
        {
            switch (skill)
            {
                case Skills.Climb:
                case Skills.Swim:
                    return Attributes.Strength;
                case Skills.Acrobatics:
                case Skills.DisableDevice:
                case Skills.EscapeArtist:
                case Skills.Fly:
                case Skills.Ride:
                case Skills.SleightOfHand:
                case Skills.Stealth:
                    return Attributes.Dexterity;
                case Skills.Appraise:
                case Skills.CraftMisc:
                case Skills.CraftAlcehmy:
                case Skills.CraftWeapons:
                case Skills.CraftTraps:
                case Skills.CraftBows:
                case Skills.CraftArmor:
                case Skills.KnowledgeArcane:
                case Skills.KnowledgeReligion:
                case Skills.KnowledgePlanes:
                case Skills.KnowledgeNobility:
                case Skills.KnowledgeNature:
                case Skills.KnowledgeDungeoneering:
                case Skills.KnowledgeHistory:
                case Skills.KnowledgeLocal:
                case Skills.Linguistics:
                case Skills.Spellcraft:
                    return Attributes.Intelligence;
                case Skills.Heal:
                case Skills.Perception:
                case Skills.Profession:
                case Skills.SenseMotive:
                case Skills.Survival:
                    return Attributes.Wisdom;
                case Skills.Bluff:
                case Skills.Diplomacy:
                case Skills.Disguise:
                case Skills.HandleAnimal:
                case Skills.Intimidate:
                case Skills.Perform:
                case Skills.UseMagicDevice:
                    return Attributes.Charisma;
                case Skills.INVALID:
                    return Attributes.INVALID;
                default:
                    throw new ArgumentException("Unknown attribute for skill: " + skill, "skill");
            }
        }
    }

    [Flags]
    public enum RacialArchtype : ulong
    {
        Racial = 1ul << 63
    }

    [Flags]
    public enum BaseRacialTraits : ulong
    {
        None = 0ul,
        [Display(ShortName = "Low-Light Vision")]
        LowLightVision = 1ul << 63,
        [Display(ShortName = "Darkvision")]
        Darkvision = 1ul << 62,
        [Display(ShortName = "Weapon Familiarity")]
        WeaponFamiliarity = 1ul << 61,
        [Display(ShortName = "Keen Senses")]
        KeenSenses = 1ul << 60,
    }

    public enum MagicSchool
    {
        None = 0,
        Abjuration = 1,
        Conjuration = 2,
        Divination = 4,
        Enchantment = 8,
        Evocation = 16,
        Illusion = 32,
        Necromancy = 64,
        Transmutation = 128,
    }

    public enum BABProgression
    {
        Full = 100,
        Medium = 75,
        Slow = 50
    }

    public enum Dice
    {
        // ReSharper disable InconsistentNaming
        d4 = 4,
        d6 = 6,
        d8 = 8,
        d10 = 10,
        d12 = 12,
        d20 = 20,
        d3 = 3,
        d2 = 2,
        d100 = 100
        // ReSharper restore InconsistentNaming
    }

    public enum WeaponGroup
    {
        None,
        Simple,
        Martial,
        Exotic,
        Natural,
    }

    public enum WeaponType
    {
        [Display(Name = "Piercing")]
        Piercing,
        [Display(Name = "Slashing")]
        Slashing,
        [Display(Name = "Bludgeoning")]
        Bludgeoning,
        [Display(Name = "Bludgeoning-Piercing")]
        BludgeoningPiercing,
        [Display(Name = "Bludgeoning-Slashing")]
        BludgeoningSlashing,
        [Display(Name = "Piercing-Slashing")]
        PiercingSlashing
    }

    public enum WeaponSize
    {
        [Display(Name = "Light")]
        Light,
        [Display(Name = "One-Handed")]
        OneHanded,
        [Display(Name = "Two-Handed")]
        TwoHanded,
        [Display(Name = "Double")]
        Double
    }

    public enum ArmorGroup
    {
        [Display(Name = "None")]
        None,
        [Display(Name = "Light")]
        Light,
        [Display(Name = "Medium")]
        Medium,
        [Display(Name = "Heavy")]
        Heavy,
        [Display(Name = "Shield")]
        Shield
    }

    public enum Size
    {
        Tiny,
        Small,
        Medium,
        Large,
        Huge,
        Gargantuan,
        Colossal
    }

    public enum Language
    {
        Common,
        Elven,
        Dwarven,
        Gnome,
        Halfling,
        Orc,
        Goblin,
        Terran,
        Undercommon,
        Giant,
        Abyssal,
        Draconic,
        Celestial,
        Sylvan,
        Gnoll,
        [Display(ShortName = "Drow Sign Language")]
        DrowSignLanguage,
        Aklo,
        Infernal,
        Ignan,
        Centaur,
        Aquan,
        Auran,
        Catfolk,
        Tribal
    }

    public enum Attributes
    {
        Strength,
        Dexterity,
        Constitution,
        Intelligence,
        Wisdom,
        Charisma,

        INVALID = 99999
    }

    public enum SpellComponents
    {
        Verbal,
        Somatic,
        Material,
        DivineFocus
    }

    public enum Saves
    {
        Fortitude,
        Reflex,
        Will
    }

    public enum EnergyTypes
    {
        Fire,
        Cold,
        Air,
        Earth,
        Electricity,
        Acid,
        Sonic,
        Physical,
        Negative,
        Positive
    }

    public enum Skills
    {
        Acrobatics,
        [Display(ShortName = "Appraise")]
        Appraise,
        Bluff,
        Climb,
        [Display(ShortName = "Craft (Alcehmy)")]
        CraftAlcehmy,
        [Display(ShortName = "Craft (Armor)")]
        CraftArmor,
        [Display(ShortName = "Craft (Bows)")]
        CraftBows,
        [Display(ShortName = "Craft (Weapons)")]
        CraftWeapons,
        [Display(ShortName = "Craft (Traps)")]
        CraftTraps,
        [Display(ShortName = "Craft")]
        CraftMisc,
        Diplomacy,
        [Display(ShortName = "Disable Device")]
        DisableDevice,
        Disguise,
        [Display(ShortName = "Escape Artist")]
        EscapeArtist,
        Fly,
        [Display(ShortName = "Handle Animal")]
        HandleAnimal,
        Heal,
        Intimidate,
        [Display(ShortName = "Knowledge (Arcane)")]
        KnowledgeArcane,
        [Display(ShortName = "Knowledge (Religion)")]
        KnowledgeReligion,
        [Display(ShortName = "Knowledge (Local)")]
        KnowledgeLocal,
        [Display(ShortName = "Knowledge (Nobility)")]
        KnowledgeNobility,
        [Display(ShortName = "Knowledge (Planes)")]
        KnowledgePlanes,
        [Display(ShortName = "Knowledge (History)")]
        KnowledgeHistory,
        [Display(ShortName = "Knowledge (Nature)")]
        KnowledgeNature,
        [Display(ShortName = "Knowledge (Dungeoneering)")]
        KnowledgeDungeoneering,
        Linguistics,
        Perception,
        Perform,
        Profession,
        Ride,
        [Display(ShortName = "Sense Motive")]
        SenseMotive,
        [Display(ShortName = "Sleight of Hand")]
        SleightOfHand,
        Spellcraft,
        Stealth,
        Survival,
        Swim,
        [Display(ShortName = "Use Magic Device")]
        UseMagicDevice,

        INVALID = 99999
    }

    public enum PointBuyOptions
    {
        [Display(ShortName = "Low Fantasy (10)")]
        O10 = 10,
        [Display(ShortName = "Standard Fantasy (15)")]
        O15 = 15,
        [Display(ShortName = "High Fantasy (20)")]
        O20 = 20,
        [Display(ShortName = "Epic Fantasy (25)")]
        O25 = 25,
        [Display(ShortName = "Custom")]
        Other = int.MaxValue
    }

    public enum DurationModifier
    {
        Constant = 0,
        Round = 1,
        Minute = 10,
        Hour = 600,
        Day = 14400
    }

    public enum Gender
    {
        Male,
        Female,
        Unknown,
        Other
    }

    public enum MagicType
    {
        Divine,
        Arcane,
        Existing
    }

    public enum BookSource
    {
        Core,
        AdvancedRaceGuide,
        AdvancedPlayersGuide,
        GamemasterGuide,
        MythicAdventures,
        UltimateCampaign,
        UltimateEquipment,
        UltimateCombat,
        UltimateMagic,
        HorrorAdventures,
        Beastiary1,
        Beastiary2,
        Beastiary3,
        Beastiary4,
        Beastiary5,
        Beastiary6,

        InnerSeaRaces,
        InnerSeaMagic,

        BloodOfShadows,
        LegacyOfDragons,
        HeroesOfTheWild,
        AlchemyManual,
        CohortsAndCompanions,

        Pathfinder5SinsOfTheSaviors,
        Pathfinder16EndlessNight,
        
    }
}
