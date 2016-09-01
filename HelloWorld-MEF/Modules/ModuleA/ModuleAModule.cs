using ModuleA.Views;
using Prism.Mef.Modularity;
using Prism.Modularity;
using Prism.Regions;
using System.ComponentModel.Composition;

//TODO: 05. Create your first Module by creating a new Class Library project and adding a ModuleInit.cs file
namespace ModuleA
{
    // TODO: 06. Implement the IModule interface.  Using the ModuleExport attribute identifies
    //           this class as the module initializer class for Prism
    [ModuleExport(typeof(ModuleAModule))]
    public class ModuleAModule : IModule
    {
        IRegionManager _regionManager;

        //TODO: 07.  Implement the module constructor to bring in required objects.
        //          When Prism loads the module it will instantiate this class using
        //          MEF, MEF will then inject a Region Manager instance.
        [ImportingConstructor]
        public ModuleAModule(IRegionManager regionManager)
        {
            _regionManager = regionManager;
        }

        //TODO: 08. Implement the required Initialize method to provide an entry point
        //         for your modules startup code.  Here we are registering ViewA with
        //         with the MEF Container and also adding it to the "MainRegion"
        //         which was defined on the MainWindow in the HelloWorld project.
        public void Initialize()
        {
            _regionManager.RegisterViewWithRegion("MainRegion", typeof(ViewA));
        }
    }
}
