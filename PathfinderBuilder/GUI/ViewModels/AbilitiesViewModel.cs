using System;
using System.Linq;
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

        public int RaceStrengthModifier => Model.Race.StrengthModifier;
        public int RaceDexterityModifier => Model.Race.DexterityModifier;
        public int RaceConstitutionModifier => Model.Race.ConstitutionModifier;
        public int RaceIntelligenceModifier => Model.Race.IntelligenceModifier;
        public int RaceWisdomModifier => Model.Race.WisdomModifier;
        public int RaceCharismaModifier => Model.Race.CharismaModifier;

        // TODO: actual sum of items
        public int EquipmentStrengthModifier => 0;
        public int EquipmentDexterityModifier => 0;
        public int EquipmentConstitutionModifier => 0;
        public int EquipmentIntelligenceModifier => 0;
        public int EquipmentWisdomModifier => 0;
        public int EquipmentCharismaModifier => 0;

        public byte Intelligence
        {
            get { return Model.AbilityScoresRaw[Attributes.Intelligence]; }
            set
            {
                Model.AbilityScoresRaw[Attributes.Intelligence] = value;
                OnPropertyChanged();
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
            }
        }

        public byte Charisma
        {
            get { return Model.AbilityScoresRaw[Attributes.Charisma]; }
            set
            {
                Model.AbilityScoresRaw[Attributes.Charisma] = value;
                OnPropertyChanged();
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
                Strength = (byte)Math.Min((int)Strength, 18);
                Dexterity = (byte)Math.Min((int)Dexterity, 18);
                Constitution = (byte)Math.Min((int)Constitution, 18);
                Intelligence = (byte)Math.Min((int)Intelligence, 18);
                Wisdom = (byte)Math.Min((int)Wisdom, 18);
                Charisma = (byte)Math.Min((int)Charisma, 18);
                OnPropertyChanged();
            }
        }

        public int TotalLevel => Model.Levels.Sum(kvp => kvp.Value);

        public PointBuyOptions PointBuyOption
        {
            get { return _pointBuyOption; }
            set
            {
                _pointBuyOption = value;
                OnPropertyChanged();
            }
        }

        public byte LevelCharisma
        {
            get
            {
                return Model.AbilityScoresLevelBonuses[Attributes.Charisma];
            }
            set
            {
                Model.AbilityScoresLevelBonuses[Attributes.Charisma] = value;
                OnPropertyChanged();
            }
        }

        public byte LevelWisdom
        {
            get
            {
                return Model.AbilityScoresLevelBonuses[Attributes.Wisdom];
            }
            set
            {
                Model.AbilityScoresLevelBonuses[Attributes.Wisdom] = value;
                OnPropertyChanged();
            }
        }

        public byte LevelIntelligence
        {
            get
            {
                return Model.AbilityScoresLevelBonuses[Attributes.Intelligence];
            }
            set
            {
                Model.AbilityScoresLevelBonuses[Attributes.Intelligence] = value;
                OnPropertyChanged();
            }
        }

        public byte LevelStrength
        {
            get
            {
                return Model.AbilityScoresLevelBonuses[Attributes.Strength];
            }
            set
            {
                Model.AbilityScoresLevelBonuses[Attributes.Strength] = value;
                OnPropertyChanged();
            }
        }

        public byte LevelDexterity
        {
            get
            {
                return Model.AbilityScoresLevelBonuses[Attributes.Dexterity];
            }
            set
            {
                Model.AbilityScoresLevelBonuses[Attributes.Dexterity] = value;
                OnPropertyChanged();
            }
        }

        public byte LevelConstitution
        {
            get
            {
                return Model.AbilityScoresLevelBonuses[Attributes.Constitution];
            }
            set
            {
                Model.AbilityScoresLevelBonuses[Attributes.Constitution] = value;
                OnPropertyChanged();
            }
        }

        public override void ReloadModelValues()
        {
            OnPropertyChanged(nameof(RaceStrengthModifier));
            OnPropertyChanged(nameof(RaceDexterityModifier));
            OnPropertyChanged(nameof(RaceConstitutionModifier));
            OnPropertyChanged(nameof(RaceIntelligenceModifier));
            OnPropertyChanged(nameof(RaceWisdomModifier));
            OnPropertyChanged(nameof(RaceCharismaModifier));

            OnPropertyChanged(nameof(EquipmentStrengthModifier));
            OnPropertyChanged(nameof(EquipmentDexterityModifier));
            OnPropertyChanged(nameof(EquipmentConstitutionModifier));
            OnPropertyChanged(nameof(EquipmentIntelligenceModifier));
            OnPropertyChanged(nameof(EquipmentWisdomModifier));
            OnPropertyChanged(nameof(EquipmentCharismaModifier));

            OnPropertyChanged(nameof(TotalLevel));
        }
    }
}
