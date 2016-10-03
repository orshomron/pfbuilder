using System.Collections.Generic;

namespace PathfinderBuilder
{
    public interface IRacialTrait
    {
        string Name { get; }
        string Description { get; }
        ICollection<IRacialTrait> ReplacedTraits { get; }
    }

    public class SimpleSkillAdditionRacialTrait : IAddToSkills, IRacialTrait
    {
        private static readonly List<IRacialTrait> NoTraits = new List<IRacialTrait>();
        private readonly Dictionary<Skills, int> _skillAddition = new Dictionary<Skills, int>();

        public SimpleSkillAdditionRacialTrait(string name, string description, IEnumerable<KeyValuePair<Skills, int>> skillAdditions)
        {
            Name = name;
            Description = description;
            foreach (var pair in skillAdditions)
            {
                _skillAddition.Add(pair.Key, pair.Value);
            }
        }

        public IDictionary<Skills, int> SkillAndModifier { get { return _skillAddition; } }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public ICollection<IRacialTrait> ReplacedTraits { get { return NoTraits; } }
    }

    public class AddSkillPointsPerLevelRacialTrait : IRacialTrait, IAddsSkillPointPerLevel
    {
        private static readonly List<IRacialTrait> NoTraits = new List<IRacialTrait>();

        public AddSkillPointsPerLevelRacialTrait(string name, string description, int pointsPerLevel)
        {
            Name = name;
            Description = description;
            SkillPointsPerLevel = pointsPerLevel;
        }

        public string Name { get; private set; }
        public string Description { get; private set; }
        public int SkillPointsPerLevel { get; private set; }
        public ICollection<IRacialTrait> ReplacedTraits { get { return NoTraits; } }
    }

    public class SimpleRacialTrait : IRacialTrait
    {
        private static readonly List<IRacialTrait> NoTraits = new List<IRacialTrait>();

        public SimpleRacialTrait(string name, string description)
        {
            Name = name;
            Description = description;
        }

        public string Name { get; private set; }
        public string Description { get; private set; }
        public ICollection<IRacialTrait> ReplacedTraits { get { return NoTraits; } }
    }

    public class LowLightVisionRacialTrait : IRacialTrait, ILowLightVision
    {
        private static readonly List<IRacialTrait> NoTraits = new List<IRacialTrait>();

        public string Name
        {
            get { return "Low-Light Vision"; }
        }
        public string Description { get { return "see twice as far as humans in conditions of dim light"; } }
        public ICollection<IRacialTrait> ReplacedTraits { get { return NoTraits; } }
    }

    public class FeatsAtFirstLevelRacialTrait : IRacialTrait, IFeatsAtFirstLevel
    {
        private static readonly List<IRacialTrait> NoTraits = new List<IRacialTrait>();

        public FeatsAtFirstLevelRacialTrait(string name, string description, int numberOfFeats)
        {
            Name = name;
            Description = description;
            NumberOfFeats = numberOfFeats;
        }

        public string Name { get; private set; }
        public string Description { get; private set; }
        public int NumberOfFeats { get; private set; }
        public ICollection<IRacialTrait> ReplacedTraits { get { return NoTraits; } }
    }
}
