using Unity;
using Prism.Unity;
using BootstrapperShell.Views;
using System.Windows;

namespace BootstrapperShell
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
