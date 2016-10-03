using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace PathfinderBuilder
{
    public abstract class Race
    {
        public abstract string Type { get; }
        public abstract List<string> Subtypes { get; }
        public abstract bool RaceHasOptionalAbilityModifier { get; }
        public abstract int StrengthModifier { get; }
        public abstract int DexterityModifier { get; }
        public abstract int ConstitutionModifier { get; }
        public abstract int IntelligenceModifier { get; }
        public abstract int WisdomModifier { get; }
        public abstract int CharismaModifier { get; }
        public abstract string RaceName { get; }
        public abstract ObservableCollection<IRacialTrait> SelectedTraits { get; }
        public abstract ObservableCollection<IRacialTrait> AvailableTraits { get; }
        public abstract Size Size { get; }
        public abstract int Speed { get; }
        public abstract List<Language> StartingLanguages { get; }
        public abstract List<Language> AvailableLanguages { get; }
    }

    public interface IRaceWithOptionalAbilityModifier
    {
        Attributes SelectedAttribute { get; set; }
    }

    public class Catfolk : Race
    {
        private static readonly List<Language> StaticStartingLanguages = new List<Language> { Language.Common, Language.Catfolk };
        private static readonly List<Language> StaticAvailableLanguages = new List<Language> { Language.Elven, Language.Gnoll, Language.Gnome, Language.Goblin, Language.Halfling, Language.Orc, Language.Sylvan };

        private readonly ObservableCollection<IRacialTrait> _selectedTraits = new ObservableCollection<IRacialTrait>();
        private readonly ObservableCollection<IRacialTrait> _availableTraits = new ObservableCollection<IRacialTrait>();

        public Catfolk()
        {
            _selectedTraits.Add(new SimpleSkillAdditionRacialTrait("Natural Hunter", "+2 Perception, Stealth and Survival", new[] { new KeyValuePair<Skills, int>(Skills.Perception, 2), new KeyValuePair<Skills, int>(Skills.Survival, 2), new KeyValuePair<Skills, int>(Skills.Stealth, 2) }));
            _selectedTraits.Add(new LowLightVisionRacialTrait());
            _selectedTraits.Add(new SimpleRacialTrait("Sprinter", "gain a 10-foot racial bonus to their speed when using the charge, run, or withdraw actions"));
        }

        public override ObservableCollection<IRacialTrait> SelectedTraits
        {
            get { return _selectedTraits; }
        }

        public override ObservableCollection<IRacialTrait> AvailableTraits
        {
            get { return _availableTraits; }
        }

        public override Size Size
        {
            get { return Size.Medium; }
        }

        public override int Speed
        {
            get { return 30; }
        }

        public override List<Language> StartingLanguages
        {
            get { return StaticStartingLanguages; }
        }

        public override List<Language> AvailableLanguages
        {
            get { return StaticAvailableLanguages; }
        }

        public override string Type
        {
            get { return "Humanoid"; }
        }

        public override List<string> Subtypes
        {
            get { return new List<string> { "Catfolk" }; }
        }

        public override bool RaceHasOptionalAbilityModifier
        {
            get { return true; }
        }

        public override int StrengthModifier
        {
            get { return 0; }
        }

        public override int DexterityModifier
        {
            get { return 2; }
        }

        public override int ConstitutionModifier
        {
            get { return 0; }
        }

        public override int IntelligenceModifier
        {
            get { return 0; }
        }

        public override int WisdomModifier
        {
            get { return -2; }
        }

        public override int CharismaModifier
        {
            get { return 2; }
        }

        public override string RaceName
        {
            get { return "Catfolk"; }
        }
    }

    public class Human : Race, IRaceWithOptionalAbilityModifier
    {
        private static readonly List<Language> StaticStartingLanguages = new List<Language> { Language.Common };
        private static readonly List<Language> StaticAvailableLanguages = Enum.GetValues(typeof(Language)).Cast<Language>().Where(l => !StaticStartingLanguages.Contains(l)).ToList();

        private readonly ObservableCollection<IRacialTrait> _selectedTraits = new ObservableCollection<IRacialTrait>();
        private readonly ObservableCollection<IRacialTrait> _availableTraits = new ObservableCollection<IRacialTrait>();

        public Human()
        {
            _selectedTraits.Add(new AddSkillPointsPerLevelRacialTrait("Skilled", "additional skill point per level", 1));
            _selectedTraits.Add(new FeatsAtFirstLevelRacialTrait("Bonus Feat", "Humans select one extra feat at 1st level", 1));
        }

        public override ObservableCollection<IRacialTrait> SelectedTraits
        {
            get { return _selectedTraits; }
        }

        public override ObservableCollection<IRacialTrait> AvailableTraits
        {
            get { return _availableTraits; }
        }

        public override Size Size
        {
            get { return Size.Medium; }
        }

        public override int Speed
        {
            get { return 30; }
        }

        public override List<Language> StartingLanguages
        {
            get { return StaticStartingLanguages; }
        }

        public override List<Language> AvailableLanguages
        {
            get { return StaticAvailableLanguages; }
        }

        public override string Type
        {
            get { return "Humanoid"; }
        }

        public override List<string> Subtypes
        {
            get { return new List<string> { "Human" }; }
        }

        public override bool RaceHasOptionalAbilityModifier
        {
            get { return true; }
        }

        public override int StrengthModifier
        {
            get { return SelectedAttribute == Attributes.Strength ? 2 : 0; }
        }

        public override int DexterityModifier
        {
            get { return SelectedAttribute == Attributes.Dexterity ? 2 : 0; }
        }

        public override int ConstitutionModifier
        {
            get { return SelectedAttribute == Attributes.Constitution ? 2 : 0; }
        }

        public override int IntelligenceModifier
        {
            get { return SelectedAttribute == Attributes.Intelligence ? 2 : 0; }
        }

        public override int WisdomModifier
        {
            get { return SelectedAttribute == Attributes.Wisdom ? 2 : 0; }
        }

        public override int CharismaModifier
        {
            get { return SelectedAttribute == Attributes.Charisma ? 2 : 0; }
        }

        public override string RaceName
        {
            get { return "Human"; }
        }

        public Attributes SelectedAttribute { get; set; }
    }

    public class HalfElf : Race, IRaceWithOptionalAbilityModifier
    {
        private static readonly List<Language> StaticStartingLanguages = new List<Language> { Language.Common, Language.Elven };
        private static readonly List<Language> StaticAvailableLanguages = Enum.GetValues(typeof(Language)).Cast<Language>().Where(l => !StaticStartingLanguages.Contains(l)).ToList();

        private readonly ObservableCollection<IRacialTrait> _selectedTraits = new ObservableCollection<IRacialTrait>();
        private readonly ObservableCollection<IRacialTrait> _availableTraits = new ObservableCollection<IRacialTrait>();

        public HalfElf()
        {
            _selectedTraits.Add(new SimpleRacialTrait("Elf Blood", "Half-elves count as both elves and humans for any effect related to race"));
            _selectedTraits.Add(new LowLightVisionRacialTrait());
        }

        public override ObservableCollection<IRacialTrait> SelectedTraits
        {
            get { return _selectedTraits; }
        }

        public override ObservableCollection<IRacialTrait> AvailableTraits
        {
            get { return _availableTraits; }
        }

        public override bool RaceHasOptionalAbilityModifier
        {
            get { return true; }
        }

        public override int StrengthModifier
        {
            get { return SelectedAttribute == Attributes.Strength ? 2 : 0; }
        }

        public override int DexterityModifier
        {
            get { return SelectedAttribute == Attributes.Dexterity ? 2 : 0; }
        }

        public override int ConstitutionModifier
        {
            get { return SelectedAttribute == Attributes.Constitution ? 2 : 0; }
        }

        public override int IntelligenceModifier
        {
            get { return SelectedAttribute == Attributes.Intelligence ? 2 : 0; }
        }

        public override int WisdomModifier
        {
            get { return SelectedAttribute == Attributes.Wisdom ? 2 : 0; }
        }

        public override int CharismaModifier
        {
            get { return SelectedAttribute == Attributes.Charisma ? 2 : 0; }
        }

        public override string RaceName
        {
            get { return "Half-Elf"; }
        }

        public Attributes SelectedAttribute { get; set; }

        public override string Type
        {
            get { return "Humanoid"; }
        }

        public override List<string> Subtypes
        {
            get { return new List<string> { "Elf", "Human" }; }
        }

        public override Size Size
        {
            get { return Size.Medium; }
        }

        public override int Speed
        {
            get { return 30; }
        }

        public override List<Language> StartingLanguages
        {
            get { return StaticStartingLanguages; }
        }

        public override List<Language> AvailableLanguages
        {
            get { return StaticAvailableLanguages; }
        }
    }

    public class Elf : Race
    {
        private static readonly List<Language> StaticStartingLanguages = new List<Language> { Language.Common, Language.Elven };
        private static readonly List<Language> StaticAvailableLanguages = new List<Language> { Language.Celestial, Language.Draconic, Language.Gnoll, Language.Gnome, Language.Goblin, Language.Orc, Language.Sylvan };

        private readonly ObservableCollection<IRacialTrait> _selectedTraits = new ObservableCollection<IRacialTrait>();
        private readonly ObservableCollection<IRacialTrait> _availableTraits = new ObservableCollection<IRacialTrait>();

        public override ObservableCollection<IRacialTrait> SelectedTraits
        {
            get { return _selectedTraits; }
        }

        public override ObservableCollection<IRacialTrait> AvailableTraits
        {
            get { return _availableTraits; }
        }

        public Elf()
        {
            _selectedTraits.Add(new SimpleSkillAdditionRacialTrait("Keen Senses", "+2 Perception", new[] { new KeyValuePair<Skills, int>(Skills.Perception, 2) }));
        }

        public override bool RaceHasOptionalAbilityModifier
        {
            get { return false; }
        }

        public override int StrengthModifier
        {
            get { return 0; }
        }

        public override int DexterityModifier
        {
            get { return 2; }
        }

        public override int ConstitutionModifier
        {
            get { return -2; }
        }

        public override int IntelligenceModifier
        {
            get { return 2; }
        }

        public override int WisdomModifier
        {
            get { return 0; }
        }

        public override int CharismaModifier
        {
            get { return 0; }
        }

        public override string RaceName
        {
            get { return "Elf"; }
        }

        public override string Type
        {
            get { return "Humanoid"; }
        }

        public override List<string> Subtypes
        {
            get { return new List<string> { "Elf" }; }
        }

        public override Size Size
        {
            get { return Size.Medium; }
        }

        public override int Speed
        {
            get { return 30; }
        }

        public override List<Language> StartingLanguages
        {
            get { return StaticStartingLanguages; }
        }

        public override List<Language> AvailableLanguages
        {
            get { return StaticAvailableLanguages; }
        }
    }

    public class Gnome : Race
    {
        private static readonly List<Language> StaticStartingLanguages = new List<Language> { Language.Common, Language.Gnome, Language.Sylvan };
        private static readonly List<Language> StaticAvailableLanguages = new List<Language> { Language.Draconic, Language.Dwarven, Language.Elven, Language.Giant, Language.Goblin, Language.Orc };

        private readonly ObservableCollection<IRacialTrait> _selectedTraits = new ObservableCollection<IRacialTrait>();
        private readonly ObservableCollection<IRacialTrait> _availableTraits = new ObservableCollection<IRacialTrait>();

        public override ObservableCollection<IRacialTrait> SelectedTraits
        {
            get { return _selectedTraits; }
        }

        public override ObservableCollection<IRacialTrait> AvailableTraits
        {
            get { return _availableTraits; }
        }

        public Gnome()
        {
            _selectedTraits.Add(new SimpleSkillAdditionRacialTrait("Keen Senses", "+2 Perception", new[] { new KeyValuePair<Skills, int>(Skills.Perception, 2) }));
        }

        public override bool RaceHasOptionalAbilityModifier
        {
            get { return false; }
        }

        public override int StrengthModifier
        {
            get { return -2; }
        }

        public override int DexterityModifier
        {
            get { return 0; }
        }

        public override int ConstitutionModifier
        {
            get { return 2; }
        }

        public override int IntelligenceModifier
        {
            get { return 0; }
        }

        public override int WisdomModifier
        {
            get { return 0; }
        }

        public override int CharismaModifier
        {
            get { return 2; }
        }

        public override string RaceName
        {
            get { return "Gnome"; }
        }

        public override string Type
        {
            get { return "Humanoid"; }
        }

        public override List<string> Subtypes
        {
            get { return new List<string> { "Gnome" }; }
        }

        public override Size Size
        {
            get { return Size.Small; }
        }

        public override int Speed
        {
            get { return 20; }
        }

        public override List<Language> StartingLanguages
        {
            get { return StaticStartingLanguages; }
        }

        public override List<Language> AvailableLanguages
        {
            get { return StaticAvailableLanguages; }
        }
    }

    public class Dwarf : Race
    {
        private static readonly List<Language> StaticStartingLanguages = new List<Language> { Language.Common, Language.Dwarven };
        private static readonly List<Language> StaticAvailableLanguages = new List<Language> { Language.Giant, Language.Gnome, Language.Goblin, Language.Orc, Language.Terran, Language.Undercommon };

        private readonly ObservableCollection<IRacialTrait> _selectedTraits = new ObservableCollection<IRacialTrait>();
        private readonly ObservableCollection<IRacialTrait> _availableTraits = new ObservableCollection<IRacialTrait>();

        public override ObservableCollection<IRacialTrait> SelectedTraits
        {
            get { return _selectedTraits; }
        }

        public override ObservableCollection<IRacialTrait> AvailableTraits
        {
            get { return _availableTraits; }
        }

        public override bool RaceHasOptionalAbilityModifier
        {
            get { return false; }
        }

        public override int StrengthModifier
        {
            get { return 0; }
        }

        public override int DexterityModifier
        {
            get { return 0; }
        }

        public override int ConstitutionModifier
        {
            get { return 2; }
        }

        public override int IntelligenceModifier
        {
            get { return 0; }
        }

        public override int WisdomModifier
        {
            get { return 2; }
        }

        public override int CharismaModifier
        {
            get { return -2; }
        }

        public override string RaceName
        {
            get { return "Dwarf"; }
        }

        public override string Type
        {
            get { return "Humanoid"; }
        }

        public override List<string> Subtypes
        {
            get { return new List<string> { "Dwarf" }; }
        }

        public override Size Size
        {
            get { return Size.Medium; }
        }

        public override int Speed
        {
            get { return 20; }
        }

        public override List<Language> StartingLanguages
        {
            get { return StaticStartingLanguages; }
        }

        public override List<Language> AvailableLanguages
        {
            get { return StaticAvailableLanguages; }
        }
    }

    public class Halfling : Race
    {
        private static readonly List<Language> StaticStartingLanguages = new List<Language> { Language.Common, Language.Halfling };
        private static readonly List<Language> StaticAvailableLanguages = new List<Language> { Language.Dwarven, Language.Elven, Language.Gnome, Language.Goblin };

        private readonly ObservableCollection<IRacialTrait> _selectedTraits = new ObservableCollection<IRacialTrait>();
        private readonly ObservableCollection<IRacialTrait> _availableTraits = new ObservableCollection<IRacialTrait>();

        public override ObservableCollection<IRacialTrait> SelectedTraits
        {
            get { return _selectedTraits; }
        }

        public override ObservableCollection<IRacialTrait> AvailableTraits
        {
            get { return _availableTraits; }
        }

        public override bool RaceHasOptionalAbilityModifier
        {
            get { return false; }
        }

        public override int StrengthModifier
        {
            get { return -2; }
        }

        public override int DexterityModifier
        {
            get { return 2; }
        }

        public override int ConstitutionModifier
        {
            get { return 0; }
        }

        public override int IntelligenceModifier
        {
            get { return 0; }
        }

        public override int WisdomModifier
        {
            get { return 0; }
        }

        public override int CharismaModifier
        {
            get { return 2; }
        }

        public override string RaceName
        {
            get { return "Halfling"; }
        }

        public override string Type
        {
            get { return "Humanoid"; }
        }

        public override List<string> Subtypes
        {
            get { return new List<string> { "Halfling" }; }
        }

        public override Size Size
        {
            get { return Size.Small; }
        }

        public override int Speed
        {
            get { return 20; }
        }

        public override List<Language> StartingLanguages
        {
            get { return StaticStartingLanguages; }
        }

        public override List<Language> AvailableLanguages
        {
            get { return StaticAvailableLanguages; }
        }
    }

    public class HalfOrc : Race, IRaceWithOptionalAbilityModifier
    {
        private static readonly List<Language> StaticStartingLanguages = new List<Language> { Language.Common, Language.Orc };
        private static readonly List<Language> StaticAvailableLanguages = new List<Language> { Language.Abyssal, Language.Draconic, Language.Giant, Language.Gnoll, Language.Goblin };

        private readonly ObservableCollection<IRacialTrait> _selectedTraits = new ObservableCollection<IRacialTrait>();
        private readonly ObservableCollection<IRacialTrait> _availableTraits = new ObservableCollection<IRacialTrait>();

        public override ObservableCollection<IRacialTrait> SelectedTraits
        {
            get { return _selectedTraits; }
        }

        public override ObservableCollection<IRacialTrait> AvailableTraits
        {
            get { return _availableTraits; }
        }

        public HalfOrc()
        {
            _selectedTraits.Add(new SimpleSkillAdditionRacialTrait("Intimidating", "+2 Intimidate", new[] { new KeyValuePair<Skills, int>(Skills.Intimidate, 2) }));
            _selectedTraits.Add(new SimpleRacialTrait("Orc Blood", "Half-orcs count as both humans and orcs for any effect related to race"));
        }

        public override string Type
        {
            get { return "Humanoid"; }
        }

        public override List<string> Subtypes
        {
            get { return new List<string> { "Human", "Orc" }; }
        }

        public override bool RaceHasOptionalAbilityModifier
        {
            get { return true; }
        }

        public override int StrengthModifier
        {
            get { return SelectedAttribute == Attributes.Strength ? 2 : 0; }
        }

        public override int DexterityModifier
        {
            get { return SelectedAttribute == Attributes.Dexterity ? 2 : 0; }
        }

        public override int ConstitutionModifier
        {
            get { return SelectedAttribute == Attributes.Constitution ? 2 : 0; }
        }

        public override int IntelligenceModifier
        {
            get { return SelectedAttribute == Attributes.Intelligence ? 2 : 0; }
        }

        public override int WisdomModifier
        {
            get { return SelectedAttribute == Attributes.Wisdom ? 2 : 0; }
        }

        public override int CharismaModifier
        {
            get { return SelectedAttribute == Attributes.Charisma ? 2 : 0; }
        }

        public Attributes SelectedAttribute { get; set; }

        public override string RaceName
        {
            get { return "Half-Orc"; }
        }

        public override Size Size
        {
            get { return Size.Medium; }
        }

        public override int Speed
        {
            get { return 30; }
        }

        public override List<Language> StartingLanguages
        {
            get { return StaticStartingLanguages; }
        }

        public override List<Language> AvailableLanguages
        {
            get { return StaticAvailableLanguages; }
        }
    }
}
