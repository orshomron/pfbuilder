using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Globalization;
using System.Linq;
using GUI.Commands;
using PathfinderBuilder;
using PathfinderBuilder.Races;

namespace GUI.ViewModels
{
    public class RaceViewModel : BaseViewModel
    {
        private readonly ObservableCollection<Race> _myRaceInstances = new ObservableCollection<Race>
        {
            new Dwarf(),
            new Elf(),
            new Gnome(),
            new HalfElf(),
            new Halfling(),
            new HalfOrc(),
            new Human(),
            new Catfolk()
        };

        private Race _race;
        private readonly CharacterViewModel _owner;
        private ObservableCollection<GenericRacialTraitViewModel> _availableTraits;
        private ObservableCollection<GenericRacialTraitViewModel> _selectedTraits;
        private GenericRacialTraitViewModel _selectedAvailableTrait;
        private GenericRacialTraitViewModel _selectedSelectedTrait;
        private readonly RacialTraitCommand _moveFromAvailableToSelectedCommand;
        private RemoveRacialTraitCommand _removeSelectedTraitCommand;

        public RaceViewModel(CharacterViewModel owner)
        {
            _owner = owner;
            Race = _myRaceInstances.First();
            _moveFromAvailableToSelectedCommand = new RacialTraitCommand(this);
            _removeSelectedTraitCommand = new RemoveRacialTraitCommand(this);
        }

        public int IntelligenceModifier
        {
            get
            {
                return _race.IntelligenceModifier;
            }
        }

        public int WisdomModifier
        {
            get
            {
                return _race.WisdomModifier;
            }
        }

        public int CharismaModifier
        {
            get
            {
                return _race.CharismaModifier;
            }
        }

        public int ConstitutionModifier
        {
            get
            {
                return _race.ConstitutionModifier;
            }
        }

        public int DexterityModifier
        {
            get
            {
                return _race.DexterityModifier;
            }
        }

        public int StrengthModifier
        {
            get
            {
                return _race.StrengthModifier;
            }
        }

        public Attributes SelectedOptionalAbilityModifier
        {
            get
            {
                var raceOptional = Race as IRaceWithOptionalAbilityModifier;
                if (raceOptional == null)
                {
                    return Attributes.Strength;
                }
                return raceOptional.SelectedAttribute;
            }
            set
            {
                var raceOptional = Race as IRaceWithOptionalAbilityModifier;
                if (raceOptional == null)
                {
                    throw new ArgumentException("Invalid race recieved at optional ability modifier race");
                }
                raceOptional.SelectedAttribute = value;
                OnPropertyChanged();
                OnPropertyChanged("StrengthModifier");
                OnPropertyChanged("DexterityModifier");
                OnPropertyChanged("ConstitutionModifier");
                OnPropertyChanged("IntelligenceModifier");
                OnPropertyChanged("WisdomModifier");
                OnPropertyChanged("CharismaModifier");
                OnPropertyChanged("AbilityModifiersString");
            }
        }

        public bool RaceHasOptionalAbilityModifier
        {
            get { return _race.RaceHasOptionalAbilityModifier; }
        }

        public Race Race
        {
            get
            {
                return _race;
            }
            set
            {
                if (_selectedTraits != null)
                {
                    _selectedTraits.CollectionChanged -= TraitsChanged;
                }
                if (_availableTraits != null)
                {
                    _availableTraits.CollectionChanged -= TraitsChanged;
                }

                _race = value;
                _owner.Character.Race = value;

                _selectedTraits = new ObservableCollection<GenericRacialTraitViewModel>(_race.SelectedTraits.Select(t => new GenericRacialTraitViewModel(t, this)));
                _availableTraits = new ObservableCollection<GenericRacialTraitViewModel>(_race.AvailableTraits.Select(t => new GenericRacialTraitViewModel(t, this)));

                _selectedTraits.CollectionChanged += TraitsChanged;
                _availableTraits.CollectionChanged += TraitsChanged;

                OnPropertyChanged();
                OnPropertyChanged("RaceHasOptionalAbilityModifier");
                OnPropertyChanged("StrengthModifier");
                OnPropertyChanged("DexterityModifier");
                OnPropertyChanged("ConstitutionModifier");
                OnPropertyChanged("IntelligenceModifier");
                OnPropertyChanged("WisdomModifier");
                OnPropertyChanged("CharismaModifier");
                OnPropertyChanged("SelectedOptionalAbilityModifier");
                OnPropertyChanged("Size");
                OnPropertyChanged("AbilityModifiersString");
                OnPropertyChanged("SelectedTraits");
                OnPropertyChanged("AvailableTraits");
            }
        }

