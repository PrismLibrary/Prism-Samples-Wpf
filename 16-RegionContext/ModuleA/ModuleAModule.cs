using ModuleA.Views;
using Prism.Modularity;
using Prism.Regions;
using System;

namespace ModuleA
{
    public class ModuleAModule : IModule
    {
        IRegionManager _regionManager;

        public ModuleAModule(RegionManager regionManager)
        {
            _regionManager = regionManager;
        }

        public void Initialize()
        {
            _regionManager.RegisterViewWithRegion("ContentRegion", typeof(PersonList));
            _regionManager.RegisterViewWithRegion("PersonDetailsRegion", typeof(PersonDetail));
        }
    }
}