using GUI.ViewModels;
using PathfinderBuilder;

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
            if (_owner.SelectedAvailableClass == null)
            {
                return false;
            }
            
            var isPrestige = _owner.SelectedAvailableClass.Instance is PrestigeClass;
            if (isPrestige)
            {
                var prestige = _owner.SelectedAvailableClass.Instance as PrestigeClass;
                return prestige.CanAddClass(_owner.Owner.Character);
            }

            return true;
        }

        public override void Execute(object parameter)
        {
            _owner.Levels.Add(new ClassWithLevelViewModel(_owner, _owner.SelectedAvailableClass.Instance));
            _owner.AvailableClasses.Remove(_owner.SelectedAvailableClass);
            _owner.UpdatedLevels();
        }
    }
}
