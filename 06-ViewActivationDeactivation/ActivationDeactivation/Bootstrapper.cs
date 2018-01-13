using Microsoft.Practices.Unity;
using Prism.Unity;
using ActivationDeactivation.Views;
using System.Windows;

namespace ActivationDeactivation
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
