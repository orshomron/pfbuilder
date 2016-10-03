using System.Windows;
using System.Windows.Controls;
using GUI.ViewModels;

namespace GUI.XAMLExtensions
{
    public class SkillViewTemplateSelector : DataTemplateSelector
    {
        public override DataTemplate
            SelectTemplate(object item, DependencyObject container)
        {
            var element = container as FrameworkElement;

            if (element != null && item != null && item is SkillViewModel)
            {
                var withSubcategory = item as SkillWithSubcategoryViewModel;
                if (withSubcategory != null)
                {
                    return element.FindResource("SkillWithSubcategoryTemplate") as DataTemplate;
                }
                return element.FindResource("SkillTemplate") as DataTemplate;
            }

            return null;
        }
    }
}