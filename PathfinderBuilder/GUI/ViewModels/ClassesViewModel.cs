using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Resources;
using GUI.Commands;
using PathfinderBuilder;
using PathfinderBuilder.Classes;
using PathfinderBuilder.Classes.Prestige;

namespace GUI.ViewModels
{
    public class ClassesViewModel : BaseViewModel
    {
        private readonly CharacterViewModel _owner;
        private readonly ObservableCollection<ClassViewModel> _availableClasses;
        private readonly ObservableCollection<ClassWithLevelViewModel> _levels;
        private readonly CreateClassLevelCommand _createClassLevelCommand;
        private readonly RemoveClassCommand _removeClassCommand;
        private readonly AddRemoveArchtypeCommand _addRemoveArchtypeCommand;

        private ClassViewModel _selectedAvailableClass;
        private ClassWithLevelViewModel _selectedLevel;
        private ArchtypeViewModel _selectedArchtype;
        private ObservableCollection<ArchtypeViewModel> _currentArchtypes;
        private bool _showPrestigeUnavailable;

        public ClassesViewModel(CharacterViewModel characterViewModel)
        {
            _owner = characterViewModel;
            _availableClasses = new ObservableCollection<ClassViewModel>
            {
                new ClassViewModel(this, new Rogue()),
                new ClassViewModel(this, new Wizard()),
                new PrestigeClassViewModel(this, new ArcaneSavant())
            };
            _levels = new ObservableCollection<ClassWithLevelViewModel>();
            _createClassLevelCommand = new CreateClassLevelCommand(this);
            _removeClassCommand = new RemoveClassCommand(this);
            _addRemoveArchtypeCommand = new AddRemoveArchtypeCommand(this);

            _owner.RaceVM.PropertyChanged += (sender, args) => UpdatedLevels();
        }

        public CharacterViewModel Owner { get { return _owner; } }

        public ObservableCollection<ClassViewModel> AvailableClasses
        {
            get { return _availableClasses; }
        }

        public ObservableCollection<ClassWithLevelViewModel> Levels
        {
            get { return _levels; }
        }

        public ClassViewModel SelectedAvailableClass
        {
            get
            {
                return _selectedAvailableClass;
            }
            set
            {
                _selectedAvailableClass = value;
                OnPropertyChanged();
            }
        }

        public CreateClassLevelCommand CreateClassLevelCommand { get { return _createClassLevelCommand; } }

        public void UpdatedLevels()
        {
            foreach (var level in Levels)
            {
                level.LevelsUpdated();
            }

            _owner.Character.Levels = _levels.ToDictionary(vm => vm.Instance, vm => vm.Level);

            OnPropertyChanged("TotalLevel");

            if (CurrentArchtypes != null)
            {
                foreach (var archtype in CurrentArchtypes)
                {
                    archtype.UpdateCanBeAdded();
                }
            }
        }

        public int TotalLevel { get { return Levels.Sum(l => l.Level); } }

        public ClassWithLevelViewModel SelectedLevel
        {
            get { return _selectedLevel; }
            set
            {
                _selectedLevel = value;
                OnPropertyChanged();
                if (value != null)
                {
                    var vms =
                        SelectedLevel.Instance.MyArchtypes.Select(
                            archtype => new ArchtypeViewModel(SelectedLevel.Instance.ArchtypeEnumType, archtype, false, SelectedLevel, _owner.Character));
                    CurrentArchtypes = new ObservableCollection<ArchtypeViewModel>(vms);
                }
                else
                {
                    CurrentArchtypes = new ObservableCollection<ArchtypeViewModel>();
                }
                SelectedArchtype = null;
            }
        }

        public RemoveClassCommand RemoveClassCommand
        {
            get { return _removeClassCommand; }
        }

