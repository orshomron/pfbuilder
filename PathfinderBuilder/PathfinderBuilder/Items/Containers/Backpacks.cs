namespace PathfinderBuilder.Items.Containers
{
    public class Backpack : Item
    {
        public Backpack() : base("Backpack", 2, 2, "This leather knapsack has one large pocket that closes with a buckled strap and holds about 2 cubic feet of material. Some may have one or more smaller pockets on the sides.")
        {
        }
    }

    public class MasterworkBackpack : Backpack, IAddToEffectiveCarryCapacity
    {
        public override string Description => "This backpack has numerous pockets for storing items that might be needed while adventuring. Hooks are included for attaching items such as canteens, pouches, or even a rolled-up blanket. It has padded bands that strap across the chest and the waist to distribute its weight more evenly. Like a common backpack, it can hold about 2 cubic feet of material in its main container. When wearing a masterwork backpack, treat your Strength score as +1 higher than normal when calculating your carrying capacity.";
        public override double Price => 50;
        public override double Weight => 4;
        public override string Name => "Backpack, Masterwork";
        public int StrIncrease => 1;
        public double FlatBonus => 0;
        public double FlatMultiplier => 1;
    }
}
