using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PathfinderBuilder.Feats
{
    public class SkillFocus : IFeatWithSkillSelection, IAddToSkillsDependOnRank
    {
        private static readonly Dictionary<int, int> Bonuses = new Dictionary<int, int> { { 0, 3 }, { 10, 3 } };

        public SkillFocus(Skills skill)
        {
            LevelCommulativeBonusBySkill = new Dictionary<Skills, Dictionary<int, int>> { { skill, Bonuses } };
        }

        public SkillFocus()
        {

        }

        public IPrerequisite Prerequisite { get { return NoPrerequisite.Instance; } }

        public string Name => Skill == Skills.INVALID ? NameWithoutSkill : $"Skill Focus [{EnumHelper.GetDescription(typeof(Skills), Skill)}]";

        public string Description { get { return "+3 bonus on skill. additional +3 if 10 ranks or more."; } }
        public Dictionary<Skills, Dictionary<int, int>> LevelCommulativeBonusBySkill { get; private set; }

        public void SetSkill(Skills skill)
        {
            LevelCommulativeBonusBySkill = new Dictionary<Skills, Dictionary<int, int>> { { skill, Bonuses } };
            Skill = skill;
        }

        public Skills Skill { get; private set; } = Skills.INVALID;
        public string NameWithoutSkill { get; } = "Skill Focus";
    }

    public class MagicalAptitudeFeat : IFeat, IAddToSkillsDependOnRank
    {
        private static readonly Dictionary<int, int> Bonuses = new Dictionary<int, int> { { 0, 2 }, { 10, 2 } };

        public MagicalAptitudeFeat()
        {
            LevelCommulativeBonusBySkill = new Dictionary<Skills, Dictionary<int, int>> { { Skills.UseMagicDevice, Bonuses }, { Skills.Spellcraft, Bonuses } };
        }

        public IPrerequisite Prerequisite { get { return NoPrerequisite.Instance; } }
        public string Name { get { return "Magical Aptitude"; } }
        public string Description { get { return "+2 bonus on spellcraft and use magic device. additional +2 if 10 ranks or more."; } }
        public Dictionary<Skills, Dictionary<int, int>> LevelCommulativeBonusBySkill { get; private set; }
    }
}
