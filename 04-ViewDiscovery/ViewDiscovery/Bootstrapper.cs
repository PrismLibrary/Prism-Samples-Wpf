using Microsoft.Practices.Unity;
using Prism.Unity;
using ViewDiscovery.Views;
using System.Windows;

namespace ViewDiscovery
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
    }
}
