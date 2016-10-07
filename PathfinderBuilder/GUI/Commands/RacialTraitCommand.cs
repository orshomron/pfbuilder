using System.Linq;
using GUI.ViewModels;

namespace GUI.Commands
{
    public class RacialTraitCommand : BaseCommand
    {
        private readonly RaceViewModel _owner;

        public RacialTraitCommand(RaceViewModel raceViewModel)
        {
            _owner = raceViewModel;
        }

        public override bool CanExecute(object parameter)
        {
            return true;
        }

        public override void Execute(object parameter)
        {
            var traitToAdd = _owner.SelectedAvailableTrait;

            if (traitToAdd == null)
            {
                return;
            }

            var categoriesToRemove = traitToAdd.Trait.TraitCategory;
            var vmsToRemove = _owner.SelectedTraits.Where(vm => ((ulong)vm.Trait.TraitCategory & (ulong)categoriesToRemove) > 0).ToList();

            foreach (var vm in vmsToRemove)
            {
                _owner.SelectedTraits.Remove(vm);
                _owner.AvailableTraits.Add(vm);
            }

            _owner.SelectedTraits.Add(traitToAdd);
            _owner.AvailableTraits.Remove(traitToAdd);

            foreach (var vm in _owner.AvailableTraits)
            {
                vm.ChangedSelectedTraits();
            }
        }
    }
}
