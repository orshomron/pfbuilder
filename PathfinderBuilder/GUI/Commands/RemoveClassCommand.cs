using GUI.ViewModels;

namespace GUI.Commands
{
    public class RemoveClassCommand : BaseCommand
    {
        private readonly ClassesViewModel _owner;

        public RemoveClassCommand(ClassesViewModel owner)
        {
            _owner = owner;
        }

        public override bool CanExecute(object parameter)
        {
            return _owner.SelectedLevel != null;
        }

        public override void Execute(object parameter)
        {
            _owner.AvailableClasses.Add(new ClassViewModel(_owner, _owner.SelectedLevel.Instance));
            _owner.Levels.Remove(_owner.SelectedLevel);
            _owner.UpdatedLevels();
        }
    }
}
