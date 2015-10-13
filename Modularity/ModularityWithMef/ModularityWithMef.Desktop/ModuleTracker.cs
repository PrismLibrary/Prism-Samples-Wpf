// Copyright (c) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.

using System.ComponentModel.Composition;
using Prism.Logging;
using Prism.Modularity;
using ModuleTracking;

namespace ModularityWithMef.Desktop
{
    /// <summary>
    /// Provides tracking of modules for the quickstart.
    /// </summary>
    /// <remarks>
    /// This class is for demonstration purposes for the quickstart and not expected to be used in a real world application.
    /// This class exports the interface for modules and the concrete type for the shell.    
    /// </remarks>
    [Export(typeof(IModuleTracker))]
    public class ModuleTracker : IModuleTracker
    {
        private readonly ModuleTrackingState moduleATrackingState;
        private readonly ModuleTrackingState moduleBTrackingState;
        private readonly ModuleTrackingState moduleCTrackingState;
        private readonly ModuleTrackingState moduleDTrackingState;
        private readonly ModuleTrackingState moduleETrackingState;
        private readonly ModuleTrackingState moduleFTrackingState;

#pragma warning disable 649  // MEF will import
        [Import] private ILoggerFacade logger;
#pragma warning restore 649

        /// <summary>
        /// Initializes a new instance of the <see cref="ModuleTracker"/> class.
        /// </summary>
        public ModuleTracker()
        {
            // These states are defined specifically for the desktop version of the quickstart.
            this.moduleATrackingState = new ModuleTrackingState
                                            {
                                                ModuleName = WellKnownModuleNames.ModuleA,
                                                ExpectedDiscoveryMethod = DiscoveryMethod.ApplicationReference,
                                                ExpectedInitializationMode = InitializationMode.WhenAvailable,
                                                ExpectedDownloadTiming = DownloadTiming.WithApplication,
                                                ConfiguredDependencies = WellKnownModuleNames.ModuleD,
                                            };

            this.moduleBTrackingState = new ModuleTrackingState
                                            {
                                                ModuleName = WellKnownModuleNames.ModuleB,
                                                ExpectedDiscoveryMethod = DiscoveryMethod.DirectorySweep,
                                                ExpectedInitializationMode = InitializationMode.OnDemand,
                                                ExpectedDownloadTiming = DownloadTiming.InBackground,
                                            };
            this.moduleCTrackingState = new ModuleTrackingState
                                            {
                                                ModuleName = WellKnownModuleNames.ModuleC,
                                                ExpectedDiscoveryMethod = DiscoveryMethod.ApplicationReference,
                                                ExpectedInitializationMode = InitializationMode.OnDemand,
                                                ExpectedDownloadTiming = DownloadTiming.WithApplication,
                                            };
            this.moduleDTrackingState = new ModuleTrackingState
                                            {
                                                ModuleName = WellKnownModuleNames.ModuleD,
                                                ExpectedDiscoveryMethod = DiscoveryMethod.DirectorySweep,
                                                ExpectedInitializationMode = InitializationMode.WhenAvailable,
                                                ExpectedDownloadTiming = DownloadTiming.InBackground,
                                            };
            this.moduleETrackingState = new ModuleTrackingState
                                            {
                                                ModuleName = WellKnownModuleNames.ModuleE,
                                                ExpectedDiscoveryMethod = DiscoveryMethod.ConfigurationManifest,
                                                ExpectedInitializationMode = InitializationMode.OnDemand,
                                                ExpectedDownloadTiming = DownloadTiming.InBackground,
                                            };
            this.moduleFTrackingState = new ModuleTrackingState
                                            {
                                                ModuleName = WellKnownModuleNames.ModuleF,
                                                ExpectedDiscoveryMethod = DiscoveryMethod.ConfigurationManifest,
                                                ExpectedInitializationMode = InitializationMode.OnDemand,
                                                ExpectedDownloadTiming = DownloadTiming.InBackground,
                                                ConfiguredDependencies = WellKnownModuleNames.ModuleE,
                                            };
        }

        /// <summary>
        /// Gets the tracking state of module A.
        /// </summary>
        /// <value>A ModuleTrackingState.</value>
        /// <remarks>
        /// This is exposed as a specific property for data-binding purposes in the quickstart UI.
        /// </remarks>
        public ModuleTrackingState ModuleATrackingState
        {
            get { return this.moduleATrackingState; }
        }

        /// <summary>
        /// Gets the tracking state of module B.
        /// </summary>
        /// <value>A ModuleTrackingState.</value>
        /// <remarks>
        /// This is exposed as a specific property for data-binding purposes in the quickstart UI.
        /// </remarks>
        public ModuleTrackingState ModuleBTrackingState
        {
            get { return this.moduleBTrackingState; }
        }

