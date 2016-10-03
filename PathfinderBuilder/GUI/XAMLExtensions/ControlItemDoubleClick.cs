using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace GUI.XAMLExtensions
{
    public class ControlItemDoubleClick : DependencyObject
    {
        public static readonly DependencyProperty CommandProperty =
       DependencyProperty.RegisterAttached("Command", typeof(ICommand), typeof(ControlItemDoubleClick), new PropertyMetadata(OnChangedCommand));

        public static readonly DependencyProperty CommandParameterProperty =
            DependencyProperty.Register("CommandParameter", typeof(object), typeof(ControlItemDoubleClick), new PropertyMetadata(default(object)));

        public static object GetCommandParameter(Control target)
        {
            return target.GetValue(CommandParameterProperty);
        }

        public static void SetCommandParameter(Control target, object value)
        {
            target.SetValue(CommandParameterProperty, value);
        }

        public static ICommand GetCommand(Control target)
        {
            return (ICommand)target.GetValue(CommandProperty);
        }

        public static void SetCommand(Control target, ICommand value)
        {
            target.SetValue(CommandProperty, value);
        }

        private static void OnChangedCommand(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = d as Control;
            control.PreviewMouseDoubleClick += Element_PreviewMouseDoubleClick;
        }

        private static void Element_PreviewMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var control = sender as Control;
            ICommand command = GetCommand(control);
            var param = GetCommandParameter(control);

            if (command.CanExecute(param))
            {
                command.Execute(param);
                e.Handled = true;
            }
        }
    }
}