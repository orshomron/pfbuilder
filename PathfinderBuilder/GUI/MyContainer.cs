using GUI.ViewModels;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;

namespace GUI
{
    public class MyContainer
    {
        private readonly IUnityContainer _container;

        public MyContainer()
        {
            _container = new UnityContainer().LoadConfiguration();
        }

        public IUnityContainer Container
        {
            get
            {
                return _container;
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
            "CA1822:MarkMembersAsStatic",
            Justification = "This non-static member is needed for data binding purposes.")]
        public CharacterViewModel CharacterViewModel
        {
            get
            {
                return _container.Resolve<CharacterViewModel>();
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
            "CA1822:MarkMembersAsStatic",
            Justification = "This non-static member is needed for data binding purposes.")]
        public SkillsViewModel SkillsViewModel
        {
            get
            {
                return _container.Resolve<SkillsViewModel>();
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
            "CA1822:MarkMembersAsStatic",
            Justification = "This non-static member is needed for data binding purposes.")]
        public RaceViewModel RaceViewModel
        {
            get
            {
                return _container.Resolve<RaceViewModel>();
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
            "CA1822:MarkMembersAsStatic",
            Justification = "This non-static member is needed for data binding purposes.")]
        public LanguagesViewModel LanguagesViewModel
        {
            get
            {
                return _container.Resolve<LanguagesViewModel>();
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
            "CA1822:MarkMembersAsStatic",
            Justification = "This non-static member is needed for data binding purposes.")]
        public ClassesViewModel ClassesViewModel
        {
            get
            {
                return _container.Resolve<ClassesViewModel>();
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
            "CA1822:MarkMembersAsStatic",
            Justification = "This non-static member is needed for data binding purposes.")]
        public AbilitiesViewModel AbilitiesViewModel
        {
            get
            {
                return _container.Resolve<AbilitiesViewModel>();
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
            "CA1822:MarkMembersAsStatic",
            Justification = "This non-static member is needed for data binding purposes.")]
        public FeatsViewModel FeatsViewModel
        {
            get
            {
                return _container.Resolve<FeatsViewModel>();
            }
        }
    }
}
