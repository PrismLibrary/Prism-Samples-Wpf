

namespace ModularityWithUnity.Desktop
{
    using System;
    using System.Windows;
    using Prism.Logging;
    using Prism.Modularity;
    using Prism.Unity;
    using Microsoft.Practices.ServiceLocation;
    using Microsoft.Practices.Unity;
    using ModuleTracking;

    /// <summary>
    /// Initializes Prism to start this quickstart Prism application to use Unity.
    /// </summary>
    public class QuickStartBootstrapper : UnityBootstrapper
    {
        private readonly CallbackLogger callbackLogger = new CallbackLogger();

        /// <summary>
        /// Creates the shell or main window of the application.
        /// </summary>
        /// <returns>The shell of the application.</returns>
        /// <remarks>
        /// If the returned instance is a <see cref="DependencyObject"/>, the
        /// <see cref="Bootstrapper"/> will attach the default <seealso cref="Prism.Regions.IRegionManager"/> of
        /// the application in its <see cref="Prism.Regions.RegionManager.RegionManagerProperty"/> attached property
        /// in order to be able to add regions by using the <seealso cref="Prism.Regions.RegionManager.RegionNameProperty"/>
        /// attached property from XAML.
        /// </remarks>
        protected override DependencyObject CreateShell()
        {
            return ServiceLocator.Current.GetInstance<Shell>();
        }

        /// <summary>
        /// Initializes the shell.
        /// </summary>
        /// <remarks>
        /// The base implemention ensures the shell is composed in the container.
        /// </remarks>
        protected override void InitializeShell()
        {
            base.InitializeShell();

            Application.Current.MainWindow = (Window)this.Shell;
            Application.Current.MainWindow.Show();
        }

        /// <summary>
        /// Create the <see cref="ILoggerFacade"/> used by the bootstrapper.
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        /// The base implementation returns a new TextLogger.
        /// </remarks>
        protected override ILoggerFacade CreateLogger()
        {
            // Because the Shell is displayed after most of the interesting boostrapper work has been performed,
            // this quickstart uses a special logger class to hold on to early log entries and display them 
            // after the UI is visible.
            return this.callbackLogger;
        }

        /// <summary>
        /// Configures the <see cref="IUnityContainer"/>. May be overwritten in a derived class to add specific
        /// type mappings required by the application.
        /// </summary>
        protected override void ConfigureContainer()
        {
            base.ConfigureContainer();

            this.RegisterTypeIfMissing(typeof(IModuleTracker), typeof(ModuleTracker), true);

            this.Container.RegisterInstance<CallbackLogger>(this.callbackLogger);
        }

        /// <summary>
        /// Returns the module catalog that will be used to initialize the modules.
        /// </summary>
        /// <returns>
        /// An instance of <see cref="IModuleCatalog"/> that will be used to initialize the modules.
        /// </returns>
        /// <remarks>
        /// When using the default initialization behavior, this method must be overwritten by a derived class.
        /// </remarks>
        protected override IModuleCatalog CreateModuleCatalog()
        {
            return new AggregateModuleCatalog();
        }

        protected override void ConfigureModuleCatalog()
        {
            // Module A is defined in the code.
            Type moduleAType = typeof(ModuleA);
            ModuleCatalog.AddModule(new ModuleInfo(moduleAType.Name, moduleAType.AssemblyQualifiedName));

            // Module C is defined in the code.
            Type moduleCType = typeof(ModuleC);
            ModuleCatalog.AddModule(new ModuleInfo()
            {
                ModuleName = moduleCType.Name,
                ModuleType = moduleCType.AssemblyQualifiedName,
                InitializationMode = InitializationMode.OnDemand
            });

            // Module B and Module D are copied to a directory as part of a post-build step.
            // These modules are not referenced in the project and are discovered by inspecting a directory.
            // Both projects have a post-build step to copy themselves into that directory.
            DirectoryModuleCatalog directoryCatalog = new DirectoryModuleCatalog() { ModulePath = @".\DirectoryModules" };
            ((AggregateModuleCatalog)ModuleCatalog).AddCatalog(directoryCatalog);

            // Module E and Module F are defined in configuration.
            ConfigurationModuleCatalog configurationCatalog = new ConfigurationModuleCatalog();
            ((AggregateModuleCatalog)ModuleCatalog).AddCatalog(configurationCatalog);

        }
    }
}
