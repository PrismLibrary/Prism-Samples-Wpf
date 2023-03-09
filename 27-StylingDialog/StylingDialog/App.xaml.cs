using Prism.Ioc;
using StylingDialog.ViewModels;
using StylingDialog.Views;
using System.Windows;

namespace StylingDialog
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
            containerRegistry.RegisterDialog<NotificationDialog, NotificationDialogViewModel>();
        }
    }
}
