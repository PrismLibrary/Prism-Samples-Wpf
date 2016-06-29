
using Microsoft.Practices.Unity;
using Prism.Modularity;
using Prism.Regions;
using Prism.Unity;
using ViewSwitchingNavigation.Contacts.Model;
using ViewSwitchingNavigation.Contacts.Views;
using ViewSwitchingNavigation.Contacts.ViewModels;
using ViewSwitchingNavigation.Infrastructure;

namespace ViewSwitchingNavigation.Contacts
{
    public class ContactsModule : IModule
    {
        private IUnityContainer unityContainer;
        private IRegionManager regionManager;

        public ContactsModule(IUnityContainer unityContainer, IRegionManager regionManager)
        {
            this.unityContainer = unityContainer;
            this.regionManager = regionManager;
        }

        public void Initialize()
        {
            this.unityContainer.RegisterType<IContactsService, ContactsService>(new ContainerControlledLifetimeManager());

            this.unityContainer.RegisterType<ContactsViewModel>(new ContainerControlledLifetimeManager());

            this.unityContainer.RegisterType<ContactsAvatarNavigationItemView>(new ContainerControlledLifetimeManager());
            this.unityContainer.RegisterType<ContactsDetailNavigationItemView>(new ContainerControlledLifetimeManager());

            this.unityContainer.RegisterType<ContactsView>(new ContainerControlledLifetimeManager());
            this.unityContainer.RegisterTypeForNavigation<ContactsView>();

            this.unityContainer.RegisterType<ContactAvatarView>(new ContainerControlledLifetimeManager());
            this.unityContainer.RegisterTypeForNavigation<ContactAvatarView>();

            this.unityContainer.RegisterType<ContactDetailView>(new ContainerControlledLifetimeManager());
            this.unityContainer.RegisterTypeForNavigation<ContactDetailView>();

            // todo: 16 - Contacts with two 'views'
            // 
            // The contacts module offers two navigation options.  One to show contacts with the details informatin
            // and the other to show contact avatars.  Each of these really navigate to the same 'view'
            // but provide additional information as part of the query string so the view can decide to 
            // display details or avatars.
            this.regionManager.RegisterViewWithRegion(RegionNames.MainNavigationRegion, typeof(ContactsAvatarNavigationItemView));
            this.regionManager.RegisterViewWithRegion(RegionNames.MainNavigationRegion, typeof(ContactsDetailNavigationItemView));
        }
    }
}
