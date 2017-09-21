using System;
using System.Collections.Generic;

namespace PathfinderBuilder
{
    public interface IRacialTrait
    {
        string Name { get; }
        string Description { get; }
        object TraitCategory { get; }
        Type CategoryType { get; }
        int RP { get; }
    }

    public class SimpleSkillAdditionRacialTrait : IAddToSkills, IRacialTrait
    {
        private readonly Dictionary<Skills, int> _skillAddition = new Dictionary<Skills, int>();

        public SimpleSkillAdditionRacialTrait(string name, string description, IEnumerable<KeyValuePair<Skills, int>> skillAdditions, Type categoryType, object traitCategory = null)
        {
            CategoryType = categoryType;
            Name = name;
            Description = description;
            foreach (var pair in skillAdditions)
            {
                _skillAddition.Add(pair.Key, pair.Value);
            }
            TraitCategory = traitCategory ?? BaseRacialTraits.None;
        }

        public object TraitCategory { get; private set; }
        public Type CategoryType { get; private set; }
        public int RP { get; set; }
        public IDictionary<Skills, int> SkillAndModifier { get { return _skillAddition; } }
        public string Name { get; private set; }
        public string Description { get; private set; }
    }

    public class AddSkillPointsPerLevelRacialTrait : IRacialTrait, IAddsSkillPointPerLevel
    {
        public AddSkillPointsPerLevelRacialTrait(string name, string description, int pointsPerLevel, Type categoryType, object traitCategory = null)
        {
            CategoryType = categoryType;
            Name = name;
            Description = description;
            SkillPointsPerLevel = pointsPerLevel;
            TraitCategory = traitCategory ?? BaseRacialTraits.None;
        }

        public string Name { get; private set; }
        public string Description { get; private set; }
        public object TraitCategory { get; private set; }
        public Type CategoryType { get; private set; }
        public int RP { get; set; }
        public int SkillPointsPerLevel { get; private set; }
    }

    public class SimpleRacialTrait : IRacialTrait
    {

        public SimpleRacialTrait(string name, string description, Type categoryType, object traitCategory = null)
        {
            CategoryType = categoryType;
            Name = name;
            Description = description;
            TraitCategory = traitCategory ?? BaseRacialTraits.None;
        }

        public string Name { get; private set; }
        public string Description { get; private set; }
        public object TraitCategory { get; private set; }
        public Type CategoryType { get; private set; }
        public int RP { get; set; }
    }

    public class AddOptionalAbilityModifiersRacialTrait : IRacialTrait, IAddOptionalAbilityModifier
    {

        public AddOptionalAbilityModifiersRacialTrait(string name, string description, Type categoryType, int number, object traitCategory = null)
        {
            CategoryType = categoryType;
            Name = name;
            Description = description;
            Number = number;
            TraitCategory = traitCategory ?? BaseRacialTraits.None;
        }

        public string Name { get; private set; }
        public string Description { get; private set; }
        public object TraitCategory { get; private set; }
        public Type CategoryType { get; private set; }
        public int RP { get; set; }
        public int Number { get; }
    }


    public class LowLightVisionRacialTrait : IRacialTrait, ILowLightVision
    {
        public LowLightVisionRacialTrait(Type categoryType, object traitCategory)
        {
            CategoryType = categoryType;
            TraitCategory = traitCategory ?? BaseRacialTraits.None;
        }

        public int RP { get; set; }
        public string Name
        {
            get { return "Low-Light Vision"; }
        }
        public string Description { get { return "see twice as far as humans in conditions of dim light"; } }
        public object TraitCategory { get; set; }
        public Type CategoryType { get; set; }
    }

    public class FeatsAtFirstLevelRacialTrait : IRacialTrait, IFeatsAtFirstLevel
    {

        public FeatsAtFirstLevelRacialTrait(string name, string description, int numberOfFeats, Type categoryType, object traitCategory = null)
        {
            CategoryType = categoryType;
            Name = name;
            Description = description;
            NumberOfFeats = numberOfFeats;
            TraitCategory = traitCategory ?? BaseRacialTraits.None;
        }

        public int RP { get; set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public object TraitCategory { get; private set; }
        public Type CategoryType { get; private set; }
        public int NumberOfFeats { get; private set; }
    }
}
