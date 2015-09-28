using ModuleA.Views;
using Prism.Modularity;
using Prism.Regions;

//TODO: 05. Create your first Module by creating a new Class Library project and adding a ModuleInit.cs file
namespace ModuleA
{
    // TODO: 06. Implement the IModule interface
    public class ModuleAModule : IModule
    {
        IRegionManager _regionManager;

        //TODO: 07.  Implement the module constructor to bring in required objects.
        //          When Prism loads the module it will instantiate this class using
        //          Autofac DI, Autofac will then inject a Region Manager instance.
        public ModuleAModule(IRegionManager regionManager)
        {
            _regionManager = regionManager;
        }

        //TODO: 08. Implement the required Initialize method to provide an entry point
        //         for your modules startup code.  Here we are registering ViewA with
        //         with the Autofac DI Container and also adding it to the "MainRegion"
        //         which was defined on the MainWindow in the HelloWorld project.
        public void Initialize()
        {
            _regionManager.RegisterViewWithRegion("MainRegion", typeof(ViewA));
        }
    }
}
