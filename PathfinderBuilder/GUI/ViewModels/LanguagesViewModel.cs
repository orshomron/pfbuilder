using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using GUI.Commands;
using GUI.Converters;
using PathfinderBuilder;

namespace GUI.ViewModels
{
    public class LanguageViewModel : BaseViewModel
    {
        private bool _isRacial;
        private readonly Language _language;

        public LanguageViewModel(bool isRacial, Language language)
        {
            _isRacial = isRacial;
            _language = language;
        }

        public bool IsRacial
        {
            get
            {
                return _isRacial;
            }
            set
            {
                _isRacial = value;
                OnPropertyChanged();
            }
        }

        public Language Language
        {
            get
            {
                return _language;
            }
        }

        public override void ReloadModelValues()
        {
            
        }
    }

    public class LanguagesViewModel : BaseViewModel<Character>
    {
        private static readonly AbilityModifierConverterToInt AbilityToModifierConverter = new AbilityModifierConverterToInt();

        private readonly ObservableCollection<LanguageViewModel> _knownLanguages;
        private readonly ObservableCollection<LanguageViewModel> _availableLanguages = new ObservableCollection<LanguageViewModel>();
        private bool _hasLinguistics;
        private LanguageViewModel _selectedAvailable, _selectedKnown;
        private readonly MoveLanguagesKnownToAvailableCommand _knownToAvailableCommand;
        private readonly MoveLanguagesAvailableToKnownCommand _availableToKnownCommand;

        public LanguagesViewModel(Character c) : base(c)
        {
            _knownLanguages = new ObservableCollection<LanguageViewModel>();
            _availableLanguages = new ObservableCollection<LanguageViewModel>();
            _knownLanguages.CollectionChanged += LangaugeCollectionsChanged;
            _availableLanguages.CollectionChanged += LangaugeCollectionsChanged;
            ResetLanguageLists();

            _knownToAvailableCommand = new MoveLanguagesKnownToAvailableCommand(this);
            _availableToKnownCommand = new MoveLanguagesAvailableToKnownCommand(this);
        }

        private void RaceVMOnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            ResetLanguageLists();
        }

        private void LangaugeCollectionsChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            OnPropertyChanged("LeftToSelect");
        }

        private void ResetLanguageLists()
        {
            _knownLanguages.Clear();
            _availableLanguages.Clear();
            
            foreach (var startingLanguage in Model.Race.StartingLanguages)
            {
                _knownLanguages.Add(new LanguageViewModel(true, startingLanguage));
            }

            foreach (var availableLanguage in Model.Race.AvailableLanguages)
            {
                _availableLanguages.Add(new LanguageViewModel(false, availableLanguage));
            }
        }

        private void CharacterVMOnPropertyChanged(object sender, PropertyChangedEventArgs propertyChangedEventArgs)
        {
            if (Model.SkillRanks.ContainsKey(Skills.Linguistics) &&
                Model.SkillRanks[Skills.Linguistics] > 0 &&
                !_hasLinguistics)
            {
                _hasLinguistics = true;

                var languagesRaw = _availableLanguages.Concat(_knownLanguages).Select(vm => vm.Language).ToList();
                foreach (var language in Enum.GetValues(typeof(Language)).Cast<Language>().Where(l => !languagesRaw.Contains(l)))
                {
                    _availableLanguages.Add(new LanguageViewModel(false, language));
                }
            }
            if (_hasLinguistics && (!Model.SkillRanks.ContainsKey(Skills.Linguistics) ||
                                    Model.SkillRanks[Skills.Linguistics] == 0))
            {
                _hasLinguistics = false;

                var removeList = new List<LanguageViewModel>();

                foreach (var availableLanguage in _availableLanguages)
                {
                    if (!Model.Race.AvailableLanguages.Contains(availableLanguage.Language))
                    {
                        removeList.Add(availableLanguage);
                    }
                }
                foreach (var vm in removeList)
                {
                    _availableLanguages.Remove(vm);
                }

                removeList.Clear();
                foreach (var selectedLanguage in _knownLanguages)
                {
                    if (!Model.Race.AvailableLanguages.Contains(selectedLanguage.Language) && !Model.Race.StartingLanguages.Contains(selectedLanguage.Language))
                    {
                        removeList.Add(selectedLanguage);
                    }
                }
                foreach (var vm in removeList)
                {
                    _knownLanguages.Remove(vm);
                }

            }

            OnPropertyChanged("AvailableLanguages");
            OnPropertyChanged("KnownLanguages");
            OnPropertyChanged("TotalToSelect");
            OnPropertyChanged("LeftToSelect");
        }

        public ObservableCollection<LanguageViewModel> AvailableLanguages { get { return _availableLanguages; } }

        public ObservableCollection<LanguageViewModel> KnownLanguages { get { return _knownLanguages; } }

        public int TotalToSelect
        {
            get
            {
                var fromInt = Math.Max(0, (int)AbilityToModifierConverter.Convert(Model.GetCalculatedAttribute(Attributes.Intelligence), typeof(int), null, null));

                int fromLinguistics;
                Model.SkillRanks.TryGetValue(Skills.Linguistics, out fromLinguistics);
                
                var isDoubleLinguistics = Model.Race.SelectedTraits.Any(t => t is IDoubleLinguistics);

                if (isDoubleLinguistics)
                {
                    fromLinguistics *= 2;
                }

                return fromInt + fromLinguistics;
            }
        }

        public int LeftToSelect
        {
            get { return TotalToSelect - _knownLanguages.Count + Model.Race.StartingLanguages.Count; }
        }

        public LanguageViewModel SelectedAvailable
        {
            get { return _selectedAvailable; }
            set
            {
                _selectedAvailable = value;
                OnPropertyChanged();
            }
        }

        public LanguageViewModel SelectedKnown
        {
            get { return _selectedKnown; }
            set
            {
                _selectedKnown = value;
                OnPropertyChanged();
            }
        }

        public ICommand MoveFromKnownToAvailableCommand
        {
            get { return _knownToAvailableCommand; }
        }

        public ICommand MoveFromAvailableToKnownCommand
        {
            get { return _availableToKnownCommand; }
        }

        public override void ReloadModelValues()
        {
        }
    }
}
