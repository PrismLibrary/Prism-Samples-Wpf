namespace ModularityWithNinject.Desktop
{
    using System;
    using Prism.Modularity;

    /// <summary>
    /// A module for the quickstart.
    /// </summary>
    [Module(ModuleName = WellKnownModuleNames.ModuleF)]
    public class ModuleF : IModule
    {
        private IModuleTracker moduleTracker;

        /// <summary>
        /// Initializes a new instance of the <see cref="ModuleF"/> class.
        /// </summary>
        /// <param name="moduleTracker">The module tracker.</param>        
        public ModuleF(IModuleTracker moduleTracker)
        {
            if (moduleTracker == null)
            {
                throw new ArgumentNullException("moduleTracker");
            }

            this.moduleTracker = moduleTracker;
            this.moduleTracker.RecordModuleConstructed(WellKnownModuleNames.ModuleF);
        }

        /// <summary>
        /// Notifies the module that it has be initialized.
        /// </summary>
        public void Initialize()
        {
            this.moduleTracker.RecordModuleInitialized(WellKnownModuleNames.ModuleF);
        }
    }
}
