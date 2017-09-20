using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GUI.ViewModels;
using PathfinderBuilder;

namespace GUI.Commands
{
    public class AddFeatCommand : BaseCommand
    {
        private readonly FeatsViewModel _owner;

        public AddFeatCommand(FeatsViewModel owner)
        {
            _owner = owner;
        }

        public override bool CanExecute(object parameter)
        {
            throw new NotImplementedException();
        }

        public override void Execute(object parameter)
        {
            _owner.FeatsChagned();
            throw new NotImplementedException();
        }

    }
}
