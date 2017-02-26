using Microsoft.Practices.Unity;
using Prism.Unity;
using UsingInvokeCommandAction.Views;
using System.Windows;

namespace UsingInvokeCommandAction
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
