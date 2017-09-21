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
    public class RaceViewModel : BaseViewModel<Character>
    {
        private ObservableCollection<GenericRacialTraitViewModel> _availableTraits;
        private ObservableCollection<GenericRacialTraitViewModel> _selectedTraits;
        private GenericRacialTraitViewModel _selectedAvailableTrait;
        private GenericRacialTraitViewModel _selectedSelectedTrait;

        public RaceViewModel(Character c) : base(c)
        {
            // make sure the race instance coming from character is maintained
            var replacedRace = RacesList.Single(r => r.GetType() == Model.Race.GetType());
            var index = RacesList.IndexOf(replacedRace);
            RacesList.RemoveAt(index);
            RacesList.Insert(index, Model.Race);
            Race = Model.Race;

            MoveFromAvailableToSelectedCommand = new RacialTraitCommand(this);
            DeleteSelectedTraitCommand = new RemoveRacialTraitCommand(this);
        }

        public int IntelligenceModifier => Model.Race.IntelligenceModifier;

        public int WisdomModifier => Model.Race.WisdomModifier;

        public int CharismaModifier => Model.Race.CharismaModifier;

        public int ConstitutionModifier => Model.Race.ConstitutionModifier;

        public int DexterityModifier => Model.Race.DexterityModifier;

        public int StrengthModifier => Model.Race.StrengthModifier;

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
                OnPropertyChanged(nameof(StrengthModifier));
                OnPropertyChanged(nameof(DexterityModifier));
                OnPropertyChanged(nameof(ConstitutionModifier));
                OnPropertyChanged(nameof(IntelligenceModifier));
                OnPropertyChanged(nameof(WisdomModifier));
                OnPropertyChanged(nameof(CharismaModifier));
                OnPropertyChanged(nameof(AbilityModifiersString));
            }
        }

        public bool RaceHasOptionalAbilityModifier => Model.Race.RaceHasOptionalAbilityModifier;

        public Race Race
        {
            get
            {
                return Model.Race;
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

                Model.Race = value;

                _selectedTraits = new ObservableCollection<GenericRacialTraitViewModel>(Model.Race.SelectedTraits.Select(t => new GenericRacialTraitViewModel(t, this)));
                _availableTraits = new ObservableCollection<GenericRacialTraitViewModel>(Model.Race.AvailableTraits.Select(t => new GenericRacialTraitViewModel(t, this)));

                _selectedTraits.CollectionChanged += TraitsChanged;
                _availableTraits.CollectionChanged += TraitsChanged;

                OnPropertyChanged();
                OnPropertyChanged(nameof(RaceHasOptionalAbilityModifier));
                OnPropertyChanged(nameof(StrengthModifier));
                OnPropertyChanged(nameof(DexterityModifier));
                OnPropertyChanged(nameof(ConstitutionModifier));
                OnPropertyChanged(nameof(IntelligenceModifier));
                OnPropertyChanged(nameof(WisdomModifier));
                OnPropertyChanged(nameof(CharismaModifier));
                OnPropertyChanged(nameof(SelectedOptionalAbilityModifier));
                OnPropertyChanged(nameof(Size));
                OnPropertyChanged(nameof(AbilityModifiersString));
                OnPropertyChanged(nameof(SelectedTraits));
                OnPropertyChanged(nameof(AvailableTraits));
            }
        }

        private void TraitsChanged(object sender, NotifyCollectionChangedEventArgs args)
        {
            Model.Race.SelectedTraits = new List<IRacialTrait>(_selectedTraits.Select(t => t.Trait));
            Model.Race.AvailableTraits = new List<IRacialTrait>(_availableTraits.Select(t => t.Trait));
            OnPropertyChanged(nameof(TotalRP));
        }

        public ObservableCollection<Race> RacesList { get; } = new ObservableCollection<Race>
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

        public Size Size => Model.Race.Size;

        public ObservableCollection<GenericRacialTraitViewModel> AvailableTraits => _availableTraits;

        public ObservableCollection<GenericRacialTraitViewModel> SelectedTraits => _selectedTraits;

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

        public RacialTraitCommand MoveFromAvailableToSelectedCommand { get; }

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

        public RemoveRacialTraitCommand DeleteSelectedTraitCommand { get; }

        public override void ReloadModelValues()
        {
        }
    }

    public class GenericRacialTraitViewModel : BaseViewModel
    {
        private readonly RaceViewModel _owner;
        public IRacialTrait Trait { get; }

        public GenericRacialTraitViewModel(IRacialTrait trait, RaceViewModel owner)
        {
            _owner = owner;
            Trait = trait;
        }

        public string Name => Trait.Name;

        public string Description => Trait.Description;

        public string ReplacedTraits
        {
            get
            {
                var selectedTraitsToReplace = _owner.SelectedTraits.Where(vm => ((ulong)vm.Trait.TraitCategory & (ulong)Trait.TraitCategory) > 0 && vm != this);

                return string.Join("; ", selectedTraitsToReplace.Select(o => o.Name));
            }
        }

        public int RP => Trait.RP;

        public bool HasRP => RP > 0;

        public void ChangedSelectedTraits()
        {
            OnPropertyChanged(nameof(ReplacedTraits));
        }

        public override void ReloadModelValues()
        {
        }
    }
}
