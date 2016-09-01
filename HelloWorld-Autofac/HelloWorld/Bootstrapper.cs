using Autofac;
using HelloWorld.Views;
using ModuleA;
using Prism.Autofac;
using Prism.Modularity;
using System.Windows;

namespace HelloWorld
{
    //TODO: 01. Create a Bootstrapper Class using AutofacBootstrapper
    public class Bootstrapper : AutofacBootstrapper
    {
        //TODO: 02. Override the CreateShell returning an instance of your shell.
        protected override DependencyObject CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        //TODO: 03. Override the InitializeShell setting the MainWindow on the application and showing the shell.
        protected override void InitializeShell()
        {
            Application.Current.MainWindow.Show();
        }

        //TODO: 04. Override the ConfigureModuleCatalog 
        protected override void ConfigureModuleCatalog()
        {
            ModuleCatalog catalog = (ModuleCatalog)ModuleCatalog;
            // In this example we are initializing a single Module in code.
            catalog.AddModule(typeof(ModuleAModule));
        }
    }
}
