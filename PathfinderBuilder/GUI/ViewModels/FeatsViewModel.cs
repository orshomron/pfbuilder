using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using GUI.Commands;
using PathfinderBuilder;

namespace GUI.ViewModels
{
    public class FeatsViewModel : BaseViewModel
    {
        private FeatViewModel _selectedFeat;

        public CharacterViewModel Owner { get; }

        public ObservableCollection<FeatViewModel> AvailableFeats { get; }

        public FeatViewModel SelectedFeat { get { return _selectedFeat; } set { _selectedFeat = value; OnPropertyChanged(); } }

        public AddFeatCommand AddFeatCommand { get; }

        public FeatsViewModel(CharacterViewModel owner)
        {
            Owner = owner;
            Owner.AbilitiesVM.PropertyChanged += FeatsChagned;
            Owner.SkillsVM.PropertyChanged += FeatsChagned;
            Owner.RaceVM.PropertyChanged += FeatsChagned;
            Owner.ClassesVM.PropertyChanged += FeatsChagned;

            var vms = Assembly.GetAssembly(typeof(IFeat)).GetTypes()
                .Where(t => typeof(IFeat).IsAssignableFrom(t) && !t.IsInterface && !t.IsAbstract)
                .Select(Activator.CreateInstance)
                .Cast<IFeat>()
                .Select(f =>
                {
                    if (f is IFeatWithSkillSelection)
                    {
                        return new FeatWithSkillSelectionViewModel((IFeatWithSkillSelection)f, this);
                    }
                    return new FeatViewModel(f, this);
                });

            AvailableFeats = new ObservableCollection<FeatViewModel>(vms);

            AddFeatCommand = new AddFeatCommand(this);
        }

        public void FeatsChagned(object sender = null, EventArgs args = null)
        {
            OnPropertyChanged("Feats");
        }
    }

    public class FeatViewModel : BaseViewModel
    {
        public FeatsViewModel Owner { get; }

        public IFeat Feat { get; }

        public FeatViewModel(IFeat feat, FeatsViewModel owner)
        {
            Feat = feat;
            Owner = owner;
            Owner.PropertyChanged += OnOwnerOnPropertyChanged;
        }

        private void OnOwnerOnPropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            OnPropertyChanged(nameof(CanUse));
            OnPropertyChanged(nameof(CharacterHas));
            OnPropertyChanged(nameof(PrerequisiteNeeded));
        }

        public string Name => Feat.Name;

        public bool CanUse => Feat.Prerequisite.CanUse(Owner.Owner.Character);

        public bool CharacterHas => Owner.Owner.Character.AllFeats.Any(f => f.Equals(Feat));

        public string PrerequisiteNeeded => Feat.Prerequisite.NotMetText;

        public override string ToString()
        {
            return $"VM: {Feat.Name}";
        }
    }

    public class FeatWithSkillSelectionViewModel : FeatViewModel
    {
        private static readonly ObservableCollection<Skills> SkillsListInternal = new ObservableCollection<Skills>(Enum.GetValues(typeof(Skills)).Cast<Skills>());

        public new IFeatWithSkillSelection Feat { get; }

        public FeatWithSkillSelectionViewModel(IFeatWithSkillSelection feat, FeatsViewModel owner) : base(feat, owner)
        {
            Feat = feat;
        }

        public Skills Skill { get { return Feat.Skill; } set { Feat.SetSkill(value); OnPropertyChanged(); } }

        public ObservableCollection<Skills> SkillList => SkillsListInternal;

        public override string ToString()
        {
            return $"VM (Skill Select): {Feat.Name}";
        }
    }
}
