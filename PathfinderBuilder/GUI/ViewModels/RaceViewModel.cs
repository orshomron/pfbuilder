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
        private int _numSelectedOptionalAbilityModifiers;
        private bool _optionalStrengthChecked,
            _optionalDexterityChecked,
            _optionalConstitutionChecked,
            _optionalIntelligenceChecked,
            _optionalWisdomChecked,
            _optionalCharismaChecked;

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

        public bool RaceHasOptionalAbilityModifier => Model.Race.RaceHasOptionalAbilityModifier;

        public int NumOptionalAbilityModifiers => (Model.Race as IRaceWithOptionalAbilityModifier)?.NumberOfAttributes ?? 0;

        public int NumOptionalAbilityModifiersLeftToChoose => NumOptionalAbilityModifiers - NumSelectedOptionalAbilityModifiers;

        public bool OptionalStrengthChecked
        {
            get { return _optionalStrengthChecked; }
            set
            {
                if (value == _optionalStrengthChecked)
                {
                    return;
                }
                _optionalStrengthChecked = value;
                var race = Model.Race as IRaceWithOptionalAbilityModifier;
                if (value) // was false before
                {
                    NumSelectedOptionalAbilityModifiers++;
                    race.AttributesWithModifier.Add(Attributes.Strength);
                }
                else
                {
                    NumSelectedOptionalAbilityModifiers = Math.Max(0, NumSelectedOptionalAbilityModifiers - 1);
                    race.AttributesWithModifier.Remove(Attributes.Strength);
                }
                OnPropertyChanged(nameof(StrengthModifier));
                OnPropertyChanged(nameof(AbilityModifiersString));
                OnPropertyChanged();
            }
        }

        public bool OptionalDexterityChecked
        {
            get { return _optionalDexterityChecked; }
            set
            {
                if (value == _optionalDexterityChecked)
                {
                    return;
                }
                _optionalDexterityChecked = value;
                var race = Model.Race as IRaceWithOptionalAbilityModifier;
                if (value) // was false before
                {
                    NumSelectedOptionalAbilityModifiers++;
                    race?.AttributesWithModifier.Add(Attributes.Dexterity);
                }
                else
                {
                    NumSelectedOptionalAbilityModifiers = Math.Max(0, NumSelectedOptionalAbilityModifiers - 1);
                    race?.AttributesWithModifier.Remove(Attributes.Dexterity);
                }
                OnPropertyChanged(nameof(DexterityModifier));
                OnPropertyChanged(nameof(AbilityModifiersString));
                OnPropertyChanged();
            }
        }

        public bool OptionalConstitutionChecked
        {
            get { return _optionalConstitutionChecked; }
            set
            {
                if (value == _optionalConstitutionChecked)
                {
                    return;
                }
                _optionalConstitutionChecked = value;
                var race = Model.Race as IRaceWithOptionalAbilityModifier;
                if (value) // was false before
                {
                    NumSelectedOptionalAbilityModifiers++;
                    race?.AttributesWithModifier.Add(Attributes.Constitution);
                }
                else
                {
                    NumSelectedOptionalAbilityModifiers = Math.Max(0, NumSelectedOptionalAbilityModifiers - 1);
                    race?.AttributesWithModifier.Remove(Attributes.Constitution);
                }
                OnPropertyChanged(nameof(ConstitutionModifier));
                OnPropertyChanged(nameof(AbilityModifiersString));
                OnPropertyChanged();
            }
        }

        public bool OptionalIntelligenceChecked
        {
            get { return _optionalIntelligenceChecked; }
            set
            {
                if (value == _optionalIntelligenceChecked)
                {
                    return;
                }
                _optionalIntelligenceChecked = value;
                var race = Model.Race as IRaceWithOptionalAbilityModifier;
                if (value) // was false before
                {
                    NumSelectedOptionalAbilityModifiers++;
                    race?.AttributesWithModifier.Add(Attributes.Intelligence);
                }
                else
                {
                    NumSelectedOptionalAbilityModifiers = Math.Max(0, NumSelectedOptionalAbilityModifiers - 1);
                    race?.AttributesWithModifier.Remove(Attributes.Intelligence);
                }
                OnPropertyChanged(nameof(IntelligenceModifier));
                OnPropertyChanged(nameof(AbilityModifiersString));
                OnPropertyChanged();
            }
        }

        public bool OptionalWisdomChecked
        {
            get { return _optionalWisdomChecked; }
            set
            {
                if (value == _optionalWisdomChecked)
                {
                    return;
                }
                _optionalWisdomChecked = value;
                var race = Model.Race as IRaceWithOptionalAbilityModifier;
                if (value) // was false before
                {
                    NumSelectedOptionalAbilityModifiers++;
                    race?.AttributesWithModifier.Add(Attributes.Wisdom);
                }
                else
                {
                    NumSelectedOptionalAbilityModifiers = Math.Max(0, NumSelectedOptionalAbilityModifiers - 1);
                    race?.AttributesWithModifier.Remove(Attributes.Wisdom);
                }
                OnPropertyChanged(nameof(WisdomModifier));
                OnPropertyChanged(nameof(AbilityModifiersString));
                OnPropertyChanged();
            }
        }

        public bool OptionalCharismaChecked
        {
            get { return _optionalCharismaChecked; }
            set
            {
                if (value == _optionalCharismaChecked)
                {
                    return;
                }
                _optionalCharismaChecked = value;
                var race = Model.Race as IRaceWithOptionalAbilityModifier;
                if (value) // was false before
                {
                    NumSelectedOptionalAbilityModifiers++;
                    race?.AttributesWithModifier.Add(Attributes.Charisma);
                }
                else
                {
                    NumSelectedOptionalAbilityModifiers = Math.Max(0, NumSelectedOptionalAbilityModifiers - 1);
                    race?.AttributesWithModifier.Remove(Attributes.Charisma);
                }
                OnPropertyChanged(nameof(CharismaModifier));
                OnPropertyChanged(nameof(AbilityModifiersString));
                OnPropertyChanged();
            }
        }

        public int NumSelectedOptionalAbilityModifiers
        {
            get { return _numSelectedOptionalAbilityModifiers; }
            set
            {
                _numSelectedOptionalAbilityModifiers = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(NumOptionalAbilityModifiersLeftToChoose));
            }
        }

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

                OptionalStrengthChecked = false;
                OptionalDexterityChecked = false;
                OptionalConstitutionChecked = false;
                OptionalIntelligenceChecked = false;
                OptionalWisdomChecked = false;
                OptionalCharismaChecked = false;

                OnPropertyChanged();
                OnPropertyChanged(nameof(RaceHasOptionalAbilityModifier));
                OnPropertyChanged(nameof(StrengthModifier));
                OnPropertyChanged(nameof(DexterityModifier));
                OnPropertyChanged(nameof(ConstitutionModifier));
                OnPropertyChanged(nameof(IntelligenceModifier));
                OnPropertyChanged(nameof(WisdomModifier));
                OnPropertyChanged(nameof(CharismaModifier));
                OnPropertyChanged(nameof(Size));
                OnPropertyChanged(nameof(AbilityModifiersString));
                OnPropertyChanged(nameof(SelectedTraits));
                OnPropertyChanged(nameof(AvailableTraits));
                OnPropertyChanged(nameof(NumOptionalAbilityModifiers));
                OnPropertyChanged(nameof(NumOptionalAbilityModifiersLeftToChoose));
            }
        }

        private void TraitsChanged(object sender, NotifyCollectionChangedEventArgs args)
        {
            Model.Race.SelectedTraits = new List<IRacialTrait>(_selectedTraits.Select(t => t.Trait));
            Model.Race.AvailableTraits = new List<IRacialTrait>(_availableTraits.Select(t => t.Trait));
            OnPropertyChanged(nameof(TotalRP));
            OnPropertyChanged(nameof(NumOptionalAbilityModifiers));
            OnPropertyChanged(nameof(NumOptionalAbilityModifiersLeftToChoose));

            if (NumOptionalAbilityModifiersLeftToChoose < 0)
            {
                OptionalStrengthChecked = false;
                OptionalDexterityChecked = false;
                OptionalConstitutionChecked = false;
                OptionalIntelligenceChecked = false;
                OptionalWisdomChecked = false;
                OptionalCharismaChecked = false;
            }
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
            OnPropertyChanged(nameof(NumOptionalAbilityModifiers));
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
