using System;
using PathfinderBuilder;

namespace GUI.ViewModels
{


    public class AbilitiesViewModel : BaseViewModel<Character>
    {
        private bool _allowMoreThan18;
        private PointBuyOptions _pointBuyOption = PointBuyOptions.O20;

        public AbilitiesViewModel(Character character) : base(character)
        {
        }

        public int RaceStrengthModifier { get { return Model.Race.StrengthModifier; } }
        public int RaceDexterityModifier { get { return Model.Race.DexterityModifier; } }
        public int RaceConstitutionModifier { get { return Model.Race.ConstitutionModifier; } }
        public int RaceIntelligenceModifier { get { return Model.Race.IntelligenceModifier; } }
        public int RaceWisdomModifier { get { return Model.Race.WisdomModifier; } }
        public int RaceCharismaModifier { get { return Model.Race.CharismaModifier; } }

        public int FinalStrength { get { return Model.GetCalculatedAttribute(Attributes.Strength); } }
        public int FinalDexterity { get { return Model.GetCalculatedAttribute(Attributes.Dexterity); } }
        public int FinalConstitution { get { return Model.GetCalculatedAttribute(Attributes.Constitution); } }
        public int FinalIntelligence { get { return Model.GetCalculatedAttribute(Attributes.Intelligence); } }
        public int FinalWisdom { get { return Model.GetCalculatedAttribute(Attributes.Wisdom); } }
        public int FinalCharisma { get { return Model.GetCalculatedAttribute(Attributes.Charisma); } }

        public byte Intelligence
        {
            get { return Model.AbilityScoresRaw[Attributes.Intelligence]; }
            set
            {
                Model.AbilityScoresRaw[Attributes.Intelligence] = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(FinalIntelligence));
            }
        }

        public byte Wisdom
        {
            get
            {
                return Model.AbilityScoresRaw[Attributes.Wisdom];
            }
            set
            {
                Model.AbilityScoresRaw[Attributes.Wisdom] = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(FinalWisdom));
            }
        }

        public byte Charisma
        {
            get { return Model.AbilityScoresRaw[Attributes.Charisma]; }
            set
            {
                Model.AbilityScoresRaw[Attributes.Charisma] = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(FinalCharisma));
            }
        }

        public byte Constitution
        {
            get
            {
                return Model.AbilityScoresRaw[Attributes.Constitution];
            }
            set
            {
                Model.AbilityScoresRaw[Attributes.Constitution] = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(FinalConstitution));
            }
        }

        public byte Dexterity
        {
            get
            {
                return Model.AbilityScoresRaw[Attributes.Dexterity];
            }
            set
            {
                Model.AbilityScoresRaw[Attributes.Dexterity] = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(FinalDexterity));
            }
        }

        public byte Strength
        {
            get
            {
                return Model.AbilityScoresRaw[Attributes.Strength];
            }
            set
            {
                Model.AbilityScoresRaw[Attributes.Strength] = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(FinalStrength));
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
            throw new ArgumentException("Unknown attribute recieved", nameof(attribute));
        }

        public override void ReloadModelValues()
        {
            OnPropertyChanged(nameof(RaceStrengthModifier));
            OnPropertyChanged(nameof(RaceDexterityModifier));
            OnPropertyChanged(nameof(RaceConstitutionModifier));
            OnPropertyChanged(nameof(RaceIntelligenceModifier));
            OnPropertyChanged(nameof(RaceWisdomModifier));
            OnPropertyChanged(nameof(RaceCharismaModifier));
        }
    }
}