        private void TraitsChanged(object sender, NotifyCollectionChangedEventArgs args)
        {
            _race.SelectedTraits = new List<IRacialTrait>(_selectedTraits.Select(t => t.Trait));
            _race.AvailableTraits = new List<IRacialTrait>(_availableTraits.Select(t => t.Trait));
            OnPropertyChanged("TotalRP");
        }

        public ObservableCollection<Race> RacesList { get { return _myRaceInstances; } }

        public Size Size
        {
            get { return _race.Size; }
        }

        public ObservableCollection<GenericRacialTraitViewModel> AvailableTraits
        {
            get { return _availableTraits; }
        }

        public ObservableCollection<GenericRacialTraitViewModel> SelectedTraits
        {
            get { return _selectedTraits; }
        }

        public string AbilityModifiersString
        {
            get
            {
                var components = new List<Tuple<string, int>>();

                if (StrengthModifier != 0)
                {
                    components.Add(new Tuple<string, int>(StrengthModifier.ToString("+0;-0") + " Str", StrengthModifier));
                }
                if (DexterityModifier != 0)
                {
                    components.Add(new Tuple<string, int>(DexterityModifier.ToString("+0;-0") + " Dex", StrengthModifier));
                }
                if (ConstitutionModifier != 0)
                {
                    components.Add(new Tuple<string, int>(ConstitutionModifier.ToString("+0;-0") + " Con", StrengthModifier));
                }
                if (IntelligenceModifier != 0)
                {
                    components.Add(new Tuple<string, int>(IntelligenceModifier.ToString("+0;-0") + " Int", StrengthModifier));
                }
                if (WisdomModifier != 0)
                {
                    components.Add(new Tuple<string, int>(WisdomModifier.ToString("+0;-0") + " Wis", StrengthModifier));
                }
                if (CharismaModifier != 0)
                {
                    components.Add(new Tuple<string, int>(CharismaModifier.ToString("+0;-0") + " Cha", StrengthModifier));
                }

                return string.Join(", ", components.OrderBy(t => t.Item2).Select(t => t.Item1));
            }
        }

        public GenericRacialTraitViewModel SelectedAvailableTrait
        {
            get { return _selectedAvailableTrait; }
            set
            {
                _selectedAvailableTrait = value;
                OnPropertyChanged();
            }
        }

        public RacialTraitCommand MoveFromAvailableToSelectedCommand
        {
            get { return _moveFromAvailableToSelectedCommand; }
        }

        public string TotalRP
        {
            get { return SelectedTraits.Sum(t => t.Trait.RP).ToString(CultureInfo.InvariantCulture); }
        }

        public GenericRacialTraitViewModel SelectedSelectedTrait
        {
            get { return _selectedSelectedTrait; }
            set
            {
                _selectedSelectedTrait = value;
                OnPropertyChanged();
            }
        }

        public RemoveRacialTraitCommand DeleteSelectedTraitCommand
        {
            get { return _removeSelectedTraitCommand; }
        }
    }

    public class GenericRacialTraitViewModel : BaseViewModel
    {
        private readonly RaceViewModel _owner;
        public IRacialTrait Trait { get; private set; }

        public GenericRacialTraitViewModel(IRacialTrait trait, RaceViewModel owner)
        {
            _owner = owner;
            Trait = trait;
        }

        public string Name { get { return Trait.Name; } }

        public string Description { get { return Trait.Description; } }

        public string ReplacedTraits
        {
            get
            {
                var selectedTraitsToReplace = _owner.SelectedTraits.Where(vm => ((ulong)vm.Trait.TraitCategory & (ulong)Trait.TraitCategory) > 0 && vm != this);

                return string.Join("; ", selectedTraitsToReplace.Select(o => o.Name));
            }
        }

        public void ChangedSelectedTraits()
        {
            OnPropertyChanged("ReplacedTraits");
        }
    }
}
