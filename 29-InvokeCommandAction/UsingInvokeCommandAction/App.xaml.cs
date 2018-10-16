using Prism.Ioc;
using Prism.Unity;
using System.Windows;
using UsingInvokeCommandAction.Views;

namespace UsingInvokeCommandAction
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
