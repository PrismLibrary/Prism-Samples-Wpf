using System.Windows;
using ModuleA;
using Modules.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Unity;

namespace Modules
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : PrismApplication
    {
        protected override Window CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            
        }

        protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
        {
            var moduleAType = typeof(ModuleAModule);
            moduleCatalog.AddModule(new ModuleInfo()
            {
                ModuleName = moduleAType.Name,
                ModuleType = moduleAType.AssemblyQualifiedName,
                InitializationMode = InitializationMode.OnDemand
            });
        }
    }
}
