using Microsoft.Practices.Unity;
using Prism.Unity;
using UsingCompositeCommands.Views;
using System.Windows;
using Prism.Modularity;
using UsingCompositeCommands.Core;

namespace UsingCompositeCommands
{
    class Bootstrapper : UnityBootstrapper
    {
        protected override DependencyObject CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        protected override void InitializeShell()
        {
            Application.Current.MainWindow.Show();
        }

        protected override void ConfigureContainer()
        {
            base.ConfigureContainer();

            Container.RegisterType<IApplicationCommands, ApplicationCommands>(new ContainerControlledLifetimeManager());
        }

        protected override void ConfigureModuleCatalog()
        {
            var catalog = (ModuleCatalog)ModuleCatalog;
            catalog.AddModule(typeof(ModuleA.ModuleAModule));
        }
    }
}
