namespace PathfinderBuilder
{
    public class Item
    {
        public Item(string name, double weight, double price, string description)
        {
            Name = name;
            Weight = weight;
            Price = price;
            Description = description;
        }

        /// <summary>
        /// Name of item
        /// </summary>
        public virtual string Name { get; }

        /// <summary>
        /// Description of the item
        /// </summary>
        public virtual string Description { get; }

        /// <summary>
        /// Is considered magical item
        /// </summary>
        public virtual bool Magical => false;

        /// <summary>
        /// Weight in Lb.
        /// </summary>
        public virtual double Weight { get; }
        
        /// <summary>
        /// Price in GP
        /// </summary>
        public virtual double Price { get; }
    }
}
