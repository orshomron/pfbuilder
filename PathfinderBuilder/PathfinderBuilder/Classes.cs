using System;
using System.Collections.Generic;
using System.Linq;

namespace PathfinderBuilder
{
    public interface ICaster
    {
        MagicType CasterType { get; }
    }

    public abstract class ClassBase
    {
        public abstract string ClassName { get; }

        public abstract bool IsArchtype { get; }

        public abstract Dice HitDie { get; }

        public abstract int SkillPointsPerLevel { get; }

        public abstract IReadOnlyList<Skills> ClassSkills { get; }

        public abstract BABProgression BABProgression { get; }

        public abstract List<Saves> GoodSaves { get; }

        public abstract List<IFeat> StartingFeats { get; }

        public abstract void AddArchtype(object archtypeEnum);

        public abstract void RemoveArchtype(object archtypeEnum);

        public abstract bool CanAddArchtype(object archtypeEnum, Character character);

        public abstract List<object> MyArchtypes { get; }

        public abstract Type ArchtypeEnumType { get; }
    }

    public abstract class PrestigeClass : ClassBase
    {
        public abstract List<IPrerequisite> Prerequisites { get; }
        private readonly List<object> _myArchtypesEmpty = new List<object>();

        public sealed override bool IsArchtype
        {
            get { return false; }
        }

        public sealed override bool CanAddArchtype(object archtypeEnum, Character character)
        {
            return false;
        }

        public sealed override void AddArchtype(object archtypeEnum)
        {
            throw new InvalidOperationException("Prestige classes don't have archtypes");
        }

        public override sealed void RemoveArchtype(object archtypeEnum)
        {
            throw new InvalidOperationException("Prestige classes don't have archtypes");
        }

        public bool CanAddClass(Character @char)
        {
            return Prerequisites.All(c => c.CanUse(@char));
        }

        public override List<object> MyArchtypes
        {
            get { return _myArchtypesEmpty; }
        }
    }
}
