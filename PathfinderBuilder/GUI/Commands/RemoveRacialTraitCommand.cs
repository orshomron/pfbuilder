using GUI.ViewModels;
using PathfinderBuilder;

namespace GUI.Commands
{
    public class RemoveRacialTraitCommand : BaseCommand
    {
        private readonly RaceViewModel _owner;

        public RemoveRacialTraitCommand(RaceViewModel raceViewModel)
        {
            _owner = raceViewModel;
        }

        public override bool CanExecute(object parameter)
        {
            return (ulong)_owner.SelectedSelectedTrait.Trait.TraitCategory == (ulong)BaseRacialTraits.None;
        }

        public override void Execute(object parameter)
        {
            var traitToAdd = _owner.SelectedSelectedTrait;

            _owner.SelectedTraits.Remove(traitToAdd);
            _owner.AvailableTraits.Add(traitToAdd);

        }
    }
}
