using Microsoft.Practices.Unity;
using Prism.Unity;
using ViewModelLocator.Views;
using System.Windows;

namespace ViewModelLocator
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
