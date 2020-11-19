using Modules.Views;
using Prism.Modularity;
using Prism.Unity;
using Prism.Ioc;
using System.Windows;
using System;

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

        protected override IModuleCatalog CreateModuleCatalog()
        {
            return new XamlModuleCatalog(new Uri("/Modules;component/ModuleCatalog.xaml", UriKind.Relative));
        }
    }
}
