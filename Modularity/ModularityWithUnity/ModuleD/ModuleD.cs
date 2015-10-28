

using System;
using Prism.Modularity;
using ModuleTracking;

namespace ModularityWithUnity.Desktop
{
    /// <summary>
    /// A module for the quickstart.
    /// </summary>
    [Module(ModuleName = WellKnownModuleNames.ModuleD)]
    public class ModuleD : IModule
    {
        private IModuleTracker moduleTracker;

        /// <summary>
        /// Initializes a new instance of the <see cref="ModuleD"/> class.
        /// </summary>
        /// <param name="moduleTracker">The module tracker.</param>
        public ModuleD(IModuleTracker moduleTracker)
        {
            if (moduleTracker == null)
            {
                throw new ArgumentNullException("moduleTracker");
            }

            this.moduleTracker = moduleTracker;
            this.moduleTracker.RecordModuleConstructed(WellKnownModuleNames.ModuleD);
        }

        /// <summary>
        /// Notifies the module that it has be initialized.
        /// </summary>
        public void Initialize()
        {
            this.moduleTracker.RecordModuleInitialized(WellKnownModuleNames.ModuleD);
        }
    }
}
