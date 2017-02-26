using Microsoft.Practices.Unity;
using ModuleA;
using Prism.Modularity;
using Prism.Unity;
using RegionContext.Views;
using System.Windows;

namespace RegionContext
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

        protected override void ConfigureModuleCatalog()
        {
            var catalog = (ModuleCatalog)ModuleCatalog;
            catalog.AddModule(typeof(ModuleAModule));
        }
    }
}
