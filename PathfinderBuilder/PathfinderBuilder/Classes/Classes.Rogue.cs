using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using PathfinderBuilder.Races;

namespace PathfinderBuilder.Classes
{
    public class Rogue : ClassBase
    {
        #region Archtypes
        [Flags]
        public enum RogueArchtypes : ulong
        {
            Vanilla = 0,
            Acrobat = 1 << 0,
            Bandit = 1 << 1,
            Burglar = 1 << 2,
            Carnivalist = 1 << 3,
            Chameleon = 1 << 4,
            Charlatan = 1 << 5,
            [Display(ShortName = "Counterfeit Mage")]
            CounterfeitMage = 1 << 6,
            Cutpurse = 1 << 7,
            Driver = 1 << 8,
            Investigator = 1 << 9,
            [Display(ShortName = "Knife Master")]
            KnifeMaster = 1 << 10,
            Liberator = 1 << 11,
            [Display(ShortName = "Makeshift Scrapper")]
            MakeshiftScrapper = 1 << 12,
            Pirate = 1 << 13,
            Poisoner = 1 << 14,
            Rake = 1 << 15,
            [Display(ShortName = "River Rat")]
            RiverRat = 1 << 16,
            [Display(ShortName = "Roof Runner")]
            RoofRunner = 1 << 17,
            [Display(ShortName = "Sanctified Rogue")]
            SanctifiedRogue = 1 << 18,
            Sapper = 1 << 19,
            Scavenger = 1 << 20,
            Scout = 1 << 21,
            [Display(ShortName = "Scroll Scoundrel")]
            ScrollScoundrel = 1 << 22,
            Smuggler = 1 << 23,
            Sniper = 1 << 24,
            Spy = 1 << 25,
            Survivalist = 1 << 26,
            Swashbuckler = 1 << 27,
            Swindler = 1 << 28,
            Thug = 1 << 29,
            Trapsmith = 1ul << 30,
            [Display(ShortName = "Underground Chemist")]
            UndergroundChemist = 1ul << 31,
            [Display(ShortName = "Vexing Dodger")]
            VexingDodger = 1ul << 32,

            [Display(ShortName = "Cat Burglar")]
            RacialCatBurglar = 1ul << 33 | RacialArchtype.Racial,
            [Display(ShortName = "Deadly Courtesan")]
            RacialDeadlyCourtesan = 1ul << 34 | RacialArchtype.Racial,
            [Display(ShortName = "Eldritch Raider")]
            RacialEldritchRaider = 1ul << 35 | RacialArchtype.Racial,
            [Display(ShortName = "Filcher")]
            RacialFilcher = 1ul << 36 | RacialArchtype.Racial,
            [Display(ShortName = "Kitsune Trickster")]
            RacialKitsuneTrickster = 1ul << 37 | RacialArchtype.Racial,
            [Display(ShortName = "Skulking Slayer")]
            RacialSkulkingSlayer = 1ul << 38 | RacialArchtype.Racial,
            [Display(ShortName = "Snare Setter")]
            RacialSnareSetter = 1ul << 39 | RacialArchtype.Racial,
            [Display(ShortName = "Swordmaster")]
            RacialSwordmaster = 1ul << 40 | RacialArchtype.Racial,
        }
        #endregion

        private readonly List<Saves> _myGoodSaves = new List<Saves> { Saves.Reflex };
        private readonly List<Skills> _myClassSkills = new List<Skills>
        {
            Skills.Acrobatics,
            Skills.Appraise,
            Skills.Bluff,
            Skills.Climb,
            Skills.CraftMisc,
            Skills.CraftAlcehmy,
            Skills.CraftWeapons,
            Skills.CraftTraps,
            Skills.CraftBows,
            Skills.CraftArmor,
            Skills.Diplomacy,
            Skills.DisableDevice,
            Skills.Disguise,
            Skills.EscapeArtist,
            Skills.Intimidate,
            Skills.KnowledgeDungeoneering,
            Skills.KnowledgeLocal,
            Skills.Linguistics,
            Skills.Perception,
            Skills.Perform,
            Skills.Profession,
            Skills.SenseMotive,
            Skills.SleightOfHand,
            Skills.Stealth,
            Skills.Swim,
            Skills.UseMagicDevice
        };
        private readonly List<IFeat> _myStartingFeats = new List<IFeat>();

