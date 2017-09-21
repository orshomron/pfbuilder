using PathfinderBuilder;

namespace GUI.ViewModels
{
    public class CharacterViewModel : BaseViewModel
    {
        private int _selectedTabIndex;

        public RaceViewModel RaceVM { get; }

        public ClassesViewModel ClassesVM { get; }

        public SkillsViewModel SkillsVM { get; }

        public AbilitiesViewModel AbilitiesVM { get; }

        public LanguagesViewModel LanguagesVM { get; }

        public FeatsViewModel FeatsVM { get; }

        public Character Character { get; }

        public int SelectedTabIndex
        {
            get { return _selectedTabIndex; }
            set
            {
                _selectedTabIndex = value;
                AbilitiesVM.ReloadModelValues();
                SkillsVM.ReloadModelValues();
                RaceVM.ReloadModelValues();
                LanguagesVM.ReloadModelValues();
                FeatsVM.ReloadModelValues();
                ClassesVM.ReloadModelValues();
            }
        }

        public CharacterViewModel()
        {
            Character = new Character();

            RaceVM = new RaceViewModel(this);
            AbilitiesVM = new AbilitiesViewModel(Character);
            ClassesVM = new ClassesViewModel(this);
            SkillsVM = new SkillsViewModel(this);
            FeatsVM = new FeatsViewModel(this);
            LanguagesVM = new LanguagesViewModel(this);

        }

        public override void ReloadModelValues()
        {
            
        }
    }
}
