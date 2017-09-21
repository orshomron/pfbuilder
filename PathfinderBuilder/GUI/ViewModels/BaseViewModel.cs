using System.ComponentModel;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;

namespace GUI.ViewModels
{
    public abstract class BaseViewModel<T> : BaseViewModel
    {
        public T Model { get; }

        protected BaseViewModel(T model)
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
            var handler = PropertyChanged;
            handler?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
