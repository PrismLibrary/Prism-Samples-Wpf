
using Microsoft.Practices.Unity;
using Prism.Modularity;
using Prism.Regions;
using Prism.Unity;
using ViewSwitchingNavigation.Calendar.Model;
using ViewSwitchingNavigation.Calendar.Views;
using ViewSwitchingNavigation.Calendar.ViewModels;
using ViewSwitchingNavigation.Infrastructure;

namespace ViewSwitchingNavigation.Calendar
{
    public class CalendarModule : IModule
    {
        private IUnityContainer unityContainer;
        private IRegionManager regionManager;

        public CalendarModule(IUnityContainer unityContainer, IRegionManager regionManager)
        {
            this.unityContainer = unityContainer;
            this.regionManager = regionManager;
        }

        public void Initialize()
        {
            this.unityContainer.RegisterType<ICalendarService, CalendarService>(new ContainerControlledLifetimeManager());
            this.unityContainer.RegisterType<CalendarViewModel>();
            this.unityContainer.RegisterType<CalendarNavigationItemView>();
            this.unityContainer.RegisterType<CalendarView>();

            this.unityContainer.RegisterTypeForNavigation<CalendarView>();

            // todo: 11 - Pre-populating regions with items
            // Items may be placed in regions prior to navigating to them.  In this case, since we're registering
            // the type CalendarNavigationItemView, it will be created and added to the region when the MainNavigationRegion 
            // is loaded.  Even though it is created when the control associated with the region becomes visible,
            // the view itself won't be visible until it's navigated to. 
            //
            // Anything placed in a region in this manner will not be added to the navigation journal.
            this.regionManager.RegisterViewWithRegion(RegionNames.MainNavigationRegion, typeof(CalendarNavigationItemView));
        }
    }
}