        public ArchtypeViewModel SelectedArchtype
        {
            get { return _selectedArchtype; }
            set
            {
                _selectedArchtype = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<ArchtypeViewModel> CurrentArchtypes
        {
            get { return _currentArchtypes; }
            set
            {
                _currentArchtypes = value;
                OnPropertyChanged();
            }
        }

        public AddRemoveArchtypeCommand AddRemoveArchtypeCommand
        {
            get { return _addRemoveArchtypeCommand; }
        }

        public bool ShowPrestigeUnavailable
        {
            get { return _showPrestigeUnavailable; }
            set
            {
                _showPrestigeUnavailable = value;
                OnPropertyChanged();
            }
        }

        public void PrerequestiesMayChanged()
        {
            OnPropertyChanged("ShowPrestigeUnavailable");
        }
    }

    public class ClassWithLevelViewModel : ClassViewModel
    {
        private int _level = 1;

        public ClassWithLevelViewModel(ClassesViewModel owner, ClassBase classInstance)
            : base(owner, classInstance)
        {
        }

        public int Level
        {
            get { return _level; }
            set
            {
                _level = value;
                Owner.UpdatedLevels();
                OnPropertyChanged();
            }
        }

        public int MyMaximalLevel
        {
            get { return 20 - Owner.TotalLevel + Level; }
        }

        public void LevelsUpdated()
        {
            OnPropertyChanged("MyMaximalLevel");
        }
    }

    public class ArchtypeViewModel : BaseViewModel
    {
        private string _name;
        private bool _isUsed;
        private readonly ClassViewModel _owner;
        private readonly Character _character;

        private readonly Type _enumType;
        private readonly object _value;

        public ArchtypeViewModel(Type enumType, object value, bool isUsed, ClassViewModel owner, Character character)
        {
            _enumType = enumType;
            _value = value;
            _isUsed = isUsed;
            _owner = owner;
            _character = character;
            Name = EnumHelper.GetDescription(enumType, value);
        }

        public object Value { get { return _value; } }

        public Type EnumType { get { return _enumType; } }

        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                OnPropertyChanged();
            }
        }

        public bool IsRacial
        {
            get { return ((RacialArchtype)_value).HasFlag(RacialArchtype.Racial); }
        }

        public bool IsUsed
        {
            get { return _isUsed; }
            set
            {
                _isUsed = value;
                OnPropertyChanged();
            }
        }

        public bool CanBeAdded
        {
            get { return _owner.Instance.CanAddArchtype(_value, _character); }
        }

        public void UpdateCanBeAdded()
        {
            OnPropertyChanged("CanBeAdded");
        }
    }

    public class ClassViewModel : BaseViewModel
    {
        protected ClassBase ClassInstance;
        protected ClassesViewModel Owner;

        public ClassViewModel(ClassesViewModel owner, ClassBase classInstance)
        {
            Owner = owner;
            ClassInstance = classInstance;
        }

        public string Name { get { return ClassInstance.ClassName; } }

        public ClassBase Instance { get { return ClassInstance; } }
    }

    public class PrestigeClassViewModel : ClassViewModel
    {
        protected new PrestigeClass ClassInstance;

        public PrestigeClassViewModel(ClassesViewModel owner, PrestigeClass classInstance)
            : base(owner, classInstance)
        {
            ClassInstance = classInstance;
            Register();
        }

        private void Register()
        {
            Owner.PropertyChanged += PropChanged;
        }

        private void PropChanged(object sender, PropertyChangedEventArgs propertyChangedEventArgs)
        {
            OnPropertyChanged("CanUse");
            OnPropertyChanged("UnmetPrerequisites");
        }

        public new PrestigeClass Instance { get { return ClassInstance; } }

        public bool CanUse { get { return Instance.CanAddClass(Owner.Owner.Character); } }

        public string UnmetPrerequisites
        {
            get
            {
                return string.Join("; ", Instance.Prerequisites.Where(p => !p.CanUse(Owner.Owner.Character)).Select(p => p.NotMetText));
            }
        }
    }
}
