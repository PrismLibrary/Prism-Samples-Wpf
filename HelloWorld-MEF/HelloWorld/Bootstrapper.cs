using HelloWorld.Views;
using ModuleA;
using Prism.Mef;
using Prism.Modularity;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Windows;

namespace HelloWorld
{
    //TODO: 01. Create a Bootstrapper Class using MEF Bootstrapper
    public class Bootstrapper : MefBootstrapper
    {
        //TODO: 02. Override the CreateShell returning an instance of your shell.
        protected override DependencyObject CreateShell()
        {
            return Container.GetExportedValue<MainWindow>();
        }

        //TODO: 03. Override the InitializeShell setting the MainWindow on the application and showing the shell.
        protected override void InitializeShell()
        {
            Application.Current.MainWindow.Show();
        }

        //TODO: 04. Override the ConfigureAggregateCatalog.  Add the new assembly catalogs which will register all
        //          classes with [Export] attributes
        protected override void ConfigureAggregateCatalog()
        {
            base.ConfigureAggregateCatalog();
            this.AggregateCatalog.Catalogs.Add(new AssemblyCatalog(typeof(Bootstrapper).Assembly));
            this.AggregateCatalog.Catalogs.Add(new AssemblyCatalog(typeof(ModuleAModule).Assembly));
        }
    }
}