        /// <summary>
        /// Gets the tracking state of module C.
        /// </summary>
        /// <value>A ModuleTrackingState.</value>
        /// <remarks>
        /// This is exposed as a specific property for data-binding purposes in the quickstart UI.
        /// </remarks>
        public ModuleTrackingState ModuleCTrackingState
        {
            get { return this.moduleCTrackingState; }
        }

        /// <summary>
        /// Gets the tracking state of module D.
        /// </summary>
        /// <value>A ModuleTrackingState.</value>
        /// <remarks>
        /// This is exposed as a specific property for data-binding purposes in the quickstart UI.
        /// </remarks>
        public ModuleTrackingState ModuleDTrackingState
        {
            get { return this.moduleDTrackingState; }
        }

        /// <summary>
        /// Gets the tracking state of module E.
        /// </summary>
        /// <value>A ModuleTrackingState.</value>
        /// <remarks>
        /// This is exposed as a specific property for data-binding purposes in the quickstart UI.
        /// </remarks>
        public ModuleTrackingState ModuleETrackingState
        {
            get { return this.moduleETrackingState; }
        }

        /// <summary>
        /// Gets the tracking state of module F.
        /// </summary>
        /// <value>A ModuleTrackingState.</value>
        /// <remarks>
        /// This is exposed as a specific property for data-binding purposes in the quickstart UI.
        /// </remarks>
        public ModuleTrackingState ModuleFTrackingState
        {
            get { return this.moduleFTrackingState; }
        }

        
        /// <summary>
        /// Records the module is loading.
        /// </summary>
        /// <param name="moduleName">The <see cref="WellKnownModuleNames">well-known name</see> of the module.</param>
        /// <param name="bytesReceived">The number of bytes downloaded.</param>
        /// <param name="totalBytesToReceive">The total number of bytes expected.</param>
        public void RecordModuleDownloading(string moduleName, long bytesReceived, long totalBytesToReceive)
        {
            ModuleTrackingState moduleTrackingState = this.GetModuleTrackingState(moduleName);
            if (moduleTrackingState != null)
            {
                moduleTrackingState.BytesReceived = bytesReceived;
                moduleTrackingState.TotalBytesToReceive = totalBytesToReceive;

                if (bytesReceived < totalBytesToReceive)
                {
                    moduleTrackingState.ModuleInitializationStatus = ModuleInitializationStatus.Downloading;
                }
                else
                {
                    moduleTrackingState.ModuleInitializationStatus = ModuleInitializationStatus.Downloaded;
                }
            }

            this.logger.Log(
                string.Format("'{0}' module is loading {1}/{2} bytes.", moduleName, bytesReceived, totalBytesToReceive),
                Category.Debug, 
                Priority.Low);
        }       

        /// <summary>
        /// Records the module has been constructed.
        /// </summary>
        /// <param name="moduleName">The <see cref="WellKnownModuleNames">well-known name</see> of the module.</param>
        public void RecordModuleConstructed(string moduleName)
        {
            ModuleTrackingState moduleTrackingState = this.GetModuleTrackingState(moduleName);
            if (moduleTrackingState != null)
            {
                moduleTrackingState.ModuleInitializationStatus = ModuleInitializationStatus.Constructed;
            }

            this.logger.Log(string.Format("'{0}' module constructed.", moduleName), Category.Debug, Priority.Low);
        }


        /// <summary>
        /// Records the module has been initialized.
        /// </summary>
        /// <param name="moduleName">The <see cref="WellKnownModuleNames">well-known name</see> of the module.</param>
        public void RecordModuleInitialized(string moduleName)
        {
            ModuleTrackingState moduleTrackingState = this.GetModuleTrackingState(moduleName);
            if (moduleTrackingState != null)
            {
                moduleTrackingState.ModuleInitializationStatus = ModuleInitializationStatus.Initialized;
            }

            this.logger.Log(string.Format("{0} module initialized.", moduleName), Category.Debug, Priority.Low);
        }

        /// <summary>
        /// Records the module is loaded.
        /// </summary>
        /// <param name="moduleName">The <see cref="WellKnownModuleNames">well-known name</see> of the module.</param>
        public void RecordModuleLoaded(string moduleName)
        {
            this.logger.Log(string.Format("'{0}' module loaded.", moduleName), Category.Debug, Priority.Low);
        }

        // A helper to make updating specific property instances by name easier.
        private ModuleTrackingState GetModuleTrackingState(string moduleName)
        {
            switch (moduleName)
            {
                case WellKnownModuleNames.ModuleA:
                    return this.ModuleATrackingState;
                case WellKnownModuleNames.ModuleB:
                    return this.ModuleBTrackingState;
                case WellKnownModuleNames.ModuleC:
                    return this.ModuleCTrackingState;
                case WellKnownModuleNames.ModuleD:
                    return this.ModuleDTrackingState;
                case WellKnownModuleNames.ModuleE:
                    return this.ModuleETrackingState;
                case WellKnownModuleNames.ModuleF:
                    return this.ModuleFTrackingState;
                default:
                    return null;
            }
        }
    }
}
