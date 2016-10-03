using System;
using System.Collections.Generic;

namespace PathfinderBuilder
{
    public interface ICaster
    {

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
}
