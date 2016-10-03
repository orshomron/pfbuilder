using System.Collections.Generic;

namespace PathfinderBuilder
{
    public interface IAddToSkills
    {
        IDictionary<Skills, int> SkillAndModifier { get; }
    }

    public interface IAddToSkillDependOnRank
    {
        Skills Skill { get; }

        Dictionary<int, int> LevelCommulativeBonus { get; }
    }

    public interface IDoubleLinguistics
    {

    }

    public interface IAddImmunity
    {
        ICollection<string> Immunities { get; }
    }

    public interface IAddFort
    {
        int FortModifier { get; }
    }

    public interface IAddReflex
    {
        int ReflexModifier { get; }
    }

    public interface IAddWill
    {
        int WillModifier { get; }
    }

    public interface IEnergyResistance
    {
        IDictionary<EnergyTypes, int> EnergyResistances { get; }
    }

    public interface IAddsSkillPointPerLevel
    {
        int SkillPointsPerLevel { get; }
    }

    public interface IFeatsAtFirstLevel
    {
        int NumberOfFeats { get; }
    }

    public interface ILowLightVision
    {

    }

    public interface IAddDodgeBonus
    {
        int DodgeBonus { get; }
    }

    public interface IDarkvision
    {
        /// <summary>
        /// in ft.
        /// </summary>
        int Range { get; }
    }
}
