using GUI.ViewModels;

namespace GUI.Commands
{
    public class AddRemoveArchtypeCommand : BaseCommand
    {
        private readonly ClassesViewModel _owner;

        public AddRemoveArchtypeCommand(ClassesViewModel owner)
        {
            _owner = owner;
        }

        public override bool CanExecute(object parameter)
        {
            if (!_owner.SelectedArchtype.IsUsed)
            {
                return _owner.SelectedArchtype.CanBeAdded;
            }
            return true;
        }

        public override void Execute(object parameter)
        {
            if (!_owner.SelectedArchtype.IsUsed)
            {
                _owner.SelectedLevel.Instance.AddArchtype(_owner.SelectedArchtype.Value);
                _owner.SelectedArchtype.IsUsed = true;
            }
            else
            {
                _owner.SelectedLevel.Instance.RemoveArchtype(_owner.SelectedArchtype.Value);
                _owner.SelectedArchtype.IsUsed = false;
            }
            _owner.UpdatedLevels();
        }
    }
}