        private const string VanillaClassName = "Rogue";
        private RogueArchtypes _myArchtype = RogueArchtypes.Vanilla;

        public override string ClassName
        {
            get
            {
                if (IsArchtype)
                {
                    var myArchtypes = Enum.GetValues(typeof(RogueArchtypes)).Cast<RogueArchtypes>()
                        .Where(archtype => _myArchtype.HasFlag(archtype))
                        .Select(e => EnumHelper.GetDescription(typeof(RogueArchtypes), e));

                    return string.Format("{0} ({1})", VanillaClassName, string.Join(", ", myArchtypes));
                }
                return VanillaClassName;
            }
        }

        public override bool IsArchtype
        {
            get { return _myArchtype != RogueArchtypes.Vanilla; }
        }

        public override Dice HitDie { get { return Dice.d8; } }

        public override int SkillPointsPerLevel { get { return 8; } }

        public override IReadOnlyList<Skills> ClassSkills { get { return _myClassSkills; } }

        public override BABProgression BABProgression { get { return BABProgression.Medium; } }

        public override List<Saves> GoodSaves { get { return _myGoodSaves; } }

        public override List<IFeat> StartingFeats
        {
            get { return _myStartingFeats; }
        }

        public override bool CanAddArchtype(object archtypeEnum, Character character)
        {
            var val = (RogueArchtypes)archtypeEnum;

            if (ArchtypeChangesEvasion(val) && ArchtypeChangesEvasion(_myArchtype))
            {
                return false;
            }
            if (ArchtypeChangesTrapSense(val) && ArchtypeChangesTrapSense(_myArchtype))
            {
                return false;
            }
            if (ArchtypeChangesTrapFinding(val) && ArchtypeChangesTrapFinding(_myArchtype))
            {
                return false;
            }
            if (ArchtypeChangesUncannyDodge(val) && ArchtypeChangesUncannyDodge(_myArchtype))
            {
                return false;
            }
            if (ArchtypeChangesClassSkills(val) && ArchtypeChangesClassSkills(_myArchtype))
            {
                return false;
            }
            if (ArchtypeChangesSneakAttack(val) && ArchtypeChangesSneakAttack(_myArchtype))
            {
                return false;
            }
            if (ArchtypeChangesTalent2(val) && ArchtypeChangesTalent2(_myArchtype))
            {
                return false;
            }
            if (ArchtypeChangesTalent4(val) && ArchtypeChangesTalent4(_myArchtype))
            {
                return false;
            }

            if (val.HasFlag(RogueArchtypes.RacialCatBurglar) && !(character.Race is Catfolk))
            {
                return false;
            }

            if (val.HasFlag(RogueArchtypes.RacialFilcher) && !(character.Race is Halfling))
            {
                return false;
            }

            if (val.HasFlag(RogueArchtypes.RacialSkulkingSlayer) && !(character.Race is HalfOrc))
            {
                return false;
            }
            if (val.HasFlag(RogueArchtypes.RacialDeadlyCourtesan))
            {
                return false;
            }
            if (val.HasFlag(RogueArchtypes.RacialEldritchRaider))
            {
                return false;
            }
            if (val.HasFlag(RogueArchtypes.RacialKitsuneTrickster))
            {
                return false;
            }
            if (val.HasFlag(RogueArchtypes.RacialSwordmaster))
            {
                return false;
            }
            if (val.HasFlag(RogueArchtypes.RacialSnareSetter))
            {
                return false;
            }

            return true;
        }

