using Prism.Ioc;
using System.Windows;
using UsingCustomWindow.ViewModels;
using UsingCustomWindow.Views;

namespace UsingCustomWindow
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        protected override Window CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterDialogWindow<MyCustomWindow>();
            containerRegistry.RegisterDialog<NotificationDialog, NotificationDialogViewModel>();
        }
    }
}
