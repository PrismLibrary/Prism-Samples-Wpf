
using Microsoft.Practices.Unity;
using Prism.Modularity;
using Prism.Regions;
using Prism.Unity;
using ViewSwitchingNavigation.Email.Model;
using ViewSwitchingNavigation.Email.Views;
using ViewSwitchingNavigation.Infrastructure;

namespace ViewSwitchingNavigation.Email
{
    public class EmailModule : IModule
    {
        private IUnityContainer unityContainer;
        private IRegionManager regionManager;

        public EmailModule(IUnityContainer unityContainer, IRegionManager regionManager)
        {
            this.unityContainer = unityContainer;
            this.regionManager = regionManager;
        }

        public void Initialize()
        {
            this.unityContainer.RegisterType<EmailNavigationItemView>();
            this.unityContainer.RegisterType<IEmailService, EmailService>(new ContainerControlledLifetimeManager());

            this.unityContainer.RegisterType<InboxView>();
            this.unityContainer.RegisterTypeForNavigation<InboxView>();

            this.unityContainer.RegisterTypeForNavigation<EmailView>();

            this.unityContainer.RegisterType<ComposeEmailView>();
            this.unityContainer.RegisterTypeForNavigation<ComposeEmailView>();

            this.regionManager.RegisterViewWithRegion(RegionNames.MainNavigationRegion, typeof(EmailNavigationItemView));
        }
    }
}