        public override IReadOnlyList<object> MyArchtypes => Enum.GetValues(typeof(RogueArchtypes)).Cast<RogueArchtypes>().Where(a => a != RogueArchtypes.Vanilla).Cast<object>().ToList();

        public override Type ArchtypeEnumType
        {
            get { return typeof(RogueArchtypes); }
        }

        public override void AddArchtype(object archtypeEnum)
        {
            switch ((RogueArchtypes)archtypeEnum)
            {
                case RogueArchtypes.Carnivalist:
                    AsCarnivalist();
                    return;
                default:
                    throw new NotImplementedException();
            }
        }

        public override void RemoveArchtype(object archtypeEnum)
        {
            switch ((RogueArchtypes)archtypeEnum)
            {
                case RogueArchtypes.Carnivalist:
                    RemoveCarnivalist();
                    return;
                default:
                    throw new NotImplementedException();
            }
        }

        private void AsCarnivalist()
        {
            _myArchtype |= RogueArchtypes.Carnivalist;
            _myClassSkills.Add(Skills.HandleAnimal);
        }

        private void RemoveCarnivalist()
        {
            _myArchtype ^= RogueArchtypes.Carnivalist;
            _myClassSkills.Remove(Skills.HandleAnimal);
        }

        #region Archtype Mix and Match rules

        private static bool ArchtypeChangesTalent4(RogueArchtypes archtype)
        {
            return archtype.HasFlag(RogueArchtypes.Carnivalist)
                || archtype.HasFlag(RogueArchtypes.CounterfeitMage)
                || archtype.HasFlag(RogueArchtypes.UndergroundChemist);
        }

        private static bool ArchtypeChangesTalent2(RogueArchtypes archtype)
        {
            return archtype.HasFlag(RogueArchtypes.Carnivalist)
                || archtype.HasFlag(RogueArchtypes.Pirate)
                || archtype.HasFlag(RogueArchtypes.ScrollScoundrel)
                || archtype.HasFlag(RogueArchtypes.RacialEldritchRaider)
                || archtype.HasFlag(RogueArchtypes.RacialDeadlyCourtesan);
        }

        private static bool ArchtypeChangesClassSkills(RogueArchtypes archtype)
        {
            return archtype.HasFlag(RogueArchtypes.Carnivalist)
                || archtype.HasFlag(RogueArchtypes.RacialSkulkingSlayer)
                || archtype.HasFlag(RogueArchtypes.RacialDeadlyCourtesan)
                || archtype.HasFlag(RogueArchtypes.RacialEldritchRaider)
                || archtype.HasFlag(RogueArchtypes.RacialSwordmaster);
        }

        private static bool ArchtypeChangesSneakAttack(RogueArchtypes archtype)
        {
            return archtype.HasFlag(RogueArchtypes.Carnivalist)
                || archtype.HasFlag(RogueArchtypes.RacialSnareSetter);
        }

        private static bool ArchtypeChangesEvasion(RogueArchtypes archtype)
        {
            return archtype.HasFlag(RogueArchtypes.UndergroundChemist)
                   || archtype.HasFlag(RogueArchtypes.RacialFilcher)
                   || archtype.HasFlag(RogueArchtypes.Smuggler);
        }

        private static bool ArchtypeChangesUncannyDodge(RogueArchtypes archtype)
        {
            return archtype.HasFlag(RogueArchtypes.Bandit)
                   || archtype.HasFlag(RogueArchtypes.Burglar)
                   || archtype.HasFlag(RogueArchtypes.SanctifiedRogue)
                   || archtype.HasFlag(RogueArchtypes.Scout)
                   || archtype.HasFlag(RogueArchtypes.ScrollScoundrel)
                   || archtype.HasFlag(RogueArchtypes.Trapsmith)
                   || archtype.HasFlag(RogueArchtypes.VexingDodger)
                   || archtype.HasFlag(RogueArchtypes.RacialCatBurglar)
                   || archtype.HasFlag(RogueArchtypes.RacialFilcher)
                   || archtype.HasFlag(RogueArchtypes.RacialDeadlyCourtesan);
        }

