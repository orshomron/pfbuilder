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
