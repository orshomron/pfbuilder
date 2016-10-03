using System;
using System.Collections.Generic;

namespace PathfinderBuilder
{
    public interface IFeat
    {
        IPrerequisite Prerequisite { get; }
        string Name { get; }
        string Description { get; }
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

    public class SkillFocus : IFeat, IAddToSkillDependOnRank
    {
        private static readonly Dictionary<int, int> Bonuses = new Dictionary<int, int> { { 0, 3 }, { 10, 3 } };

        public SkillFocus(Skills skill)
        {
            Skill = skill;
        }

        public IPrerequisite Prerequisite { get { return NoPrerequisite.Instance; } }
        public string Name { get { return string.Format("Skill Focus [{0}]", EnumHelper.GetDescription(typeof(Skills), Skill)); } }
        public string Description { get { return "+3 bonus on skill. additional +3 if 10 ranks or more."; } }
        public Skills Skill { get; private set; }
        public Dictionary<int, int> LevelCommulativeBonus { get { return Bonuses; } }
    }

    public class MagicItemCreationFeat : IItemCreationFeat
    {
        public MagicItemCreationFeat(IPrerequisite prerequisite, string name, string description)
        {
            Prerequisite = prerequisite;
            Name = name;
            Description = description;
        }

        public IPrerequisite Prerequisite { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
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

    public class Feat : FluffyEntity
    {
        public WeaponGroup WeaponProficiencyGiven { get; set; }

        public ArmorGroup ArmorProficiencyGiven { get; set; }

        public virtual ICollection<SkillPointPair> SkillBonuses { get; set; }

        public virtual ICollection<SkillPointPair> SkillPrerequests { get; set; }

        public virtual ICollection<Guid> FeatPrerequestsIds { get; set; }

        public virtual ICollection<Guid> PrerequsiteClassIds { get; set; }

        public virtual ICollection<NonCombatAbility> NonCombatAbilityGivenIds { get; set; }

        public virtual ICollection<CombatAbility> CombatAbilityGivenIds { get; set; }

        public int MinBaseAtkBonus { get; set; }

        public int MinLevelInClass { get; set; }

        public MagicSchool SpecificSchool { get; set; }

        public int MinStr { get; set; }
        public int MinDex { get; set; }
        public int MinCon { get; set; }
        public int MinInt { get; set; }
        public int MinWis { get; set; }
        public int MinCha { get; set; }
    }
}
