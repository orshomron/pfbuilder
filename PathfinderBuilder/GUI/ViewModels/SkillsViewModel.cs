using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using GUI.Converters;
using PathfinderBuilder;
using SkillEnum = PathfinderBuilder.Skills;

namespace GUI.ViewModels
{
    public class SkillViewModel : BaseViewModel
    {
        private static readonly AbilityModifierConverterToInt ModifierConverter = new AbilityModifierConverterToInt();

        private int _ranks;
        private bool _proficient;

        public AbilitiesViewModel Abilities { get; private set; }

        private readonly RaceViewModel _race;
        private readonly SkillsViewModel _owner;
        private readonly SkillEnum _skill;

        public SkillEnum Skill { get { return _skill; } }

        public int Ranks
        {
            get { return _ranks; }
            set
            {
                _ranks = value;
                OnPropertyChanged();
                OnPropertyChanged("FinalScore");
                _owner.SkillRankChanged();
            }
        }

        public bool Proficient { get { return _proficient; } set { _proficient = value; OnPropertyChanged(); OnPropertyChanged("FinalScore"); } }

        public Attributes Attribute { get; set; }

        public int FinalScore
        {
            get
            {
                var raceModifier = _race.Race.SelectedTraits.OfType<IAddToSkills>()
                        .Where(t => t.SkillAndModifier.ContainsKey(Skill))
                        .Sum(t => t.SkillAndModifier[Skill]);

                return Ranks + (Proficient && Ranks > 0 ? 3 : 0) + (int)ModifierConverter.Convert(Abilities.GetAttribute(Attribute), typeof(int), null, null) + raceModifier;
            }
        }

        public SkillViewModel(SkillsViewModel owner, SkillEnum skill, bool proficient)
        {
            Abilities = owner.Abilities;
            Abilities.PropertyChanged += Abilities_PropertyChanged;
            _race = owner.Race;
            _owner = owner;
            _skill = skill;
            Proficient = proficient;
            Ranks = 0;
            Attribute = Skill.GetAttribute();

            owner.PropertyChanged += OwnerOnPropertyChanged;
        }

        private void OwnerOnPropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            if (args.PropertyName == "TotalLevel" || args.PropertyName == "CurrentAvailableRanks")
            {
                OnPropertyChanged("MaximumLevel");
            }
        }

        public int MaximumLevel
        {
            get
            {
                return Math.Min(Ranks + _owner.CurrentAvailableRanks, _owner.TotalLevel);
            }
        }

        void Abilities_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            OnPropertyChanged("FinalScore");
        }
    }

    public class SkillWithSubcategoryViewModel : SkillViewModel
    {
        public string Subcategory { get; set; }

        public SkillWithSubcategoryViewModel(SkillsViewModel owner, SkillEnum skill, bool proficient, string subcategory = "")
            : base(owner, skill, proficient)
        {
            Subcategory = subcategory;
        }
    }

    public class SkillsViewModel : BaseViewModel
    {
        private static readonly AbilityModifierConverterToInt ModifierConverter = new AbilityModifierConverterToInt();
        private readonly ObservableCollection<SkillViewModel> _skills = new ObservableCollection<SkillViewModel>();
        private readonly CharacterViewModel _owner;

        public AbilitiesViewModel Abilities { get { return _owner.AbilitiesVM; } }
        public RaceViewModel Race { get { return _owner.RaceVM; } }

        public int TotalAvailableRanks { get { return _owner.ClassesVM.Levels.Sum(vm => vm.Level * Math.Max(1, vm.Instance.SkillPointsPerLevel + (int)ModifierConverter.Convert(_owner.AbilitiesVM.FinalIntelligence, typeof(int), null, null))); } }

        public int CurrentAvailableRanks
        {
            get
            {
                var totalSkillRanks = _skills.Sum(s => s.Ranks);
                return TotalAvailableRanks - totalSkillRanks;
            }
        }

        public int TotalLevel { get { return _owner.ClassesVM.TotalLevel; } }

        public ObservableCollection<SkillViewModel> Skills { get { return _skills; } }

        public SkillsViewModel(CharacterViewModel character)
        {
            _owner = character;
            _owner.ClassesVM.PropertyChanged += ClassesVMOnPropertyChanged;
            _owner.AbilitiesVM.PropertyChanged += AbilitiesVMOnPropertyChanged;

            var tmp = new List<SkillViewModel>();

            foreach (Skills skill in Enum.GetValues(typeof(SkillEnum)))
            {
                if (skill != SkillEnum.Craft && skill != SkillEnum.Perform && skill != SkillEnum.Profession)
                {
                    tmp.Add(new SkillViewModel(this, skill, false));
                }
            }

            tmp.Add(new SkillWithSubcategoryViewModel(this, SkillEnum.Craft, false));
            tmp.Add(new SkillWithSubcategoryViewModel(this, SkillEnum.Craft, false));
            tmp.Add(new SkillWithSubcategoryViewModel(this, SkillEnum.Craft, false));

            tmp.Add(new SkillWithSubcategoryViewModel(this, SkillEnum.Perform, false));
            tmp.Add(new SkillWithSubcategoryViewModel(this, SkillEnum.Perform, false));
            tmp.Add(new SkillWithSubcategoryViewModel(this, SkillEnum.Perform, false));

            tmp.Add(new SkillWithSubcategoryViewModel(this, SkillEnum.Profession, false));
            tmp.Add(new SkillWithSubcategoryViewModel(this, SkillEnum.Profession, false));
            tmp.Add(new SkillWithSubcategoryViewModel(this, SkillEnum.Profession, false));

            foreach (var skill in tmp.OrderBy(s => s.Skill))
            {
                skill.PropertyChanged += SkillOnPropertyChanged;
                _skills.Add(skill);
            }
        }

        private void AbilitiesVMOnPropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            OnPropertyChanged("TotalLevel");
            OnPropertyChanged("TotalAvailableRanks");
            OnPropertyChanged("CurrentAvailableRanks");
        }

        private void ClassesVMOnPropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            var allClassSkills = _owner.Character.Levels.SelectMany(c => c.Key.ClassSkills).Distinct().ToList();

            foreach (var skill in _skills)
            {
                skill.Proficient = allClassSkills.Contains(skill.Skill);
            }
            OnPropertyChanged("TotalLevel");
            OnPropertyChanged("TotalAvailableRanks");
            OnPropertyChanged("CurrentAvailableRanks");
        }

        private void SkillOnPropertyChanged(object sender, PropertyChangedEventArgs eventArgs)
        {
            foreach (var skill in _skills)
            {
                _owner.Character.SkillRanks[skill.Skill] = skill.Ranks;
                _owner.Character.FinalSkillModifiers[skill.Skill] = skill.FinalScore;
            }
        }

        public void SkillRankChanged()
        {
            OnPropertyChanged("CurrentAvailableRanks");
        }
    }
}
