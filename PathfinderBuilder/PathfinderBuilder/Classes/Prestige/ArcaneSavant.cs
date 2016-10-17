using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PathfinderBuilder.Classes.Prestige
{
    public class ArcaneSavant : PrestigeClass, ICaster
    {
        private readonly List<IPrerequisite> _prerequisites = new List<IPrerequisite>
        {
            new SkillRankPrerequisite(Skills.KnowledgeArcane, 5),
            new SkillRankPrerequisite(Skills.Spellcraft, 5),
            new SkillRankPrerequisite(Skills.UseMagicDevice, 5),
            new FeatPrerequisite("Magical Aptitude"),
            new FeatTypePrerequisite(typeof(IItemCreationFeat), "Item Creation Feats")
            // TODO: add caster can cast 2nd level spells
        };
        private readonly List<Saves> _myGoodSaves = new List<Saves> { Saves.Will };
        private readonly List<Skills> _myClassSkills = new List<Skills>
        {
            Skills.Appraise,
           Skills.Craft,
           Skills.KnowledgeArcane,
           Skills.KnowledgeDungeoneering,
           Skills.KnowledgeHistory,
           Skills.KnowledgeLocal,
           Skills.KnowledgeNature,
           Skills.KnowledgeNobility,
           Skills.KnowledgePlanes,
           Skills.KnowledgeReligion,
           Skills.Linguistics,
           Skills.Perception,
           Skills.Profession,
           Skills.Spellcraft,
           Skills.Survival,
           Skills.UseMagicDevice
        };
        private readonly List<IFeat> _myStartingFeats = new List<IFeat>();

        public override string ClassName
        {
            get { return "Arcane Savant"; }
        }

        public override Dice HitDie
        {
            get { return Dice.d6; }
        }

        public override int SkillPointsPerLevel
        {
            get { return 2; }
        }

        public override IReadOnlyList<Skills> ClassSkills { get { return _myClassSkills; } }

        public override BABProgression BABProgression { get { return BABProgression.Slow; } }

        public override List<Saves> GoodSaves { get { return _myGoodSaves; } }

        public override List<IFeat> StartingFeats
        {
            get { return _myStartingFeats; }
        }

        public override Type ArchtypeEnumType
        {
            get { return typeof(RacialArchtype); }
        }

        public override List<IPrerequisite> Prerequisites
        {
            get { return _prerequisites; }
        }

        public MagicType CasterType
        {
            get
            {
                return MagicType.Existing;
            }
        }
    }
}
