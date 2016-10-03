using GUI.ViewModels;

namespace GUI.Commands
{
    public class CreateClassLevelCommand : BaseCommand
    {
        private readonly ClassesViewModel _owner;

        public CreateClassLevelCommand(ClassesViewModel owner)
        {
            _owner = owner;
        }

        public override bool CanExecute(object parameter)
        {
            return _owner.SelectedAvailableClass != null;
        }

        public override void Execute(object parameter)
        {
            _owner.Levels.Add(new ClassWithLevelViewModel(_owner, _owner.SelectedAvailableClass.Instance));
            _owner.AvailableClasses.Remove(_owner.SelectedAvailableClass);
            _owner.UpdatedLevels();
        }
    }
}
