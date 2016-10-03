using System.Collections.Generic;
using System.Linq;

namespace PathfinderBuilder
{
    public interface IPrerequisite
    {
        bool CanUse(Character character);
    }

    public class NoPrerequisite : IPrerequisite
    {
        private static readonly NoPrerequisite MyInstance = new NoPrerequisite();

        public static NoPrerequisite Instance { get { return MyInstance; } }

        public bool CanUse(Character character)
        {
            return true;
        }
    }

    public class CasterLevelPrerequisite : IPrerequisite
    {
        private readonly int _minLevel;

        public CasterLevelPrerequisite(int minLevel)
        {
            _minLevel = minLevel;
        }

        public bool CanUse(Character character)
        {
            var casterClasses = character.Levels.Where(kvp => kvp.Key is ICaster).ToList();
            if (casterClasses.Count == 0)
            {
                return false;
            }
            var casterLevel = casterClasses.Max(kvp => kvp.Value);
            return casterLevel >= _minLevel;
        }
    }

    public class ComplexPrerequisite : IPrerequisite
    {
        private readonly ICollection<IPrerequisite> _prerequisites;

        public ComplexPrerequisite(ICollection<IPrerequisite> prerequisites)
        {
            _prerequisites = prerequisites;
        }

        public bool CanUse(Character character)
        {
            return _prerequisites.All(p => p.CanUse(character));
        }
    }

    public class AbilityPrerequisite : IPrerequisite
    {
        private readonly Dictionary<Attributes, int> _minimums;

        public AbilityPrerequisite(Dictionary<Attributes, int> minimums)
        {
            _minimums = minimums;
        }

        public bool CanUse(Character character)
        {
            return _minimums.All(kvp => character.AbilityScores[kvp.Key] >= kvp.Value);
        }
    }
}
