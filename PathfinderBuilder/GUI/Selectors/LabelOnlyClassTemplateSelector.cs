using System;
using System.Windows;
using System.Windows.Controls;
using GUI.ViewModels;

namespace GUI.Selectors
{
    public class LabelOnlyClassTemplateSelector : DataTemplateSelector
    {
        private const string PrestigeTemplateName = "LabelOnlyPrestigeClassViewModelTemplate";
        private const string RegularTemplateName = "LabelOnlyClassViewModelTemplate";

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            var element = container as FrameworkElement;

            if (element == null)
            {
                throw new InvalidOperationException("container was not a framework element");
            }

            var vm = item as ClassViewModel;
            
            if (vm is PrestigeClassViewModel)
            {
                return element.FindResource(PrestigeTemplateName) as DataTemplate;
            }
            return element.FindResource(RegularTemplateName) as DataTemplate;
        }
    }
}
