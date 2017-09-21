namespace PathfinderBuilder.Feats
{
    public abstract class MagicItemCreationFeat : IItemCreationFeat
    {
        protected MagicItemCreationFeat(IPrerequisite prerequisite, string name, string description)
        {
            Prerequisite = prerequisite;
            Name = name;
            Description = description;
        }

        public IPrerequisite Prerequisite { get; }
        public string Name { get; }
        public string Description { get; }
        public BookSource Source { get; protected set; } = BookSource.Core;
    }

    public class ScribeScrollFeat : MagicItemCreationFeat
    {
        public ScribeScrollFeat() : base(new CasterLevelPrerequisite(1), "Scribe Scroll", "You can create a scroll of any spell that you know")
        {
        }
    }

    public class BrewFleshcraftingPoisonFeat : MagicItemCreationFeat
    {
        public BrewFleshcraftingPoisonFeat() : base(new ComplexPrerequisite(new CasterLevelPrerequisite(10), new SkillRankPrerequisite(Skills.CraftAlcehmy, 5)), "Brew Fleshcrafting Poison", "Learn the ancient drow art of creating fleshcrafting poisons.")
        {
            Source = BookSource.Pathfinder16EndlessNight;
        }
    }

    public class BrewPotionFeat : MagicItemCreationFeat
    {
        public BrewPotionFeat() : base(new CasterLevelPrerequisite(3), "Brew Potion", "Create magic potions")
        {
        }
    }

    public class CraftWandFeat : MagicItemCreationFeat
    {
        public CraftWandFeat() : base(new CasterLevelPrerequisite(5), "Craft Wand", "Create magic wands")
        {
        }
    }

    public class CraftRodFeat : MagicItemCreationFeat
    {
        public CraftRodFeat() : base(new CasterLevelPrerequisite(9), "Craft Rod", "Create magic rods")
        {
        }
    }

    public class CraftStaffFeat : MagicItemCreationFeat
    {
        public CraftStaffFeat() : base(new CasterLevelPrerequisite(11), "Craft Staff", "Create magic staves")
        {
        }
    }

    public class CraftMagicArmsAndArmorFeat : MagicItemCreationFeat
    {
        public CraftMagicArmsAndArmorFeat() : base(new CasterLevelPrerequisite(5), "Craft Magic Arms and Armor", "Create magic armors, shields, and weapons")
        {
        }
    }

    public class CraftWondrousItemFeat : MagicItemCreationFeat
    {
        public CraftWondrousItemFeat() : base(new CasterLevelPrerequisite(3), "Craft Wondrous Item", "Create magic wondrous items")
        {
        }
    }

    public class CraftConstructFeat : MagicItemCreationFeat
    {
        public CraftConstructFeat() : base(new ComplexPrerequisite(new CasterLevelPrerequisite(5), new FeatPrerequisite("Craft Wondrous Item"), new FeatPrerequisite("Craft Magic Arms and Armor")),
            "Craft Construct", "You can create any construct whose prerequisites you meet.")
        {
        }
    }

    public class CraftOozeFeat : MagicItemCreationFeat
    {
        public CraftOozeFeat() : base(new ComplexPrerequisite(new CasterLevelPrerequisite(5), new FeatPrerequisite("Craft Wondrous Item"), new FeatPrerequisite("Brew Potion"), new SkillRankPrerequisite(Skills.CraftAlcehmy, 3)),
            "Craft Ooze", "You can create any construct whose prerequisites you meet.")
        {
            Source = BookSource.AlchemyManual;
        }
    }

    public class FleshwarperFeat : MagicItemCreationFeat
    {
        public FleshwarperFeat() : base(new ComplexPrerequisite(new SkillRankPrerequisite(Skills.CraftAlcehmy, 5), new SkillRankPrerequisite(Skills.Heal, 5)), // TODO: add evil alignment pre
            "Fleshwarper", "You can create fleshwarped creatures and fleshcraft grafts. A newly created fleshwarped creature has average hit points for its Hit Dice.")
        {
            Source = BookSource.HorrorAdventures;
        }
    }

    public class ForgeRingFeat : MagicItemCreationFeat
    {
        public ForgeRingFeat() : base(new CasterLevelPrerequisite(7), "Forge Ring", "Create magic rings")
        {
        }
    }

    public class GrowPlantCreatureFeat : MagicItemCreationFeat
    {
        public GrowPlantCreatureFeat() : base(new ComplexPrerequisite(new FeatPrerequisite("Train Plants"), new SkillRankPrerequisite(Skills.HandleAnimal, 5), new SkillRankPrerequisite(Skills.KnowledgeNature, 5)),
            "Grow Plant Creature", "You can grow plants and perform rituals to invest them with animating spirits, becoming assassin vines, treants, or viper vines. Each plant creature has a list of additional prerequisites, costs, and skill checks required to grow them (see Growing Plant Creatures below). At the GM's discretion, characters with this feat may grow other plant creatures as well.")
        {
            Source = BookSource.CohortsAndCompanions;
        }
    }

    public class InscribeRuneFeat : MagicItemCreationFeat
    {
        public InscribeRuneFeat() : base(new CasterLevelPrerequisite(3), "Inscribe Rune", "Create magic runes.")
        {
            Source = BookSource.Pathfinder5SinsOfTheSaviors;
        }
    }

    public class InscribeMagicalTattooFeat : MagicItemCreationFeat
    {
        public InscribeMagicalTattooFeat() : base(new ComplexPrerequisite(new CasterLevelPrerequisite(5), new SkillRankPrerequisite(Skills.CraftMisc, 5), new SpecialPrerequisite()), // TODO: change to correct craft handling
            "Inscribe Magical Tattoo", "Create magic tattoos")
        {
            Source = BookSource.InnerSeaMagic;
        }
    }
}