using ActivationDeactivation.Views;
using Prism.Ioc;
using Prism.Unity;
using System.Windows;

namespace ActivationDeactivation
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
    }
}
