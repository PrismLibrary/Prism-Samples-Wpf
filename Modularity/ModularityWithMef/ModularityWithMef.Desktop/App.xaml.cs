// Copyright (c) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.

namespace ModularityWithMef.Desktop
{
    using System.Windows;

    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // The boostrapper will create the Shell instance, so the App.xaml does not have a StartupUri.
            QuickStartBootstrapper bootstrapper = new QuickStartBootstrapper();
            bootstrapper.Run();
        }
    }
}
