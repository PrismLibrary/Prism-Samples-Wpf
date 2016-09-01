// Copyright (c) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.

using System;
using System.Windows;
using Prism.Modularity;
using Prism.Unity;
using UIComposition.Shell.Views;

namespace UIComposition.Shell
{
    public class UICompositionBootstrapper : UnityBootstrapper
    {
        // TODO: 02 - The Shell loads the EmployeeModule, as specified in the module catalog (ModuleCatalog.xaml).

        protected override void ConfigureModuleCatalog()
        {
            base.ConfigureModuleCatalog();

            ModuleCatalog moduleCatalog = (ModuleCatalog)this.ModuleCatalog;
            moduleCatalog.AddModule(typeof(EmployeeModule.ModuleInit));
        }

        protected override DependencyObject CreateShell()
        {
            // Use the container to create an instance of the shell.
            ShellView view = this.Container.TryResolve<ShellView>();
            return view;
        }

        protected override void InitializeShell()
        {
            base.InitializeShell();
            App.Current.MainWindow = (Window)this.Shell;
            App.Current.MainWindow.Show();
        }
    }
}