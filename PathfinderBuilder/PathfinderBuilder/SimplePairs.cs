using System.ComponentModel.DataAnnotations;

namespace PathfinderBuilder
{
    public class DieAmountPair
    {
        public Dice Die { get; set; }

        [Range(1, 100, ErrorMessage = "Well number of dice really can't be non-natural")]
        public int Amount { get; set; }

        public EnergyTypes Type { get; set; }

        public DieAmountPair()
        {
            Die = Dice.d6;
            Amount = 1;
            Type = EnergyTypes.Physical;
        }

        public DieAmountPair(DieAmountPair p)
        {
            Die = p.Die;
            Amount = p.Amount;
            Type = p.Type;
        }

        public DieAmountPair(Dice die, int amount = 1, EnergyTypes type = EnergyTypes.Physical)
        {
            Die = die;
            Amount = amount;
            Type = type;
        }

    }

    public class AbilityScore
    {
        public Attributes Attribute { get; set; }

        public int Score { get; set; }

        public int Modifier { get; set; }
    }

    public class SaveBonusPair
    {
        public Saves Save { get; set; }

        public int Bonus { get; set; }
    }

    public class SkillPointPair
    {
        public Skills Skill { get; set; }

        public int Ranks { get; set; }
    }

    public class EnergyResistance
    {
        public EnergyResistance(EnergyTypes type, int amount = 0)
        {
            Type = type;
            Amount = amount;
        }

        public EnergyTypes Type { get; set; }

        public int Amount { get; set; }
    }
}
