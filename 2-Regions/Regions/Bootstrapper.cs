using Microsoft.Practices.Unity;
using Prism.Unity;
using Regions.Views;
using System.Windows;

namespace Regions
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
