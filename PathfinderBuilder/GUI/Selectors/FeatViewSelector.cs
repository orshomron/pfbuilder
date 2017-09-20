using System;
using System.Windows;
using System.Windows.Controls;
using GUI.ViewModels;

namespace GUI.Selectors
{
    public class FeatViewSelector : DataTemplateSelector
    {
        private const string SkillSelectTemplateName = "SkillSelectFeatTemplate";
        private const string RegularTemplateName = "SimpleFeatTemplate";

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            var element = container as FrameworkElement;

            if (element == null)
            {
                throw new InvalidOperationException("container was not a framework element");
            }

            var vm = item as FeatViewModel;
            
            if (vm is FeatWithSkillSelectionViewModel)
            {
                return element.FindResource(SkillSelectTemplateName) as DataTemplate;
            }
            return element.FindResource(RegularTemplateName) as DataTemplate;
        }
    }
}
