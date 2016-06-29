

using System;
using Prism.Modularity;
using Prism.Regions;
using ViewSwitchingNavigation.Infrastructure;
using System.Windows;

namespace ViewSwitchingNavigation
{
    public partial class Shell : Window
    {
        private const string EmailModuleName = "EmailModule";
        private static Uri InboxViewUri = new Uri("/InboxView", UriKind.Relative);

        private IRegionManager regionManager;
        private IModuleManager moduleManager;

        public Shell(IRegionManager regionManager, IModuleManager moduleManager)
        {
            this.regionManager = regionManager;
            this.moduleManager = moduleManager;

            InitializeComponent();

            this.moduleManager.LoadModuleCompleted +=
                (s, e) =>
                {
                    // todo: 01 - Navigation on when modules are loaded.
                    // When using region navigation, be sure to use it consistently
                    // to ensure you get proper journal behavior.  If we mixed
                    // usage of adding views directly to regions, such as through
                    // RegionManager.AddToRegion, and then use RegionManager.RequestNavigate,
                    // we may not be able to navigate back correctly.
                    // 
                    // Here, we wait until the module we want to start with is
                    // loaded and then navigate to the view we want to display
                    // initially.
                    //     
                    if (e.ModuleInfo.ModuleName == EmailModuleName)
                    {
                        this.regionManager.RequestNavigate(
                            RegionNames.MainContentRegion,
                            InboxViewUri);
                    }
                };
        }
    }
}
