using System.Collections.Generic;

namespace PathfinderBuilder
{
    public sealed class Character
    {
        private readonly Dictionary<Attributes, int> _abilityScores = new Dictionary<Attributes, int>();
        private readonly Dictionary<Attributes, int> _abilityScoresRaw = new Dictionary<Attributes, int>();
        private readonly Dictionary<Skills, int> _skillRanks = new Dictionary<Skills, int>();
        private readonly Dictionary<Skills, int> _skillFinalModifiers = new Dictionary<Skills, int>();
        private readonly List<Language> _knownLanguages = new List<Language>();

        /// <summary>
        /// Before modifications
        /// </summary>
        public Dictionary<Attributes, int> AbilityScoresRaw { get { return _abilityScoresRaw; } }

        /// <summary>
        /// this is final values after all modifications
        /// </summary>
        public Dictionary<Attributes, int> AbilityScores
        {
            get
            {
                return _abilityScores;
            }
        }

        /// <summary>
        /// Skill ranks raw
        /// </summary>
        public Dictionary<Skills, int> SkillRanks { get { return _skillRanks; } }

        /// <summary>
        /// Skills after all modifications
        /// </summary>
        public Dictionary<Skills, int> FinalSkillModifiers { get { return _skillFinalModifiers; } }

        public Race Race { get; set; }

        public List<Language> KnownLanguages { get { return _knownLanguages; } }

        public Dictionary<ClassBase, int> Levels { get; set; }

        public Character()
        {
            Levels = new Dictionary<ClassBase, int>();
        }
    }
}