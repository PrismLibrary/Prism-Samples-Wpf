// Copyright (c) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.

using System;
using System.Windows;

namespace Commanding
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            CommandingBootstrapper bootstrapper = new CommandingBootstrapper();
            bootstrapper.Run();
        }
    }
}
