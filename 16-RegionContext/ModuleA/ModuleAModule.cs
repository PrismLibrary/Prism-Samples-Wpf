using ModuleA.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

namespace ModuleA
{
    public class ModuleAModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
            var regionManager = containerProvider.Resolve<IRegionManager>();
            regionManager.RegisterViewWithRegion("ContentRegion", typeof(PersonList));
            regionManager.RegisterViewWithRegion("PersonDetailsRegion", typeof(PersonDetail));
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            
        }
    }
}