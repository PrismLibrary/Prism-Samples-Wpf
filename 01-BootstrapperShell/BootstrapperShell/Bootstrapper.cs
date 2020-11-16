using Unity;
using Prism.Unity;
using BootstrapperShell.Views;
using System.Windows;
using Prism.Ioc;

namespace BootstrapperShell
{
    class Bootstrapper : PrismBootstrapper
    {
        protected override DependencyObject CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            
        }
    }
}