        private static bool ArchtypeChangesTrapFinding(RogueArchtypes archtype)
        {
            return archtype.HasFlag(RogueArchtypes.Acrobat)
                   || archtype.HasFlag(RogueArchtypes.CounterfeitMage)
                   || archtype.HasFlag(RogueArchtypes.Chameleon)
                   || archtype.HasFlag(RogueArchtypes.Investigator)
                   || archtype.HasFlag(RogueArchtypes.Charlatan)
                   || archtype.HasFlag(RogueArchtypes.Cutpurse)
                   || archtype.HasFlag(RogueArchtypes.Driver)
                   || archtype.HasFlag(RogueArchtypes.KnifeMaster)
                   || archtype.HasFlag(RogueArchtypes.Pirate)
                   || archtype.HasFlag(RogueArchtypes.Poisoner)
                   || archtype.HasFlag(RogueArchtypes.Rake)
                   || archtype.HasFlag(RogueArchtypes.RoofRunner)
                   || archtype.HasFlag(RogueArchtypes.Smuggler)
                   || archtype.HasFlag(RogueArchtypes.Sniper)
                   || archtype.HasFlag(RogueArchtypes.Spy)
                   || archtype.HasFlag(RogueArchtypes.Survivalist)
                   || archtype.HasFlag(RogueArchtypes.Swashbuckler)
                   || archtype.HasFlag(RogueArchtypes.VexingDodger)
                   || archtype.HasFlag(RogueArchtypes.Thug)
                   || archtype.HasFlag(RogueArchtypes.RacialKitsuneTrickster)
                   || archtype.HasFlag(RogueArchtypes.RacialSkulkingSlayer)
                   || archtype.HasFlag(RogueArchtypes.RacialSnareSetter);
        }

        private static bool ArchtypeChangesTrapSense(RogueArchtypes archtype)
        {
            return archtype.HasFlag(RogueArchtypes.Acrobat)
                   || archtype.HasFlag(RogueArchtypes.Carnivalist)
                   || archtype.HasFlag(RogueArchtypes.Chameleon)
                   || archtype.HasFlag(RogueArchtypes.Charlatan)
                   || archtype.HasFlag(RogueArchtypes.Cutpurse)
                   || archtype.HasFlag(RogueArchtypes.Driver)
                   || archtype.HasFlag(RogueArchtypes.KnifeMaster)
                   || archtype.HasFlag(RogueArchtypes.Pirate)
                   || archtype.HasFlag(RogueArchtypes.Poisoner)
                   || archtype.HasFlag(RogueArchtypes.Rake)
                   || archtype.HasFlag(RogueArchtypes.RoofRunner)
                   || archtype.HasFlag(RogueArchtypes.ScrollScoundrel)
                   || archtype.HasFlag(RogueArchtypes.Smuggler)
                   || archtype.HasFlag(RogueArchtypes.Sniper)
                   || archtype.HasFlag(RogueArchtypes.Spy)
                   || archtype.HasFlag(RogueArchtypes.Survivalist)
                   || archtype.HasFlag(RogueArchtypes.Swashbuckler)
                   || archtype.HasFlag(RogueArchtypes.Thug)
                   || archtype.HasFlag(RogueArchtypes.VexingDodger)
                   || archtype.HasFlag(RogueArchtypes.RacialDeadlyCourtesan)
                   || archtype.HasFlag(RogueArchtypes.RacialEldritchRaider)
                   || archtype.HasFlag(RogueArchtypes.RacialFilcher)
                   || archtype.HasFlag(RogueArchtypes.RacialKitsuneTrickster)
                   || archtype.HasFlag(RogueArchtypes.RacialSkulkingSlayer)
                   || archtype.HasFlag(RogueArchtypes.RacialSwordmaster);
        }

        #endregion
    }
}
