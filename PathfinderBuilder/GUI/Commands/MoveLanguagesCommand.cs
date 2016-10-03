using System;
using System.Collections.ObjectModel;
using GUI.ViewModels;

namespace GUI.Commands
{
    public class MoveLanguagesAvailableToKnownCommand : BaseCommand
    {
        private readonly ObservableCollection<LanguageViewModel> _destination;
        private readonly LanguagesViewModel _owner;
        private readonly ObservableCollection<LanguageViewModel> _source;

        public MoveLanguagesAvailableToKnownCommand(LanguagesViewModel vm)
        {
            _source = vm.AvailableLanguages;
            _destination = vm.KnownLanguages;
            _owner = vm;
        }

        public override bool CanExecute(object parameter)
        {
            var vm = parameter as LanguageViewModel;
            if (vm == null)
            {
                return false;
            }

            if (vm.IsRacial)
            {
                return false;
            }

            return _owner.LeftToSelect > 0;
        }

        public override void Execute(object parameter)
        {
            var vm = parameter as LanguageViewModel;
            if (vm == null)
            {
                throw new ArgumentException("Parameter must be a valid language view model", "parameter");
            }

            _source.Remove(vm);
            _destination.Add(vm);
        }
    }

    public class MoveLanguagesKnownToAvailableCommand : BaseCommand
    {
        private readonly ObservableCollection<LanguageViewModel> _destination;
        private readonly ObservableCollection<LanguageViewModel> _source;

        public MoveLanguagesKnownToAvailableCommand(LanguagesViewModel vm)
        {
            _source = vm.KnownLanguages;
            _destination = vm.AvailableLanguages;
        }

        public override bool CanExecute(object parameter)
        {
            var vm = parameter as LanguageViewModel;
            if (vm == null)
            {
                return false;
            }

            if (vm.IsRacial)
            {
                return false;
            }

            return true;
        }

        public override void Execute(object parameter)
        {
            var vm = parameter as LanguageViewModel;
            if (vm == null)
            {
                throw new ArgumentException("Parameter must be a valid language view model", "parameter");
            }

            _source.Remove(vm);
            _destination.Add(vm);
        }
    }
}
