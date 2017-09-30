using System;
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

        public Dictionary<Item, int> Inventory { get; } = new Dictionary<Item, int>();

        public int NominalAC
        {
            get
            {
                var naturalArmor = Race.SelectedTraits.OfType<INaturalArmor>().Union(Inventory.Keys.OfType<INaturalArmor>()).Max(t => t.NaturalArmorBonus);
                var naturalArmorEnhancementBonus = Race.SelectedTraits.OfType<INaturalArmorEnhancementBonus>().Union(Inventory.Keys.OfType<INaturalArmorEnhancementBonus>()).Max(t => t.NaturalArmorEnhancementBonus);
                return 10 + GetCalculatedAbilityModifier(Attributes.Dexterity) + naturalArmor + naturalArmorEnhancementBonus;
            }
        }

        public int NominalTouchAC
        {
            get { return 10 + GetCalculatedAbilityModifier(Attributes.Dexterity); }
        }

        public int NominalFlatFootedAC
        {
            get
            {
                var naturalArmor = Race.SelectedTraits.OfType<INaturalArmor>().Union(Inventory.Keys.OfType<INaturalArmor>()).Max(t => t.NaturalArmorBonus);
                var naturalArmorEnhancementBonus = Race.SelectedTraits.OfType<INaturalArmorEnhancementBonus>().Union(Inventory.Keys.OfType<INaturalArmorEnhancementBonus>()).Max(t => t.NaturalArmorEnhancementBonus);
                return 10 + naturalArmor + naturalArmorEnhancementBonus;
            }
        }

        public void GetCarryCapacity(out double light, out double medium, out double heavy, out double lift, out double push)
        {
            var str = GetCalculatedAttribute(Attributes.Strength);

            var traits = new List<IAddToEffectiveCarryCapacity>();
            traits.AddRange(AllFeats.OfType<IAddToEffectiveCarryCapacity>());
            traits.AddRange(Race.SelectedTraits.OfType<IAddToEffectiveCarryCapacity>());
            traits.AddRange(Inventory.Keys.OfType<IAddToEffectiveCarryCapacity>());

            var effectiveStr = str + traits.Sum(e => e.StrIncrease);
            var flatBonus = traits.Sum(e => e.FlatBonus);
            var totalMultiplier = traits.Aggregate(1.0, (i, e) => e.FlatMultiplier * i);

            var t = CarryCapacityTable.Table[effectiveStr];

            light = (t.Item1 + flatBonus) * totalMultiplier;
            medium = (t.Item2 + flatBonus) * totalMultiplier;
            heavy = (t.Item3 + flatBonus) * totalMultiplier;

            lift = heavy * 2;
            push = heavy * 5;
        }

        public int GetCalculatedAttribute(Attributes attribute)
        {
            return AbilityScoresRaw[attribute] + AbilityScoresLevelBonuses[attribute] + Race.GetModifier(attribute);
        }

        private int GetCalculatedAbilityModifier(Attributes attribute)
        {
            return GetAbilityModifier(GetCalculatedAttribute(attribute));
        }

        private static int GetAbilityModifier(int ability)
        {
            return (int)Math.Floor((ability - 10) / 2.0);
        }
    }
}