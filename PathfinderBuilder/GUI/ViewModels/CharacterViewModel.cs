using PathfinderBuilder;

namespace GUI.ViewModels
{
    public class CharacterViewModel : BaseViewModel
    {
        private readonly AbilitiesViewModel _abilitiesVM;
        private readonly RaceViewModel _raceVM;
        private readonly SkillsViewModel _skillsVM;
        private readonly LanguagesViewModel _languagesVM;
        private readonly Character _character;
        private readonly ClassesViewModel _classesViewModel;

        public RaceViewModel RaceVM
        {
            get { return _raceVM; }
        }

        public ClassesViewModel ClassesVM
        {
            get { return _classesViewModel; }
        }

        public SkillsViewModel SkillsVM
        {
            get { return _skillsVM; }
        }

        public AbilitiesViewModel AbilitiesVM
        {
            get { return _abilitiesVM; }
        }

        public LanguagesViewModel LanguagesVM
        {
            get { return _languagesVM; }
        }

        public Character Character { get { return _character; } }

        public CharacterViewModel()
        {
            _character = new Character();

            _raceVM = new RaceViewModel(this);
            _abilitiesVM = new AbilitiesViewModel(this);
            _classesViewModel = new ClassesViewModel(this);
            _skillsVM = new SkillsViewModel(this);
            _languagesVM = new LanguagesViewModel(this);
            
        }

    }
}
