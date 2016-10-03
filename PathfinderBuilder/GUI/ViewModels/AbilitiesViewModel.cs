using System;
using System.ComponentModel;
using PathfinderBuilder;

namespace GUI.ViewModels
{
    

    public class AbilitiesViewModel : BaseViewModel
    {
        private byte _str = 10, _dex = 10, _con = 10, _int = 10, _wis = 10, _cha = 10;
        private bool _allowMoreThan18;
        private PointBuyOptions _pointBuyOption = PointBuyOptions.O20;
        private readonly CharacterViewModel _owner;

        public AbilitiesViewModel(CharacterViewModel character)
        {
            _owner = character;
            _owner.RaceVM.PropertyChanged += RaceOnPropertyChanged;
        }

        protected override void OnPropertyChanged(string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);
            _owner.Character.AbilityScores[Attributes.Strength] = FinalStrength;
            _owner.Character.AbilityScores[Attributes.Dexterity] = FinalDexterity;
            _owner.Character.AbilityScores[Attributes.Constitution] = FinalConstitution;
            _owner.Character.AbilityScores[Attributes.Intelligence] = FinalIntelligence;
            _owner.Character.AbilityScores[Attributes.Wisdom] = FinalWisdom;
            _owner.Character.AbilityScores[Attributes.Charisma] = FinalCharisma;

            _owner.Character.AbilityScoresRaw[Attributes.Strength] = Strength;
            _owner.Character.AbilityScoresRaw[Attributes.Dexterity] = Dexterity;
            _owner.Character.AbilityScoresRaw[Attributes.Constitution] = Constitution;
            _owner.Character.AbilityScoresRaw[Attributes.Intelligence] = Intelligence;
            _owner.Character.AbilityScoresRaw[Attributes.Wisdom] = Wisdom;
            _owner.Character.AbilityScoresRaw[Attributes.Charisma] = Charisma;
        }

        private void RaceOnPropertyChanged(object sender, PropertyChangedEventArgs propertyChangedEventArgs)
        {
            OnPropertyChanged("RaceStrengthModifier");
            OnPropertyChanged("RaceDexterityModifier");
            OnPropertyChanged("RaceConstitutionModifier");
            OnPropertyChanged("RaceIntelligenceModifier");
            OnPropertyChanged("RaceWisdomModifier");
            OnPropertyChanged("RaceCharismaModifier");
        }

        public int RaceStrengthModifier { get { return _owner.RaceVM.StrengthModifier; } }
        public int RaceDexterityModifier { get { return _owner.RaceVM.DexterityModifier; } }
        public int RaceConstitutionModifier { get { return _owner.RaceVM.ConstitutionModifier; } }
        public int RaceIntelligenceModifier { get { return _owner.RaceVM.IntelligenceModifier; } }
        public int RaceWisdomModifier { get { return _owner.RaceVM.WisdomModifier; } }
        public int RaceCharismaModifier { get { return _owner.RaceVM.CharismaModifier; } }

        public int FinalStrength { get { return _str + _owner.RaceVM.StrengthModifier; } }
        public int FinalDexterity { get { return _dex + _owner.RaceVM.DexterityModifier; } }
        public int FinalConstitution { get { return _con + _owner.RaceVM.ConstitutionModifier; } }
        public int FinalIntelligence { get { return _int + _owner.RaceVM.IntelligenceModifier; } }
        public int FinalWisdom { get { return _wis + _owner.RaceVM.WisdomModifier; } }
        public int FinalCharisma { get { return _cha + _owner.RaceVM.CharismaModifier; } }

        public byte Intelligence
        {
            get
            {
                return _int;
            }
            set
            {
                _int = value;
                OnPropertyChanged();
            }
        }

        public byte Wisdom
        {
            get
            {
                return _wis;
            }
            set
            {
                _wis = value;
                OnPropertyChanged();
                OnPropertyChanged("WisdomCost");
                OnPropertyChanged("CurrentTotalPointCost");
            }
        }

        public byte Charisma
        {
            get
            {
                return _cha;
            }
            set
            {
                _cha = value;
                OnPropertyChanged();
            }
        }

        public byte Constitution
        {
            get
            {
                return _con;
            }
            set
            {
                _con = value;
                OnPropertyChanged();
                OnPropertyChanged("ConstitutionCost");
                OnPropertyChanged("CurrentTotalPointCost");
            }
        }

        public byte Dexterity
        {
            get
            {
                return _dex;
            }
            set
            {
                _dex = value;
                OnPropertyChanged();
                OnPropertyChanged("DexterityCost");
                OnPropertyChanged("CurrentTotalPointCost");
            }
        }

        public byte Strength
        {
            get
            {
                return _str;
            }
            set
            {
                _str = value;
                OnPropertyChanged();
            }
        }

        public bool AllowMoreThan18
        {
            get
            {
                return _allowMoreThan18;
            }
            set
            {
                _allowMoreThan18 = value;
                OnPropertyChanged();
            }
        }

        public PointBuyOptions PointBuyOption
        {
            get { return _pointBuyOption; }
            set
            {
                _pointBuyOption = value;
                OnPropertyChanged();
            }
        }

        public int GetAttribute(Attributes attribute)
        {
            switch (attribute)
            {
                case Attributes.Strength:
                    return FinalStrength;
                case Attributes.Dexterity:
                    return FinalDexterity;
                case Attributes.Constitution:
                    return FinalConstitution;
                case Attributes.Wisdom:
                    return FinalWisdom;
                case Attributes.Intelligence:
                    return FinalIntelligence;
                case Attributes.Charisma:
                    return FinalCharisma;
            }
            throw new ArgumentException("Unknown attribute recieved", "attribute");
        }
    }
}
