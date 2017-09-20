using PathfinderBuilder;

namespace GUI.ViewModels
{
    public class CharacterViewModel : BaseViewModel
    {
        public RaceViewModel RaceVM { get; }

        public ClassesViewModel ClassesVM { get; }

        public SkillsViewModel SkillsVM { get; }

        public AbilitiesViewModel AbilitiesVM { get; }

        public LanguagesViewModel LanguagesVM { get; }

        public FeatsViewModel FeatsVM { get; }

        public Character Character { get; }

        public CharacterViewModel()
        {
            Character = new Character();

            RaceVM = new RaceViewModel(this);
            AbilitiesVM = new AbilitiesViewModel(this);
            ClassesVM = new ClassesViewModel(this);
            SkillsVM = new SkillsViewModel(this);
            FeatsVM = new FeatsViewModel(this);
            LanguagesVM = new LanguagesViewModel(this);

        }

    }
}
