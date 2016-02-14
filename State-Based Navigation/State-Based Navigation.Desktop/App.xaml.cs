// Copyright (c) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.

using StateBasedNavigation.Desktop.Model;
using StateBasedNavigation.Desktop.ViewModels;
using System.Windows;

namespace StateBasedNavigation.Desktop
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            this.MainWindow =
                new MainWindow
                {
                    DataContext = new ChatViewModel(new ChatService())
                };

            this.MainWindow.Show();
        }
    }
}
