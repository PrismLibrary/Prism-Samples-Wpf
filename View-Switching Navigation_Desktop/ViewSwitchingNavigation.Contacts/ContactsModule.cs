

using System.ComponentModel.Composition;
using Prism.Mef.Modularity;
using Prism.Modularity;
using Prism.Regions;
using ViewSwitchingNavigation.Contacts.Views;
using ViewSwitchingNavigation.Infrastructure;

namespace ViewSwitchingNavigation.Contacts
{
    [ModuleExport(typeof(ContactsModule))]
    public class ContactsModule : IModule
    {
        [Import]
        public IRegionManager RegionManager;

        public void Initialize()
        {
            // todo: 16 - Contacts with two 'views'
            // 
            // The contacts module offers two navigation options.  One to show contacts with the details informatin
            // and the other to show contact avatars.  Each of these really navigate to the same 'view'
            // but provide additional information as part of the query string so the view can decide to 
            // display details or avatars.
            this.RegionManager.RegisterViewWithRegion(RegionNames.MainNavigationRegion, typeof(ContactsAvatarNavigationItemView));
            this.RegionManager.RegisterViewWithRegion(RegionNames.MainNavigationRegion, typeof(ContactsDetailNavigationItemView));
        }
    }
}
