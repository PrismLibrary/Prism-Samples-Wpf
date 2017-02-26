using Microsoft.Practices.Unity;
using ModuleA.ViewModels;
using ModuleA.Views;
using Prism.Modularity;
using Prism.Regions;
using System;

namespace ModuleA
{
    public class ModuleAModule : IModule
    {
        IRegionManager _regionManager;
        IUnityContainer _container;

        public ModuleAModule(RegionManager regionManager, IUnityContainer container)
        {
            _regionManager = regionManager;
            _container = container;
        }

        public void Initialize()
        {
            IRegion region = _regionManager.Regions["ContentRegion"];

            var tabA = _container.Resolve<TabView>();
            SetTitle(tabA, "Tab A");
            region.Add(tabA);

            var tabB = _container.Resolve<TabView>();
            SetTitle(tabB, "Tab B");
            region.Add(tabB);

            var tabC = _container.Resolve<TabView>();
            SetTitle(tabC, "Tab C");
            region.Add(tabC);

        }

        void SetTitle(TabView tab, string title)
        {
            (tab.DataContext as TabViewModel).Title = title;
        }
    }
}