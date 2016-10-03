using System;
using System.ComponentModel.DataAnnotations;

namespace PathfinderBuilder
{
    public abstract class Entity : IEntity
    {
        protected Entity()
        {
            Id = Guid.NewGuid();
        }

        public Guid Id { get; set; }
    }

    public abstract class NamedEntity : Entity
    {
        [Display(Name = "Name")]
        public string Name { get; set; }
    }

    public abstract class DescribedEntity : NamedEntity
    {
        [Display(Name = "Description")]
        public string Description { get; set; }

        [Display(Name = "Short Description")]
        public string ShortDescription { get; set; }
    }

    public abstract class FluffyEntity : DescribedEntity
    {
        [Display(Name = "Fluff Sentance")]
        public virtual string Fluff { get; set; }
    }

    public interface IEntity
    {
        Guid Id { get; set; }
    }
}
