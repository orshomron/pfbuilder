using System.Collections.Generic;
using System.Linq;
using PathfinderBuilder.Races;

namespace PathfinderBuilder
{
    public sealed class Character
    {
        /// <summary>
        /// Before modifications
        /// </summary>
        public Dictionary<Attributes, byte> AbilityScoresRaw { get; } = new Dictionary<Attributes, byte>
        {
            {Attributes.Strength,     10},
            {Attributes.Dexterity,    10 },
            {Attributes.Constitution, 10 },
            {Attributes.Intelligence, 10 },
            {Attributes.Wisdom,       10 },
            {Attributes.Charisma,     10 },
        };

        /// <summary>
        /// Level bonuses to attributes
        /// </summary>
        public Dictionary<Attributes, byte> AbilityScoresLevelBonuses { get; } = new Dictionary<Attributes, byte>
        {
            {Attributes.Strength,     0},
            {Attributes.Dexterity,    0 },
            {Attributes.Constitution, 0 },
            {Attributes.Intelligence, 0 },
            {Attributes.Wisdom,       0 },
            {Attributes.Charisma,     0 },
        };

        /// <summary>
        /// Skill ranks raw
        /// </summary>
        public Dictionary<Skills, int> SkillRanks { get; } = new Dictionary<Skills, int>();

        /// <summary>
        /// Skills after all modifications
        /// </summary>
        public Dictionary<Skills, int> FinalSkillModifiers { get; } = new Dictionary<Skills, int>();

        public Race Race { get; set; } = new Human();

        public List<Language> KnownLanguages { get; } = new List<Language>();

        public Dictionary<ClassBase, int> Levels { get; set; } = new Dictionary<ClassBase, int>();

        public List<IFeat> FreeFeats { get; set; } = new List<IFeat>();

        public List<IFeat> SelectedFeats { get; set; } = new List<IFeat>();

        public IEnumerable<IFeat> AllFeats => FreeFeats.Union(SelectedFeats);

        public int GetCalculatedAttribute(Attributes attribute)
        {
            return AbilityScoresRaw[attribute] + AbilityScoresLevelBonuses[attribute] + Race.GetModifier(attribute);
        }
    }
}