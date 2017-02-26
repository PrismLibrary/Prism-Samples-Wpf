using Microsoft.Practices.Unity;
using Prism.Unity;
using ViewInjection.Views;
using System.Windows;

namespace ViewInjection
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
