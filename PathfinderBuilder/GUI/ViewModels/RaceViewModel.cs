using System;
using System.Collections.ObjectModel;
using System.Linq;
using PathfinderBuilder;

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

        public RaceViewModel(CharacterViewModel owner)
        {
            _owner = owner;
            Race = _myRaceInstances.First();
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
                _race = value;
                _owner.Character.Race = value;
                OnPropertyChanged();
                OnPropertyChanged("RaceHasOptionalAbilityModifier");
                OnPropertyChanged("StrengthModifier");
                OnPropertyChanged("DexterityModifier");
                OnPropertyChanged("ConstitutionModifier");
                OnPropertyChanged("IntelligenceModifier");
                OnPropertyChanged("WisdomModifier");
                OnPropertyChanged("CharismaModifier");
                OnPropertyChanged("SelectedOptionalAbilityModifier");
            }
        }

        public ObservableCollection<Race> RacesList { get { return _myRaceInstances; } }
    }
}
