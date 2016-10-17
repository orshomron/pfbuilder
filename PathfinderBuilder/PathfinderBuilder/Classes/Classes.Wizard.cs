using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace PathfinderBuilder.Classes
{
    public class Wizard : ClassBase, ICaster
    {
        [Flags]
        public enum WizardArchtypes : ulong
        {
            Vanilla = 0,
            [Display(ShortName = "Arcane Bomber")]
            ArcaneBomber = 1 << 0,
            [Display(ShortName = "Exploiter Wizard")]
            ExploiterWizard = 1 << 1,
            [Display(ShortName = "Familiar Adept")]
            FamiliarAdept = 1 << 2,
            [Display(ShortName = "Pact Wizard")]
            PactWizard = 1 << 3,
            Primalist = 1 << 4,
            Scrollmaster = 1 << 5,
            [Display(ShortName = "Scroll Scholar")]
            ScrollScholar = 1 << 6,
            Shadowcaster = 1 << 7,
            [Display(ShortName = "Siege Mage")]
            SiegeMage = 1 << 8,
            Spellslinger = 1 << 9,
            [Display(ShortName = "Spell Sage")]
            SpellSage = 1 << 10,
            [Display(ShortName = "Spirit Binder")]
            SpiritBinder = 1 << 11,
            [Display(ShortName = "Spirit Whisperer")]
            SpiritWhisperer = 1 << 12,
        }

        private static readonly List<Saves> StaticGoodSaves = new List<Saves> { Saves.Will };

        private static readonly List<Skills> StaticClassSkills = new List<Skills>
        {
            Skills.Appraise,
            Skills.Craft,
            Skills.Fly,
            Skills.KnowledgeArcane,
            Skills.KnowledgeDungeoneering,
            Skills.KnowledgeHistory,
            Skills.KnowledgeLocal,
            Skills.KnowledgeNature,
            Skills.KnowledgeNobility,
            Skills.KnowledgePlanes,
            Skills.KnowledgeReligion,
            Skills.Linguistics,
            Skills.Profession,
            Skills.Spellcraft
        };

        private static readonly List<IFeat> StaticStartingFeats = new List<IFeat>
        {
            new MagicItemCreationFeat(new CasterLevelPrerequisite(1), "Scribe Scroll", "You can create a scroll of any spell that you know")
        };

        private readonly List<object> _myArchtypes = Enum.GetValues(typeof(WizardArchtypes)).Cast<WizardArchtypes>().Where(a => a != WizardArchtypes.Vanilla).Cast<object>().ToList();

        public override string ClassName
        {
            get { return "Wizard"; }
        }

        public override bool IsArchtype
        {
            get { return false; }
        }

        public override Dice HitDie { get { return Dice.d6; } }

        public override int SkillPointsPerLevel { get { return 2; } }

        public override IReadOnlyList<Skills> ClassSkills { get { return StaticClassSkills; } }

        public override BABProgression BABProgression { get { return BABProgression.Slow; } }

        public override List<Saves> GoodSaves { get { return StaticGoodSaves; } }

        public override List<IFeat> StartingFeats
        {
            get { return StaticStartingFeats; }
        }

        public override void AddArchtype(object archtypeEnum)
        {
            switch ((WizardArchtypes)archtypeEnum)
            {
                default:
                    throw new NotImplementedException();
            }
        }

        public override void RemoveArchtype(object archtypeEnum)
        {
            switch ((WizardArchtypes)archtypeEnum)
            {
                default:
                    throw new NotImplementedException();
            }
        }

        public override bool CanAddArchtype(object archtypeEnum, Character character)
        {
            return true;
        }

        public override List<object> MyArchtypes
        {
            get { return _myArchtypes; }
        }

        public override Type ArchtypeEnumType
        {
            get { return typeof(WizardArchtypes); }
        }

        public MagicType CasterType { get { return MagicType.Arcane; } }
    }
}
