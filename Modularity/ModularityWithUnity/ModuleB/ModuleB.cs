

using System;
using Prism.Modularity;
using ModuleTracking;

namespace ModularityWithUnity.Desktop
{
    /// <summary>
    /// A module for the quickstart.
    /// </summary>
    [Module(ModuleName = WellKnownModuleNames.ModuleB, OnDemand = true)]
    public class ModuleB : IModule
    {
        private readonly IModuleTracker moduleTracker;

        /// <summary>
        /// Initializes a new instance of the <see cref="ModuleB"/> class.
        /// </summary>
        /// <param name="moduleTracker">The module tracker.</param>
        public ModuleB(IModuleTracker moduleTracker)
        {
            if (moduleTracker == null)
            {
                throw new ArgumentNullException("moduleTracker");
            }

            this.moduleTracker = moduleTracker;
            this.moduleTracker.RecordModuleConstructed(WellKnownModuleNames.ModuleB);
        }

        /// <summary>
        /// Notifies the module that it has be initialized.
        /// </summary>
        public void Initialize()
        {
            this.moduleTracker.RecordModuleInitialized(WellKnownModuleNames.ModuleB);
        }
    }
}
