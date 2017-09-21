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

        private readonly SkillsViewModel _owner;

        public SkillEnum Skill { get; }

        public int Ranks
        {
            get { return _ranks; }
            set
            {
                _ranks = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(FinalScore));
                _owner.SkillRankChanged();
            }
        }

        public bool Proficient { get { return _proficient; } set { _proficient = value; OnPropertyChanged(); OnPropertyChanged(nameof(FinalScore)); } }

        public Attributes Attribute { get; set; }

        public int FinalScore
        {
            get
            {
                var raceModifier = _owner.Model.Race.SelectedTraits.OfType<IAddToSkills>()
                        .Where(t => t.SkillAndModifier.ContainsKey(Skill))
                        .Sum(t => t.SkillAndModifier[Skill]);

                return Ranks + (Proficient && Ranks > 0 ? 3 : 0) + (int)ModifierConverter.Convert(_owner.Model.GetCalculatedAttribute(Attribute), typeof(int), null, null) + raceModifier;
            }
        }

        public SkillViewModel(SkillsViewModel owner, SkillEnum skill, bool proficient)
        {
            _owner = owner;
            Skill = skill;
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
        
        public override void ReloadModelValues()
        {
            OnPropertyChanged(nameof(FinalScore));
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

    public class SkillsViewModel : BaseViewModel<Character>
    {
        private static readonly AbilityModifierConverterToInt ModifierConverter = new AbilityModifierConverterToInt();
        private readonly ObservableCollection<SkillViewModel> _skills = new ObservableCollection<SkillViewModel>();


        public int TotalAvailableRanks { get { return Model.Levels.Sum(vm => vm.Value * Math.Max(1, vm.Key.SkillPointsPerLevel + (int)ModifierConverter.Convert(Model.GetCalculatedAttribute(Attributes.Intelligence), typeof(int), null, null))); } }

        public int CurrentAvailableRanks
        {
            get
            {
                var totalSkillRanks = _skills.Sum(s => s.Ranks);
                return TotalAvailableRanks - totalSkillRanks;
            }
        }

        public int TotalLevel { get { return Model.Levels.Sum(kvp => kvp.Value); } }

        public ObservableCollection<SkillViewModel> Skills => _skills;

        public SkillsViewModel(Character character) : base(character)
        {
            var tmp = new List<SkillViewModel>();

            foreach (Skills skill in Enum.GetValues(typeof(SkillEnum)))
            {
                if (skill != SkillEnum.CraftMisc && skill != SkillEnum.Perform && skill != SkillEnum.Profession && skill != SkillEnum.INVALID)
                {
                    tmp.Add(new SkillViewModel(this, skill, false));
                }
            }

            tmp.Add(new SkillWithSubcategoryViewModel(this, SkillEnum.CraftMisc, false));
            tmp.Add(new SkillWithSubcategoryViewModel(this, SkillEnum.CraftMisc, false));
            tmp.Add(new SkillWithSubcategoryViewModel(this, SkillEnum.CraftMisc, false));

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

        private void SkillOnPropertyChanged(object sender, PropertyChangedEventArgs eventArgs)
        {
            foreach (var skill in _skills)
            {
                Model.SkillRanks[skill.Skill] = skill.Ranks;
                Model.FinalSkillModifiers[skill.Skill] = skill.FinalScore;
            }
        }

        public void SkillRankChanged()
        {
            OnPropertyChanged(nameof(CurrentAvailableRanks));
        }

        public override void ReloadModelValues()
        {
            var allClassSkills = Model.Levels.SelectMany(c => c.Key.ClassSkills).Distinct().ToList();

            foreach (var skill in _skills)
            {
                skill.Proficient = allClassSkills.Contains(skill.Skill);
            }

            OnPropertyChanged(nameof(TotalLevel));
            OnPropertyChanged(nameof(TotalAvailableRanks));
            OnPropertyChanged(nameof(CurrentAvailableRanks));
            foreach (var vm in _skills)
            {
                vm.ReloadModelValues();
            }
        }
    }
}
