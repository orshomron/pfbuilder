using System.ComponentModel;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;

namespace GUI.ViewModels
{
    public abstract class BaseViewModel<T> : BaseViewModel
    {
        protected T Model { get; }

        public BaseViewModel(T model)
        {
            Model = model;
        }
    }

    public abstract class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public abstract void ReloadModelValues();

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
