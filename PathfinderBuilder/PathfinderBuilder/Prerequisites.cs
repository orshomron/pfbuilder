using System;
using System.Collections.Generic;
using System.Linq;

namespace PathfinderBuilder
{
    public interface IPrerequisite
    {
        bool CanUse(Character character);

        string NotMetText { get; }
    }

    public class SpecialPrerequisite : IPrerequisite
    {
        public bool CanUse(Character character)
        {
            // TODO: add handling this thingie

            return true;
        }

        public string NotMetText { get; } = string.Empty;
    }

    public class SkillRankPrerequisite : IPrerequisite
    {
        private readonly Skills _skill;
        private readonly int _rank;


        public SkillRankPrerequisite(Skills skill, int rank)
        {
            _skill = skill;
            _rank = rank;
        }

        public bool CanUse(Character character)
        {
            return character.SkillRanks.ContainsKey(_skill) && character.SkillRanks[_skill] >= _rank;
        }

        public string NotMetText { get { return string.Format("Must have at least {0} ranks in {1}", _rank, EnumHelper.GetDescription(typeof(Skills), _skill)); } }
    }

    public class FeatPrerequisite : IPrerequisite
    {
        private readonly string _featName;

        public FeatPrerequisite(string featName)
        {
            _featName = featName;
        }

        public bool CanUse(Character character)
        {
            return character.AllFeats.Count(f => f.Name == _featName) > 0;
        }

        public string NotMetText { get { return string.Format("Must have feat: {0}", _featName); } }
    }

    public class FeatTypePrerequisite : IPrerequisite
    {
        private readonly Type _type;
        private readonly string _typeName;
        private readonly int _count;

        public FeatTypePrerequisite(Type type, string typeName, int count = 1)
        {
            _type = type;
            _typeName = typeName;
            _count = count;
        }

        public bool CanUse(Character character)
        {
            return character.AllFeats.Count(f => f.GetType().IsAssignableFrom(_type)) >= _count;
        }

        public string NotMetText { get { return string.Format("Must have at least {1} feats of type: {0}", _typeName, _count); } }
    }

    public class NoPrerequisite : IPrerequisite
    {
        private static readonly NoPrerequisite MyInstance = new NoPrerequisite();

        public static NoPrerequisite Instance { get { return MyInstance; } }

        public bool CanUse(Character character)
        {
            return true;
        }

        public string NotMetText { get { return string.Empty; } }
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

        public string NotMetText { get { return string.Format("Must be caster level {0}", _minLevel); } }
    }

    public class ComplexPrerequisite : IPrerequisite
    {
        private readonly ICollection<IPrerequisite> _prerequisites;

        public ComplexPrerequisite(ICollection<IPrerequisite> prerequisites)
        {
            _prerequisites = prerequisites;
        }

        public ComplexPrerequisite(params IPrerequisite[] prerequisites)
        {
            _prerequisites = prerequisites;
        }

        public bool CanUse(Character character)
        {
            return _prerequisites.All(p => p.CanUse(character));
        }

        public string NotMetText { get { return string.Join("; ", _prerequisites.Select(p => p.NotMetText)); } }
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

        public string NotMetText
        {
            get
            {
                return string.Join("; ", _minimums.Select(m => string.Format("{0} must be at least {1}", m.Key, m.Value)));
            }
        }
    }
}
