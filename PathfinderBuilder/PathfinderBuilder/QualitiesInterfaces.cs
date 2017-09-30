using System.Collections.Generic;

namespace PathfinderBuilder
{
    public interface IAddOptionalAbilityModifier
    {
        int Number { get; }
    }

    public interface IAddToSkills
    {
        IDictionary<Skills, int> SkillAndModifier { get; }
    }

    public interface IAddToSkillsDependOnRank
    {
        Dictionary<Skills, Dictionary<int, int>> LevelCommulativeBonusBySkill { get; }
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

    public interface INaturalArmor
    {
        int NaturalArmorBonus { get; }
    }

    public interface INaturalArmorEnhancementBonus
    {
        int NaturalArmorEnhancementBonus { get; }
    }

    public interface IAddToEffectiveCarryCapacity
    {
        int StrIncrease { get; }
        double FlatBonus { get; }
        double FlatMultiplier { get;  }
    }
}
