using System;
using System.Collections.Generic;
using System.Linq;

namespace PathfinderBuilder
{
    public interface IFeat
    {
        IPrerequisite Prerequisite { get; }
        string Name { get; }
        string Description { get; }
    }

    public interface IFeatWithSkillSelection : IFeat
    {
        void SetSkill(Skills skill);
        Skills Skill { get; }
        string NameWithoutSkill { get; }
    }

    public interface IItemCreationFeat : IFeat
    {

    }

    public interface ICombatFeat : IFeat
    {

    }

    public interface IGritPanacheFeat : IFeat
    {

    }

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

    public class Dodge : IFeat, IAddDodgeBonus
    {
        public Dodge()
        {
            Prerequisite = new AbilityPrerequisite(new Dictionary<Attributes, int> { { Attributes.Dexterity, 13 } });
        }

        public IPrerequisite Prerequisite { get; private set; }
        public string Name { get { return "Dodge"; } }
        public string Description { get { return "+1 Dodge AC bonus"; } }
        public int DodgeBonus { get { return 1; } }
    }
}
